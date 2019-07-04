using System;

namespace PhotoKinia.Modules.ImageSortingModule
{
    internal interface IImageCreationDateReader
    {
        DateTime Read(string imagePath);
    }
}