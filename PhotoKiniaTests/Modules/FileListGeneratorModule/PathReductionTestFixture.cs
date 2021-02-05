using ImageSortingModule.FileListGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PhotoKiniaTests.Modules.ImageSortingModule
{
    [TestClass]
    public class PathReductionTestFixture
    {
        [TestMethod]
        public void ReduceNestedDirectory()
        {
            var input = new List<string>
            {
                @"C:\images\m01\d01",
                @"C:\images\m01\"
            };

            var reduction = new PathReductor();
            List<string> output = reduction.Reduce(input);
            Assert.IsNotNull(output);
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual(@"C:\images\m01\", output[0]);
        }
    }
}
