using ImageSortingModule;
using ImageSortingModule.Files;
using MaterialDesignThemes.Wpf;
using PhotoKinia.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.ViewModels
{
    class ProgressControlViewModel : ViewModelBase
    {
        private readonly IImageSorter sorter;
        private readonly IEnumerable<string> imageFiles;
        private readonly string outputDirectory;
        private readonly IFileOperation fileOperation;
        private BackgroundWorker worker;

        private int minimum;
        public int Minimum
        {
            get { return minimum; }
            set { minimum = value; RaisePropertyChanged(nameof(Minimum)); }
        }

        private long maximum;
        public long Maximum
        {
            get { return maximum; }
            set { maximum = value; RaisePropertyChanged(nameof(Maximum)); }
        }

        private long progress;
        public long Progress
        {
            get => progress;
            set { progress = value; RaisePropertyChanged(nameof(Progress)); }
        }

        private string finishText;
        public string FinishText
        {
            get { return finishText; }
            set { finishText = value; RaisePropertyChanged(nameof(FinishText)); }
        }

        private string dialogMessage;
        public string DialogMessage
        {
            get { return dialogMessage; }
            set { dialogMessage = value; RaisePropertyChanged(nameof(DialogMessage)); }
        }


        public event EventHandler OnCompleted;

        public ProgressControlViewModel(IImageSorter sorter, IEnumerable<string> imageFiles, string outputDirectory, IFileOperation fileOperation)
        {
            this.sorter = sorter;
            this.imageFiles = imageFiles;
            this.outputDirectory = outputDirectory;
            this.fileOperation = fileOperation;
            FinishText = Properties.Resources.ProgressControlAbortButtonLabel;
            Minimum = 0;
            Maximum = 100;
            Progress = 0;
        }

        public void OnDialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {
            DialogMessage = Properties.Resources.ProgressControlProcessingLabel;
            worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        public void OnDialogClosed(object sender, DialogClosingEventArgs eventArgs)
        {
            if(worker.IsBusy)
                worker.CancelAsync();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!e.Cancelled)
                {
                    DialogMessage = Properties.Resources.ProgressControlFinishLabel;
                    FinishText = Properties.Resources.ProgressControlFinishButtonLabel;
                }

                OnCompleted?.Invoke(this, new EventArgs());
            }
            finally
            {
                sorter.SortingProgressChanged -= Sorter_SortingProgressChanged;
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            sorter.SortingProgressChanged += Sorter_SortingProgressChanged;
            sorter.Sort(imageFiles, outputDirectory, fileOperation);
            if (worker.CancellationPending)
                e.Cancel = true;
        }

        private void Sorter_SortingProgressChanged(object sender, SortingProgressChangedEventArgs e)
        {
            float progressPercentage = (float)e.CurrentPhotoNumber / (float)e.TotalNumberOfPhotos * 100.0f;
            if (worker.CancellationPending)
                e.CancelSorting = true;
            worker.ReportProgress((int)progressPercentage);
        }
    }
}
