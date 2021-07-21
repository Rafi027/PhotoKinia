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
    class ProgressControlViewModel: ViewModelBase
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
        }

        public void OnDialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
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
