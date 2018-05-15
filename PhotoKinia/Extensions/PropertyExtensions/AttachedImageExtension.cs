using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PhotoKinia.Extensions.PropertyExtensions
{
    class AttachedImageExtension
    {
        public static readonly DependencyProperty ImageProperty = DependencyProperty.RegisterAttached(
            "Image", typeof(ImageSource), typeof(AttachedImageExtension), new FrameworkPropertyMetadata());

        public static ImageSource GetImage(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageProperty);
        }

        public static void SetImage(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageProperty, value);
        }
    }
}
