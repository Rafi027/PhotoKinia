using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ImageSortingModule.Classification.EqualityCheck
{
    public class MD5Check : IImageEqualityCheck
    {
        public bool Equals(string firstFile, string secondFile)
        {
            using (var md5 = MD5.Create())
            {
                var firstHash = md5.ComputeHash(File.ReadAllBytes(firstFile));
                var secondHash = md5.ComputeHash(File.ReadAllBytes(secondFile));
                var f = BitConverter.ToString(firstHash);
                var s = BitConverter.ToString(secondHash);
                return f == s;
            }
        }
    }
}
