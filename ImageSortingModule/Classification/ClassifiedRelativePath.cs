using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageSortingModule
{
    public class ClassifiedRelativePath
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string FileName { get; set; }
        public string RelativePath => Path.Combine(new string[] { Year, Month, Day, FileName });
    }
}
