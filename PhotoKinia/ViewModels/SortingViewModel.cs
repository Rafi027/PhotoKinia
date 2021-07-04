using ImageSortingModule;
using ImageSortingModule.Classification.EqualityCheck;
using ImageSortingModule.FileListGeneration;
using ImageSortingModule.Files;
using PhotoKinia.Contracts;
using PhotoKinia.Models;
using PhotoKinia.Modules.ImageSortingModule;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PhotoKinia.ViewModels
{
    public class SortingViewModel : ViewModelBase
    {
        private readonly IFileListGenerator fileListGenerator;
        private readonly IImageSorter sorter;
        private readonly IDirectorySelector directorySelector;
        private readonly IDialogHostWrapper dialogHostWrapper;

        public ObservableCollection<string> InputDirectories { get; private set; }
        public string SelectedDirectory { get; set; }
        public FileOperationMode FileMode { get; set; }
        private string outputDirectory;

        public string OutputDirectory
        {
            get { return outputDirectory; }
            set { outputDirectory = value; RaisePropertyChanged(nameof(OutputDirectory)); }
        }

        public ICommand AddDirectory { get; private set; }
        public ICommand RemoveDirectory { get; private set; }
        public ICommand RunProcessing { get; private set; }
        public ICommand SelectOutputDirectory { get; set; }

        public SortingViewModel(IFileListGenerator fileListGenerator, ImageSortingModule.IImageSorter imageSorter, IDirectorySelector directorySelector, IDialogHostWrapper dialogHostWrapper)
        {
            this.fileListGenerator = fileListGenerator;
            sorter = imageSorter;
            this.directorySelector = directorySelector;
            this.dialogHostWrapper = dialogHostWrapper;
            Initialize();
        }

        private void Initialize()
        {
            InputDirectories = new ObservableCollection<string>();

            AddDirectory = new SimpleCommand((o) => 
            {
                var selectedPath = directorySelector.SelectDirectory();
                if (string.IsNullOrEmpty(selectedPath) || InputDirectories.Contains(selectedPath))
                    return;
                
                InputDirectories.Add(selectedPath);
            });

            RemoveDirectory = new SimpleCommand((o) =>
            {   
                if(InputDirectories.Contains(SelectedDirectory))
                    InputDirectories.Remove(SelectedDirectory);
            });

            RunProcessing = new SimpleCommand(async (o) =>
            {
                var progressControlViewModel = new ProgressControlViewModel(sorter, fileListGenerator.GetFiles(InputDirectories), OutputDirectory, GetFileOperatingMode());
                await dialogHostWrapper.ShowAsync(null, "SortingPageHost", OnDialogOpened, null);
            },
            new Predicate<object>((o) => InputDirectories.Count > 0 && !string.IsNullOrEmpty(OutputDirectory)));

            SelectOutputDirectory = new SimpleCommand((o) =>
            {
                var selectedPath = directorySelector.SelectDirectory();
                if (string.IsNullOrEmpty(selectedPath) || InputDirectories.Contains(selectedPath))
                    return;

                OutputDirectory = selectedPath;
            });
        }

        private IFileOperation GetFileOperatingMode()
        {
            switch (FileMode)
            {
                case FileOperationMode.Copy:
                    return new FileCopyOperation();
                case FileOperationMode.Move:
                    return new FileMoveOperation();
            }
            throw new ArgumentException("Cannot select file operation mode");
        }

        void OnDialogOpened(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {
            sorter.Sort(fileListGenerator.GetFiles(InputDirectories), OutputDirectory, GetFileOperatingMode());
        }
    }
}
