using ImageSortingModule;
using ImageSortingModule.Classification.EqualityCheck;
using ImageSortingModule.Classification.RenameMethod;
using ImageSortingModule.Files;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Modules.ImageSortingModule
{
    public class ImageSorter : IImageSorter
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IImageClassificationMethod imageClassification;
        private readonly IImageEqualityCheck imageEquality;

        public ImageSorter(IImageClassificationMethod imageClassification, IImageEqualityCheck imageEquality)
        {
            Logger.Trace("ImageSorter(IFileListGenerator fileProvider, IImageClassificationMethod imageClassification, IImageEqualityCheck imageEquality)");
            this.imageClassification = imageClassification;
            this.imageEquality = imageEquality;
        }

        public event EventHandler<SortingProgressChangedEventArgs> SortingProgressChanged;

        public void Sort(IEnumerable<string> imageFiles, string outputDirectory, IFileOperation fileOperation)
        {
            Logger.Trace("void Sort({outputDirectory})", outputDirectory);
            var totalNumberOfFiles = imageFiles.Count();
            var currentFileNumber = 0;
            var totalNumberOfPhotos = imageFiles.Count();
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

                        SortingProgressChanged?.Invoke(this, new SortingProgressChangedEventArgs { CurrentPhotoNumber = currentFileNumber, TotalNumberOfPhotos = totalNumberOfPhotos });
                        var destinationFilePath = Path.Combine(outputDirectory, classification.ClassifiedPath.RelativePath);
                        if (!File.Exists(destinationFilePath))
                        {
                            if (!fileOperation.Process(image, destinationFilePath))
                                Logger.Error("Cannot process file: {filePath}", image);
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
