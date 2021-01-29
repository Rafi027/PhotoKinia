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
    public class DateTimeClassification : IImageClassificationMethod
    {
        private readonly ICreationDateReader dateReader;
        private readonly MonthToString monthToString = new MonthToString();

        public DateTimeClassification(ICreationDateReader dateReader)
        {
            this.dateReader = dateReader;
        }

        public ClassificationResult GetClassifiedFilePath(string imagePath)
        {
            try
            {
                var creationDate = dateReader.Read(imagePath);

                return new ClassificationResult
                {
                    ClassifiedPath = ConvertDateToRelativePath(creationDate, Path.GetFileName(imagePath)),
                    Success = true
                };
            }
            catch (Exception)
            {

                return new ClassificationResult
                {
                    ClassifiedPath = null,
                    Success = false
                };
            }
        }

        private ClassifiedRelativePath ConvertDateToRelativePath(DateTime creationDate, string imageName)
        {
            return new ClassifiedRelativePath
            {
                Year = creationDate.Year.ToString(),
                Month = monthToString.Convert(creationDate.Month),
                Day = creationDate.Day.ToString(),
                FileName = imageName
            };
        }

    }
}
