using ImageSortingModule.Files;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSortingModule
{
    public interface IImageSorter
    {
        event EventHandler<SortingProgressChangedEventArgs> SortingProgressChanged;
        void Sort(IEnumerable<string> imageFiles, string outputDirectory, IFileOperation fileOperation);
    }
}
