using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSortingModule.Classification.EqualityCheck
{
    public interface IImageEqualityCheck
    {
        bool Equals(string firstFile, string secondFile);
    }
}
