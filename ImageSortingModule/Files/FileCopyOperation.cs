using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageSortingModule.Files
{
    public class FileCopyOperation : IFileOperation
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public bool Process(string sourceFile, string destinationFile)
        {
            try
            {
                File.Copy(sourceFile, destinationFile, false);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }

            return true;
        }
    }
}
