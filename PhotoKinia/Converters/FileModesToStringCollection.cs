using PhotoKinia.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PhotoKinia.Converters
{
    class FileModesToStringCollection : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var modes = value as FileOperationMode[];
            var results = new List<string>(modes.Length);
            foreach (var mode in modes)
            {
                switch (mode)
                {
                    case FileOperationMode.Copy:
                        results.Add(Properties.Resources.FileCopyMode);
                        break;
                    case FileOperationMode.Move:
                        results.Add(Properties.Resources.FileMoveMode);
                        break;
                    default:
                        break;
                }
            }
            return results;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
