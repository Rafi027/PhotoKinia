using PhotoKinia.Modules.ImageSortingModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageSortingModule.FileListGeneration
{
    public class DirectoriesFileSource : IFileListGenerator
    {
        private readonly string sourceFilePath;

        public DirectoriesFileSource(string sourceFilePath)
        {
            this.sourceFilePath = sourceFilePath;
        }

        public List<string> GetFiles()
        {
            if (!File.Exists(sourceFilePath))
            {
                Console.WriteLine($"Error - Cannot find file with photo directories. File: {sourceFilePath}");
                return null;
            }

            var directories = File.ReadAllLines(sourceFilePath);
            var result = new List<string>();
            foreach (var directory in directories)
            {
                var directoryInfo = new DirectoryInfo(directory);
                var imageFiles = directoryInfo.GetFiles().Where(i => i.Extension.ToLower().Equals(".jpg")).Select(f => f.FullName).ToList();
                result.AddRange(imageFiles);
            }

            return result;
        }
    }
}
