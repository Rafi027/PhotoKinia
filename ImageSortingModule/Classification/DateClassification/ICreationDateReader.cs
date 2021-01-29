using System;

namespace PhotoKinia.Modules.ImageSortingModule
{
    public  interface ICreationDateReader
    {
        DateTime Read(string imagePath);
    }
}