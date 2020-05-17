using PhotoKinia.Modules.ImageSortingModule;
using System;
using System.IO;
using System.Linq;

namespace ImageSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var inputDirectory = args[0];
            var outputDirectory = args[1];
            var classification = new DateTimeClassification(outputDirectory, new ExifCreationDateReader());
            var directoryInfo = new DirectoryInfo(inputDirectory);

            var imageFiles = directoryInfo.GetFiles().Where(i => i.Extension.ToLower().Equals(".jpg")).Select(f => f.FullName).ToList();
            foreach (var image in imageFiles)
            {
                var newImagePath = classification.GetClassifiedFilePath(image);
                string directoryPath = Path.Combine(newImagePath.ClassifiedPath.OutputDirectory,
                    newImagePath.ClassifiedPath.Year, newImagePath.ClassifiedPath.Month, newImagePath.ClassifiedPath.Day);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                try
                {
                    Console.WriteLine($"Copy file {Path.GetFileName(image)} to {newImagePath.ClassifiedPath.FullPath}");
                    File.Copy(image, newImagePath.ClassifiedPath.FullPath);
                }
                catch (Exception)
                {
                    Console.WriteLine($"ERROR - Cannot copy image. Source: {image}. Destination: {newImagePath.ClassifiedPath.FullPath}");
                }
            }

        }
    }
}
