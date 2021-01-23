using ImageSortingModule.Classification.EqualityCheck;
using ImageSortingModule.Classification.RenameMethod;
using NLog;
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
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IFileListGenerator fileProvider;
        private readonly IImageClassificationMethod imageClassification;
        private readonly IImageEqualityCheck imageEquality;

        public ImageSorter(IFileListGenerator fileProvider, IImageClassificationMethod imageClassification, IImageEqualityCheck imageEquality)
        {
            Logger.Trace("ImageSorter(IFileListGenerator fileProvider, IImageClassificationMethod imageClassification, IImageEqualityCheck imageEquality)");
            this.fileProvider = fileProvider;
            this.imageClassification = imageClassification;
            this.imageEquality = imageEquality;
        }

        public void Sort(string outputDirectory)
        {
            Logger.Trace("void Sort({outputDirectory})", outputDirectory);
            var imageFiles = fileProvider.GetFiles();
            var totalNumberOfFiles = imageFiles.Count;
            var currentFileNumber = 0;
            foreach (var image in imageFiles)
            {
                currentFileNumber++;
                var classification = imageClassification.GetClassifiedFilePath(image);
                if(!classification.Success)
                {
                    Logger.Warn("Cannot classify file: {image}. File skipped.", image);
                    continue;
                }
                try
                {
                    int safetyBreak = 0;
                    while (true)
                    {
                        if(++safetyBreak > 10000)
                        {
                            Logger.Fatal("Cannot find new file name for file: {image}", image);
                            throw new InvalidOperationException($"Cannot find new file name for file: {image}");
                        }
                        
                        string directoryPath = Path.Combine(outputDirectory,
                                    classification.ClassifiedPath.Year, classification.ClassifiedPath.Month, classification.ClassifiedPath.Day);
                        if (!Directory.Exists(directoryPath))
                            Directory.CreateDirectory(directoryPath);

                        Logger.Info("Copy file {currentFileNumber}/{totalNumberOfFiles} {sourceImage} to {destinationPath}", currentFileNumber, totalNumberOfFiles, Path.GetFileName(image), classification.ClassifiedPath.RelativePath);

                        var destinationFilePath = Path.Combine(outputDirectory, classification.ClassifiedPath.RelativePath);
                        if (!File.Exists(destinationFilePath))
                        {
                            File.Copy(image, destinationFilePath, false);
                            break;
                        }

                        if (imageEquality.Equals(image, destinationFilePath))
                        {
                            Logger.Info("File {filePath} already exists at location {destinationFilePath}. Skip", image, destinationFilePath);
                            break;
                        }
                        var rename = new IncrementalRename();
                        classification.ClassifiedPath.FileName = rename.GetNewFileName(classification.ClassifiedPath.FileName);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Cannot copy image. Source: {sourcePath}. Destination: {destinationPath}", image, classification.ClassifiedPath.RelativePath);
                    Logger.Error(ex);
                }
            }
        }
    }
}
