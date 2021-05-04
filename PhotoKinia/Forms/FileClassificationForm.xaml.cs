using PhotoKinia.Modules.ImageSortingModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhotoKinia.Forms
{
    /// <summary>
    /// Interaction logic for FileClassificationForm.xaml
    /// </summary>
    public partial class FileClassificationForm : Window
    {
        public FileClassificationForm()
        {
            InitializeComponent();
            var reader = new MetadataCreationDateReader();
            var exists = File.Exists(@"Images\ExifDemo\demo.jpg");
            var date = reader.Read(@"Images\ExifDemo\demo.jpg");

        }
    }
}
