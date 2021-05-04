using Ookii.Dialogs.Wpf;
using PhotoKinia.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Utils.FileOperations
{
    public class UIDirectoryBrowser : IDirectorySelector
    {
        public string SelectDirectory()
        {
            var folderBrowser = new VistaFolderBrowserDialog();
            if (folderBrowser.ShowDialog() != true)
                return string.Empty;
            return folderBrowser.SelectedPath;
        }
    }
}
