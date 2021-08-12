using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSortingModule
{
    public class SortingProgressChangedEventArgs: EventArgs
    {
        public int TotalNumberOfPhotos { get; set; }
        public int CurrentPhotoNumber { get; set; }
    }
}
