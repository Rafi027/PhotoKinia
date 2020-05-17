using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Modules.ImageSortingModule
{
    internal class ImageSorter
    {
        private readonly IFileListGenerator fileProvider;
        private readonly IImageClassificationMethod imageClassification;

        public ImageSorter(IFileListGenerator fileProvider, IImageClassificationMethod imageClassification)
        {
            this.fileProvider = fileProvider;
            this.imageClassification = imageClassification;
        }

        public bool Sort(string outputDirectory, params string[] inputDirectories)
        {
            throw new NotImplementedException();
        }
    }
}
