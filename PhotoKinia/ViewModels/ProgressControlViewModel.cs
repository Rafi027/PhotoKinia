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
        private BackgroundWorker worker;

        public string FinishText
        {
            get { return finishText; }
            set { finishText = value; RaisePropertyChanged(nameof(FinishText)); }
        }

        public event EventHandler OnCompleted;

        public ProgressControlViewModel(IImageSorter sorter, IEnumerable<string> imageFiles, string outputDirectory, IFileOperation fileOperation)
        {
            this.sorter = sorter;
            this.imageFiles = imageFiles;
            this.outputDirectory = outputDirectory;
            this.fileOperation = fileOperation;
            FinishText = "Abort";
            Minimum = 0;
            Maximum = 100;
            Progress = 0;
        }

        public void OnDialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerAsync();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FinishText = "Close";
            OnCompleted?.Invoke(this, new EventArgs());
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            sorter.Sort(imageFiles, outputDirectory, fileOperation);
        }
    }
}
