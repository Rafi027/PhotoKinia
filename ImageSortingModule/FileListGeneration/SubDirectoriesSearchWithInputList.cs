using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSortingModule.FileListGeneration
{
    public class SubDirectoriesSearchWithInputList : SubDirectoriesSearchBase
    {
        private readonly List<string> directoriesToSearch;

        public SubDirectoriesSearchWithInputList(List<string> directoriesToSearch)
        {
            this.directoriesToSearch = directoriesToSearch;
        }
        protected override List<string> GetDirectoriesToSearch()
        {
            return directoriesToSearch;
        }
    }
}
