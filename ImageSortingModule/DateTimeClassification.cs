using ImageSortingModule;
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

            return new ClassificationResult
            {
                ClassifiedPath = ConvertDateToRelativePath(creationDate, Path.GetFileName(imagePath)),
                Success = true
            };
        }

        private ClassifiedPath ConvertDateToRelativePath(DateTime creationDate, string imageName)
        {
            return new ClassifiedPath
            {
                OutputDirectory = storageDirectory,
                Year = creationDate.Year.ToString(),
                Month = monthToString.Convert(creationDate.Month),
                Day = creationDate.Day.ToString(),
                FileName = imageName
            };
        }

    }
}
