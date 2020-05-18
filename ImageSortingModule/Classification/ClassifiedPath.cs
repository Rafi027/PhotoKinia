using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageSortingModule
{
    public class ClassifiedPath
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string FileName { get; set; }
        public string FullPath => Path.Combine(new string[] { Year, Month, Day, FileName });
    }
}
