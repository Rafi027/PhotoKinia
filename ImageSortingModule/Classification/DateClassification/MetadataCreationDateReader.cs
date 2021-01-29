using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.QuickTime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Modules.ImageSortingModule
{
    public class MetadataCreationDateReader : ICreationDateReader
    {
        public DateTime Read(string imagePath)
        {

            using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                var metadata = ImageMetadataReader.ReadMetadata(stream);

                switch (Path.GetExtension(imagePath).ToLower())
                {
                    case ".mp4":
                        return ReadMovieHeaderCreationDate(metadata, imagePath);
                    case ".jpg":
                    case ".dng":
                        return ReadExifCreationDate(metadata, imagePath);
                    default:
                        throw new Exception($"Not supported image extension. File path: {imagePath}");
                }

            }
        }

        private DateTime ReadMovieHeaderCreationDate(IReadOnlyList<MetadataExtractor.Directory> metadata, string imagePath)
        {
            var movieHeader = metadata.OfType<QuickTimeMovieHeaderDirectory>();
            foreach (var headerData in movieHeader)
            {
                if (headerData.TryGetDateTime(QuickTimeMovieHeaderDirectory.TagCreated, out DateTime creationDate))
                    return creationDate;
            }

            throw new Exception($"Cannot read file creation date from exif. File path: {imagePath}");
        }

        private DateTime ReadExifCreationDate(IReadOnlyList<MetadataExtractor.Directory> metadata, string imagePath)
        {
            var exifDataDirectories = metadata.OfType<ExifSubIfdDirectory>();
            foreach (var exifData in exifDataDirectories)
            {
                if (exifData.TryGetDateTime(ExifDirectoryBase.TagDateTimeOriginal, out DateTime creationDate))
                    return creationDate;
            }

            throw new Exception($"Cannot read file creation date from exif. File path: {imagePath}");
        }
    }
}
