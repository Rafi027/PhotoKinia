using ImageSortingModule.Utils.RecursionHelper;
using NLog;
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
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly string sourceFilePath;

        public SubDirectoriesTextFileSource(string sourceFilePath)
        {
            this.sourceFilePath = sourceFilePath;
        }

        public List<string> GetFiles()
        {
            if (!File.Exists(sourceFilePath))
            {
                Logger.Error("Cannot find file with photo directories. File: {sourceFilePath}", sourceFilePath);
                return null;
            }

            var directories = File.ReadAllLines(sourceFilePath);
            var result = new List<string>();
            foreach (var directory in directories)
            {
                if (directory.StartsWith("#"))
                    continue;
                var imageFiles = RecursiveSearch(directory);
                result.AddRange(imageFiles);
            }

            return result;

        }

        private IEnumerable<string> GetFilesFromDirectory(string directory)
        {
            var directoryInfo = new DirectoryInfo(directory);
            var imageFiles = directoryInfo.GetFiles().ToArray();
            foreach (var file in imageFiles)
            {
                if(file.Extension.ToLower().Equals(".jpg") || file.Extension.ToLower().Equals(".dng"))
                    yield  return file.FullName;
            }
        }

        private List<string> RecursiveSearch(string rootDirectory)
        {
            var directoriesToScan = new Stack<string>();
            directoriesToScan.Push(rootDirectory);
            var files = new List<string>();
            Trampoline.Start(Iteration, files, directoriesToScan);
            return files;
        }

        private Bounce<List<string>, Stack<string>, List<string>> Iteration(List<string> files, Stack<string> directoriesToScan)
        {
            var rootDirectory = directoriesToScan.Pop();
            var subdirectories = Directory.EnumerateDirectories(rootDirectory);
            foreach (var subdirectory in subdirectories)
                directoriesToScan.Push(subdirectory);

            files.AddRange(GetFilesFromDirectory(rootDirectory));

            return directoriesToScan.Count == 0 ? Bounce<List<string>, Stack<string>, List<string>>.End(files) :
                Bounce<List<string>, Stack<string>, List<string>>.Continue(files, directoriesToScan);
        }
    }
}
