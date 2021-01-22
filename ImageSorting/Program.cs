using ImageSortingModule.Classification.EqualityCheck;
using ImageSortingModule.FileListGeneration;
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
            var sort = new ImageSorter(new SubDirectoriesTextFileSource(args[0]), new DateTimeClassification(new ExifCreationDateReader()), new MD5Check());
            sort.Sort(args[1]);
        }
    }
}
