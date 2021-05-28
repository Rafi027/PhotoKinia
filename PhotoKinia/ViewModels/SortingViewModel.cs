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
        private readonly IImageSorter sorter;
        private readonly IDirectorySelector directorySelector;

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

        public SortingViewModel(ImageSortingModule.IImageSorter imageSorter, IDirectorySelector directorySelector)
        {
            sorter = imageSorter;
            this.directorySelector = directorySelector;
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

            RunProcessing = new SimpleCommand((o) =>
            {
                //var inputData = new SubDirectoriesSearchBase().GetFiles(InputDirectories.ToList());
                //var sorter = new ImageSorter(
                //    new DateTimeClassification(new MetadataCreationDateReader()),
                //    new MD5Check(),
                //    GetFileOperatingMode());

                sorter.Sort(new List<string>(), @"C:\Users\Rafi\Documents\SortingTest\Output");
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
    }
}
