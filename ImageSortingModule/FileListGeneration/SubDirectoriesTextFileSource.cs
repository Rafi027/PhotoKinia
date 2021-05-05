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
    public class SubDirectoriesTextFileSource : SubDirectoriesSearchBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly string sourceFilePath;

        public SubDirectoriesTextFileSource(string sourceFilePath)
        {
            this.sourceFilePath = sourceFilePath;
        }

        protected override List<string> GetDirectoriesToSearch()
        {
            if (!File.Exists(sourceFilePath))
            {
                Logger.Error("Cannot find file with photo directories. File: {sourceFilePath}", sourceFilePath);
                return null;
            }

            return File.ReadAllLines(sourceFilePath).ToList();
        }
    }
}
