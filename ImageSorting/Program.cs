using ImageSortingModule.Classification.EqualityCheck;
using ImageSortingModule.FileListGeneration;
using ImageSortingModule.Files;
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
            NLog.LogManager.GetCurrentClassLogger().Info("Session started");
            var sort = new ImageSorter(new DateTimeClassification(new MetadataCreationDateReader()), new MD5Check(), new FileMoveOperation());
            sort.Sort(new SubDirectoriesSearchBase().GetFiles(File.ReadAllLines(args[0])), args[1]);
        }
    }
}
