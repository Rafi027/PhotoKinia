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
        }
    }
}
