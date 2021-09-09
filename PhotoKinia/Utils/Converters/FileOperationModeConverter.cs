using PhotoKinia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoKinia.Utils.Converters
{
    static class FileOperationModeConverter
    {
        public static FileOperationMode TextToEnum(string text)
        {
            if (text.Equals(Properties.Resources.FileCopyMode))
                return FileOperationMode.Copy;
            if (text.Equals(Properties.Resources.FileMoveMode))
                return FileOperationMode.Move;

            throw new ArgumentException("Unknow file operating mode!");
        }

        public static string EnumToTranslatedString(FileOperationMode mode)
        {
            switch (mode)
            {
                case FileOperationMode.Copy:
                    return Properties.Resources.FileCopyMode;
                case FileOperationMode.Move:
                    return Properties.Resources.FileMoveMode;
                default:
                    break;
            }

            throw new ArgumentException("Unknow file operating mode!");
        }
    }
}
