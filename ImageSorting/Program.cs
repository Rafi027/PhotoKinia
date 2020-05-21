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
            var sort = new ImageSorter(null, new DateTimeClassification(new ExifCreationDateReader()), null);
            sort.Sort(args[0], args[1]);
        }
    }
}
