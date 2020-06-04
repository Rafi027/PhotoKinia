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
        private const string PhotoDirectories = "PhotoDirectories.txt";

        public string[] GetFiles()
        {
            if (!File.Exists(PhotoDirectories))
            {
                Console.WriteLine("Error - Cannot find file with photo directories");
                return null;
            }

            var directories = File.ReadAllLines(PhotoDirectories);
            var result = new List<string>();
            foreach (var directory in directories)
            {
                var directoryInfo = new DirectoryInfo(directory);
                var imageFiles = directoryInfo.GetFiles().Where(i => i.Extension.ToLower().Equals(".jpg")).Select(f => f.FullName).ToList();
                result.AddRange(imageFiles);
            }

            return result.ToArray();
        }
    }
}
