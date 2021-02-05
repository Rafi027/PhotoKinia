using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ImageSortingModule.FileListGeneration
{
    internal class PathReductor
    {
        internal List<string> Reduce(List<string> directories)
        {
            var result = directories.ToList();
            var sorted = directories.OrderBy(d => d.Length);
            foreach (var currentDirectory in directories)
            {
                var subDirectories = directories.Where(d => d.StartsWith(currentDirectory) && !string.Equals(d, currentDirectory));
                foreach (var subDirectory in subDirectories)
                {
                    result.Remove(subDirectory);
                } 

            }

            return result;
        }
    }
}
