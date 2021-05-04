using PhotoKinia.Contracts;
using PhotoKinia.Models;
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
        private readonly IDirectorySelector directorySelector;

        public ObservableCollection<string> InputDirectories { get; private set; }
        public string SelectedDirectory { get; set; }
        public FileOperationMode FileMode { get; set; }

        public ICommand AddDirectory { get; private set; }
        public ICommand RemoveDirectory { get; private set; }
        public ICommand RunProcessing { get; private set; }

        public SortingViewModel(IDirectorySelector directorySelector)
        {
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
        }
    }
}
