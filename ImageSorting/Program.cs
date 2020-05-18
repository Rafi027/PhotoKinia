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
            var sort = new ImageSorter(null, new DateTimeClassification(args[1], new ExifCreationDateReader()));
            sort.Sort("", args[0]);
        }
    }
}
