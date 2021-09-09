using PhotoKinia.Models;
using PhotoKinia.Utils.Converters;
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

            return FileOperationModeConverter.EnumToTranslatedString(mode);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return FileOperationModeConverter.TextToEnum(value.ToString());
        }
    }
}
