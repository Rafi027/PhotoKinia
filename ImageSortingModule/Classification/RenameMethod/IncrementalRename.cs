using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ImageSortingModule.Classification.RenameMethod
{
    internal class IncrementalRename
    {
        public string GetNewFileName(string fileName)
        {
            var regex = new Regex(@"\(\d+\)$");
            var extension = Path.GetExtension(fileName);
            var noExtensionName = Path.GetFileNameWithoutExtension(fileName);
            var matchResult = regex.Match(noExtensionName);
            if (matchResult.Success)
                return IncrementedName(noExtensionName, extension);
            return noExtensionName + "(1)" + extension;
        }

        private string IncrementedName(string noExtensionName, string extension)
        {
            var regex = new Regex(@"\(\d+\)$");
            var splittedName = regex.Split(noExtensionName);
            var match = regex.Match(noExtensionName);
            int id = GetID(match.Value);
            return $"{noExtensionName.Remove(match.Index, noExtensionName.Length - match.Index)}({++id}){extension}";
        }

        private int GetID(string value)
        {
            var textNumber = value.Replace("(", "").Replace(")", "");
            if (int.TryParse(textNumber, out int id))
                return id;
            throw new InvalidOperationException();
        }
    }
}
