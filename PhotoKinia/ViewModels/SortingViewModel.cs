using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhotoKinia.ViewModels
{
    public class SortingViewModel : ViewModelBase
    {
        public ObservableCollection<string> InputDirectories { get; private set; }
        public ICommand AddDirectory { get; private set; }
        public ICommand RemoveDirectory { get; private set; }
        public ICommand RunProcessing { get; private set; }
    }
}
