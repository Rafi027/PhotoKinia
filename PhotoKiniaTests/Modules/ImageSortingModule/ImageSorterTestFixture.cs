using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotoKinia.Modules.ImageSortingModule;

namespace PhotoKiniaTests.Modules.ImageSortingModule
{
    [TestClass]
    public class ImageSorterTestFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            var outputDirectory = @"C:\Photos";
            var inputDirectories = new string[] { @"D:\Directory1", @"D:\Directory2" };
            var imageSorter = new ImageSorter();
            var result = imageSorter.Sort(outputDirectory, inputDirectories);

            Assert.IsTrue(result);
        }
    }
}
