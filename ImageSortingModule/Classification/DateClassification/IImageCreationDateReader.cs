using System;

namespace PhotoKinia.Modules.ImageSortingModule
{
    public  interface IImageCreationDateReader
    {
        DateTime Read(string imagePath);
    }
}