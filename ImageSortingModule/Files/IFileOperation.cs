using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSortingModule.Files
{
    public interface IFileOperation
    {
        bool Process(string sourceFile, string destinationFile);
    }
}
