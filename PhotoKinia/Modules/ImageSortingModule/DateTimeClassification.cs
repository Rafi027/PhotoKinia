using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Modules.ImageSortingModule
{
    class DateTimeClassification : IImageClassificationMethod
    {
        private string storageDirectory;

        public DateTimeClassification(string storageDirectory)
        {
            this.storageDirectory = storageDirectory;
        }

        public ClassificationResult GetClassifiedFilePath(string imagePath)
        {
            var creationDate = ReadFileCreationDate(imagePath);
            var relativePath = ConvertDateToRelativePath(creationDate);

            return new ClassificationResult
            {
                ClassifiedPath = Path.Combine(storageDirectory, relativePath),
                Success = true
            };
        }

        private string ConvertDateToRelativePath(DateTime creationDate)
        {
            return $"\\{creationDate.Year}\\{creationDate.Month}\\{creationDate.Day}\\";
        }

        private DateTime ReadFileCreationDate(string imagePath)
        {
            throw new NotImplementedException();
        }
    }
}
