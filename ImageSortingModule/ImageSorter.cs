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

        public void Sort(string outputDirectory)
        {
            Console.WriteLine("Sorting started!");
            var imageFiles = fileProvider.GetFiles();
            var totalNumberOfFiles = imageFiles.Count;
            var currentFileNumber = 0;
            foreach (var image in imageFiles)
            {
                var classification = imageClassification.GetClassifiedFilePath(image);
                if(!classification.Success)
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
                                    classification.ClassifiedPath.Year, classification.ClassifiedPath.Month, classification.ClassifiedPath.Day);
                        if (!Directory.Exists(directoryPath))
                            Directory.CreateDirectory(directoryPath);
                        Console.WriteLine($"Copy file {++currentFileNumber}/{totalNumberOfFiles} {Path.GetFileName(image)} to {classification.ClassifiedPath.RelativePath}");
                        var destinationFilePath = Path.Combine(outputDirectory, classification.ClassifiedPath.RelativePath);
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
                        classification.ClassifiedPath.FileName = rename.GetNewFileName(classification.ClassifiedPath.FileName);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"ERROR - Cannot copy image. Source: {image}. Destination: {classification.ClassifiedPath.RelativePath}");
                }
            }
        }
    }
}
