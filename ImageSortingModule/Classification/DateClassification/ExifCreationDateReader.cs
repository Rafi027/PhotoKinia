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
    public class ExifCreationDateReader : IImageCreationDateReader
    {
        public DateTime Read(string imagePath)
        {

            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                var metadata = ImageMetadataReader.ReadMetadata(stream);

                var exifDataDirectories = metadata.OfType<ExifSubIfdDirectory>();
                foreach (var exifData in exifDataDirectories)
                {
                    if (exifData.TryGetDateTime(ExifDirectoryBase.TagDateTimeOriginal, out DateTime creationDate))
                        return creationDate;
                }
            }
            throw new Exception($"Cannot read file creation date from exif. File path: {imagePath}");
        }
    }
}
