using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageSortingModule.Classification.RenameMethod
{
    internal class IncrementalRename
    {
        public string GetNewFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var noExtensionName = Path.GetFileNameWithoutExtension(fileName);

            throw new NotImplementedException();
        }
    }
}
