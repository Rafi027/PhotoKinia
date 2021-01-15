using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Modules.ImageSortingModule
{
    public interface IImageClassificationMethod
    {
        ClassificationResult GetClassifiedFilePath(string imagePath);
    }
}
