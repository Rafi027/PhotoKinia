using ImageSortingModule.Classification.EqualityCheck;
using ImageSortingModule.Classification.RenameMethod;
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
        private readonly IImageEqualityCheck imageEquality;

        public ImageSorter(IFileListGenerator fileProvider, IImageClassificationMethod imageClassification, IImageEqualityCheck imageEquality)
        {
            this.fileProvider = fileProvider;
            this.imageClassification = imageClassification;
            this.imageEquality = imageEquality;
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
                if(!newImagePath.Success)
                {
                    Console.WriteLine($"Cannot classify file: {image}. File skipped.");
                    continue;
                }
                try
                {
                    int safetyBreak = 0;
                    while (true)
                    {
                        if(++safetyBreak > 10000)
                            throw new InvalidOperationException($"Cannot find new file name for file: {image}");
                        
                        string directoryPath = Path.Combine(outputDirectory,
                                    newImagePath.ClassifiedPath.Year, newImagePath.ClassifiedPath.Month, newImagePath.ClassifiedPath.Day);
                        if (!Directory.Exists(directoryPath))
                            Directory.CreateDirectory(directoryPath);
                        Console.WriteLine($"Copy file {++currentFileNumber}/{totalNumberOfFiles} {Path.GetFileName(image)} to {newImagePath.ClassifiedPath.RelativePath}");
                        var destinationFilePath = Path.Combine(outputDirectory, newImagePath.ClassifiedPath.RelativePath);
                        if (!File.Exists(destinationFilePath))
                        {
                            File.Copy(image, destinationFilePath, false);
                            break;
                        }

                        if (imageEquality.Equals(image, destinationFilePath))
                        {
                            Console.WriteLine("File alredy exists. Skip.");
                            break;
                        }
                        var rename = new IncrementalRename();
                        newImagePath.ClassifiedPath.FileName = rename.GetNewFileName(newImagePath.ClassifiedPath.FileName);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"ERROR - Cannot copy image. Source: {image}. Destination: {newImagePath.ClassifiedPath.RelativePath}");
                }
            }
        }
    }
}
