using ImageSortingModule.Converters;
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
        private readonly IImageCreationDateReader dateReader;
        private readonly MonthToString monthToString = new MonthToString();

        public DateTimeClassification(string storageDirectory, IImageCreationDateReader dateReader)
        {
            this.storageDirectory = storageDirectory;
            this.dateReader = dateReader;
        }

        public ClassificationResult GetClassifiedFilePath(string imagePath)
        {
            var creationDate = dateReader.Read(imagePath);
            var relativePath = ConvertDateToRelativePath(creationDate, Path.GetFileName(imagePath));

            return new ClassificationResult
            {
                ClassifiedPath = Path.Combine(storageDirectory, relativePath),
                Success = true
            };
        }

        private string ConvertDateToRelativePath(DateTime creationDate, string imageName)
        {
            return $"{creationDate.Year}\\{monthToString.Convert(creationDate.Month)}\\{creationDate.Day}\\{imageName}";
        }

    }
}
