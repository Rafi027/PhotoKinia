using ImageSortingModule;
using ImageSortingModule.Files;
using MaterialDesignThemes.Wpf;
using PhotoKinia.ViewModels;
using System;
using System.Collections.Generic;
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

        private bool isFinished;

        public bool IsFinished
        {
            get { return isFinished; }
            set { isFinished = value; RaisePropertyChanged(nameof(isFinished)); }
        }


        public ProgressControlViewModel(IImageSorter sorter, IEnumerable<string> imageFiles, string outputDirectory, IFileOperation fileOperation)
        {
            this.sorter = sorter;
            this.imageFiles = imageFiles;
            this.outputDirectory = outputDirectory;
            this.fileOperation = fileOperation;
        }

        public void OnDialogOpened(object sender, DialogOpenedEventArgs eventArgs)
        {
            sorter.Sort(imageFiles, outputDirectory, fileOperation);
            IsFinished = true;
        }
    }
}
