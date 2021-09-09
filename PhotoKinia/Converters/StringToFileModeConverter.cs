using PhotoKinia.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PhotoKinia.Converters
{
    class StringToFileModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mode = (FileOperationMode)value;
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mode = value.ToString();
            if (mode.Equals(Properties.Resources.FileCopyMode))
                return FileOperationMode.Copy;
            if (mode.Equals(Properties.Resources.FileMoveMode))
                return FileOperationMode.Move;

            throw new ArgumentException("Unknow file operating mode!");
        }
    }
}
