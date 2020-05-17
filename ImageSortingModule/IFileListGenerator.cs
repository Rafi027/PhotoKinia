using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("PhotoKiniaTests")]

namespace PhotoKinia.Modules.ImageSortingModule
{
    internal interface IFileListGenerator
    {
        string[] GetFiles();
    }
}
