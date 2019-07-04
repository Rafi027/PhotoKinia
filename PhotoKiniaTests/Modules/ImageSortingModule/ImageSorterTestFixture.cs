using System;
using System.Collections.Generic;
using System.IO;
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

            var mockFiles = new Dictionary<string, DateTime>();

            mockFiles.Add(@"D:\Pentax\dcim\100\001.jpg", new DateTime(2019, 2, 1, 10, 39, 4));
            mockFiles.Add(@"D:\Pentax\dcim\100\002.jpg", new DateTime(2019, 2, 13, 10, 39, 4));
            mockFiles.Add(@"D:\Pentax\dcim\100\003.jpg", new DateTime(2019, 2, 13, 10, 39, 4));
            mockFiles.Add(@"D:\Olympus\dcim\004.jpg", new DateTime(2019, 2, 1, 19, 20, 4));
            mockFiles.Add(@"D:\Olympus\dcim\005.jpg", new DateTime(2019, 2, 1, 10, 39, 4));

            var files = new List<string>(mockFiles.Keys).ToArray();

            fileProviderMock.Setup(mock => mock.GetFiles()).Returns<string[]>(new Func<string[], string[]>(directories =>
            {
                return new List<string>(mockFiles.Keys).ToArray();
            }));
            fileProvider = fileProviderMock.Object;

            const string StorageDirectory = @"D:\Photos\";
            IImageClassificationMethod classificationMethod = new DateTimeClassification(StorageDirectory);


            foreach (var imagePath in mockFiles.Keys)
            {

                var result = classificationMethod.GetClassifiedFilePath(imagePath);
                Assert.IsTrue(result.Success);

                var imagePathStub = "";
                if (imagePath.Contains(@"D:\Pentax\dcim\100"))
                    imagePathStub = imagePathStub.Replace(@"D:\Pentax\dcim\100", StorageDirectory);
                else
                    imagePathStub = imagePathStub.Replace(@"D:\Olympus\dcim", StorageDirectory);



                Assert.AreEqual(imagePathStub, result.ClassifiedPath);
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
