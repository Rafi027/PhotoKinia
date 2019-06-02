using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoKinia.Modules.ImageSortingModule;

namespace PhotoKiniaTests.Modules.ImageSortingModule
{
    [TestClass]
    public class ImageSorterTestFixture
    {
        private IFileListGenerator fileProvider;

        [TestInitialize]
        public void Init()
        {
            var fileProviderMock = new Mock<IFileListGenerator>();
            var mockFilesDictionary = new Dictionary<string, string>();
            mockFilesDictionary.Add(@"D:\Pentax\dcim\001.jpg", @"C:\PhotoLibrary");

            var mockFiles = new string[]
            {
                @"D:\Pentax\dcim\001.jpg",
                @"D:\Pentax\dcim\002.jpg",
                @"D:\Pentax\dcim\003.jpg",
            
                @"D:\Canon\dcim\001.jpg",
            };

            fileProviderMock.Setup(mock => mock.GetFiles()).Returns<string[]>(new Func<string[], string[]>(directories =>
            {

                return mockFiles;
            }));
            fileProvider = fileProviderMock.Object;

            var sortMethodMock = new Mock<IImageClassificationMethod>();
            foreach (var mockFile in mockFiles)
            {
                sortMethodMock.Setup(mock => mock.GetClassifiedFilePath(mockFile)).Returns<string>(new Func<string, string>(input =>
                {
                    
                    return "";
                }));


            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var outputDirectory = @"C:\Photos";
            var inputDirectories = new string[] { @"D:\Directory1", @"D:\Directory2" };
            

            
            var imageSorter = new ImageSorter(fileProvider);
            var result = imageSorter.Sort(outputDirectory, inputDirectories);

            Assert.IsTrue(result);
        }
    }
}
