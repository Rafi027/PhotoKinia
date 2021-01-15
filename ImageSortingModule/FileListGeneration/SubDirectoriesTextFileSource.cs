using ImageSortingModule.Utils.RecursionHelper;
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

        private List<string> RecursiveSearch(string rootDirectory)
        {
            var directoriesToScan = new Stack<string>();
            directoriesToScan.Push(rootDirectory);
            var files = new List<string>();
            Trampoline.Start(Iteration, files, directoriesToScan);

            throw new NotImplementedException();
        }

        private Bounce<List<string>, Stack<string>, List<string>> Iteration(List<string> files, Stack<string> directoriesToScan)
        {
            var rootDirectory = directoriesToScan.Pop();
            var subdirectories = Directory.EnumerateDirectories(directoriesToScan.Pop());
            foreach (var subdirectory in subdirectories)
                directoriesToScan.Push(subdirectory);

            files.AddRange(GetFilesFromDirectory(rootDirectory, Extension));

            return directoriesToScan.Count == 0 ? Bounce<List<string>, Stack<string>, List<string>>.End(files) :
                Bounce<List<string>, Stack<string>, List<string>>.Continue(files, directoriesToScan);
        }
    }
}
