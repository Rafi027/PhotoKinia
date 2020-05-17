using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Modules.ImageSortingModule
{
    class ExifCreationDateReader : IImageCreationDateReader
    {
        public DateTime Read(string imagePath)
        {
            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                var metadata = ImageMetadataReader.ReadMetadata(stream);

                var exifData = metadata.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                return exifData.GetDateTime(ExifDirectoryBase.TagDateTimeOriginal);
            }
        }
    }
}
