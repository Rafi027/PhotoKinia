using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Modules.ImageSortingModule
{
    public class ImageSorter
    {
        private readonly IFileListGenerator fileProvider;
        private readonly IImageClassificationMethod imageClassification;

        public ImageSorter(IFileListGenerator fileProvider, IImageClassificationMethod imageClassification)
        {
            this.fileProvider = fileProvider;
            this.imageClassification = imageClassification;
        }

        public void Sort(string inputDirectory, string outputDirectory)
        {
            Console.WriteLine("Sorting started!");
            var directoryInfo = new DirectoryInfo(inputDirectory);

            var imageFiles = directoryInfo.GetFiles().Where(i => i.Extension.ToLower().Equals(".jpg")).Select(f => f.FullName).ToList();
            var totalNumberOfFiles = imageFiles.Count;
            var currentFileNumber = 0;
            foreach (var image in imageFiles)
            {
                var newImagePath = imageClassification.GetClassifiedFilePath(image);
                string directoryPath = Path.Combine(outputDirectory,
                    newImagePath.ClassifiedPath.Year, newImagePath.ClassifiedPath.Month, newImagePath.ClassifiedPath.Day);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                try
                {
                    Console.WriteLine($"Copy file {++currentFileNumber}/{totalNumberOfFiles} {Path.GetFileName(image)} to {newImagePath.ClassifiedPath.FullPath}");
                    var destinationFilePath = Path.Combine(outputDirectory, newImagePath.ClassifiedPath.FullPath);
                    if(!File.Exists(destinationFilePath))
                    {
                        File.Copy(image, destinationFilePath, false);
                        continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"ERROR - Cannot copy image. Source: {image}. Destination: {newImagePath.ClassifiedPath.FullPath}");
                }
            }
        }
    }
}
