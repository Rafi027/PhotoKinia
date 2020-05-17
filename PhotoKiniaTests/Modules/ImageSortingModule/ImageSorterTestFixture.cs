using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoKinia.Modules.ImageSortingModule;
using System.Linq;

namespace PhotoKiniaTests.Modules.ImageSortingModule
{
    [TestClass]
    public class ImageSorterTestFixture
    {
        private IFileListGenerator fileProvider;

        [TestMethod]
        public void ImageClassificationByDate()
        {
            const string StorageDirectory = @"D:\Photos\";
            var fileProviderMock = new Mock<IFileListGenerator>();
            var mockFilesDictionary = new Dictionary<string, string>();
            mockFilesDictionary.Add(@"D:\Pentax\dcim\001.jpg", @"C:\PhotoLibrary");

            var mockFiles = new Dictionary<string, DateTime>();
            mockFiles.Add(@"D:\Pentax\dcim\100\001.jpg", new DateTime(2019, 1, 1, 10, 39, 4));
            mockFiles.Add(@"D:\Pentax\dcim\100\002.jpg", new DateTime(2019, 2, 13, 10, 39, 4));
            mockFiles.Add(@"D:\Pentax\dcim\100\003.jpg", new DateTime(2019, 2, 13, 10, 39, 4));
            mockFiles.Add(@"D:\Olympus\dcim\004.jpg", new DateTime(2019, 2, 1, 19, 20, 4));
            mockFiles.Add(@"D:\Olympus\dcim\005.jpg", new DateTime(2019, 2, 1, 10, 39, 4));

            var mockResults = new Dictionary<string, string>();
            mockResults.Add(@"D:\Pentax\dcim\100\001.jpg", Path.Combine(StorageDirectory, @"2019\1. Styczeń\1\001.jpg"));
            mockResults.Add(@"D:\Pentax\dcim\100\002.jpg", Path.Combine(StorageDirectory, @"2019\2. Luty\13\002.jpg"));
            mockResults.Add(@"D:\Pentax\dcim\100\003.jpg", Path.Combine(StorageDirectory, @"2019\2. Luty\13\003.jpg"));
            mockResults.Add(@"D:\Olympus\dcim\004.jpg", Path.Combine(StorageDirectory, @"2019\2. Luty\1\004.jpg"));
            mockResults.Add(@"D:\Olympus\dcim\005.jpg", Path.Combine(StorageDirectory, @"2019\2. Luty\1\005.jpg"));



            var files = new List<string>(mockFiles.Keys).ToArray();
            fileProviderMock.Setup(mock => mock.GetFiles()).Returns(mockFiles.Keys.ToArray());
            fileProvider = fileProviderMock.Object;

            var dateReader = new Mock<IImageCreationDateReader>();
            foreach (var file in mockFiles.Keys)
                dateReader.Setup(reader => reader.Read(file)).Returns(mockFiles[file]);


            IImageClassificationMethod classificationMethod = new DateTimeClassification(StorageDirectory, dateReader.Object);


            foreach (var imagePath in mockFiles.Keys)
            {

                var result = classificationMethod.GetClassifiedFilePath(imagePath);
                Assert.IsTrue(result.Success);

                Assert.AreEqual(mockResults[imagePath], result.ClassifiedPath);
            }
        }

    }
}
