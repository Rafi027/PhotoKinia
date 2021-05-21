using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSortingModule
{
    public interface IImageSorter
    {
        void Sort(IEnumerable<string> imageFiles, string outputDirectory);
    }
}
