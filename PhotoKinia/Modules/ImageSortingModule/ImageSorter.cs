using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Modules.ImageSortingModule
{
    internal class ImageSorter
    {
        private IFileListGenerator fileProvider;

        public ImageSorter(IFileListGenerator fileProvider)
        {
            this.fileProvider = fileProvider;
        }

        public bool Sort(string outputDirectory, params string[] inputDirectories)
        {
            throw new NotImplementedException();
        }
    }
}
