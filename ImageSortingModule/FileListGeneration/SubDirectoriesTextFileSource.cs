using PhotoKinia.Modules.ImageSortingModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageSortingModule.FileListGeneration
{
    public class SubDirectoriesTextFileSource : IFileListGenerator
    {
        private const string Extension = ".jpg";
        private readonly string sourceFilePath;

        public SubDirectoriesTextFileSource(string sourceFilePath)
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
                List<string> imageFiles = SearchDirectories(directory);
                result.AddRange(imageFiles);
            }

            return result;

        }

        private static List<string> GetFilesFromDirectory(string directory, string extension)
        {
            var directoryInfo = new DirectoryInfo(directory);
            var imageFiles = directoryInfo.GetFiles().Where(i => i.Extension.ToLower().Equals(".jpg")).Select(f => f.FullName).ToList();
            return imageFiles;
        }

        private List<string> SearchDirectories(string directory)
        {
            var result = new List<string>();
            var subdirectories = Directory.EnumerateDirectories(directory).ToArray();
            if (subdirectories.Count() == 0)
                return GetFilesFromDirectory(directory, Extension);
            for (int i = 0; i < subdirectories.Count(); i++)
                result.AddRange(SearchDirectories(subdirectories[i]));

            return result;
        }
    }
}
