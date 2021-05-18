using ImageSortingModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoKinia.Contracts;
using PhotoKinia.ViewModels;
using System;

namespace PhotoKiniaTests.Modules.DesktopApp.ViewModels
{
    [TestClass]
    public class SortingViewModelTests
    {
        private const string TestPath1 = @"C:\images1";
        private const string TestPath2 = @"C:\images2";
        private const string TestPath3 = @"C:\images3";
        private const string TestOutputPath = @"C:\ImagesLibrary";
        
        private Mock<IImageSorter> imageSortingMock;
        private Mock<IDirectorySelector> directorySelectorMock;

        [TestInitialize]
        public void Init()
        {
            imageSortingMock = new Mock<IImageSorter>();
            directorySelectorMock = new Mock<IDirectorySelector>();
        }

        [TestMethod]
        public void TestAddDirectoryMethod()
        {
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1)
                .Returns(TestPath2)
                .Returns(TestPath3);

            var ViewModel = new SortingViewModel(imageSortingMock.Object, directorySelectorMock.Object);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);

            Assert.AreEqual(3, ViewModel.InputDirectories.Count);
            Assert.AreEqual(TestPath1, ViewModel.InputDirectories[0]);
            Assert.AreEqual(TestPath2, ViewModel.InputDirectories[1]);
            Assert.AreEqual(TestPath3, ViewModel.InputDirectories[2]);
        }

        [TestMethod]
        public void CheckIfUserCanAddTheSameDirectory()
        {
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1)
                .Returns(TestPath1)
                .Returns(TestPath1);

            var ViewModel = new SortingViewModel(imageSortingMock.Object, directorySelectorMock.Object);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);



            Assert.AreEqual(1, ViewModel.InputDirectories.Count);
            Assert.AreEqual(TestPath1, ViewModel.InputDirectories[0]);
        }

        [TestMethod]
        public void CheckIfUserCanRemoveDirectory()
        {
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1)
                .Returns(TestPath2)
                .Returns(TestPath3);

            var ViewModel = new SortingViewModel(imageSortingMock.Object, directorySelectorMock.Object);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);

            ViewModel.SelectedDirectory = ViewModel.InputDirectories[1];

            ViewModel.RemoveDirectory.Execute(null);

            Assert.AreEqual(2, ViewModel.InputDirectories.Count);
            Assert.AreEqual(TestPath1, ViewModel.InputDirectories[0]);
            Assert.AreEqual(TestPath3, ViewModel.InputDirectories[1]);
        }

        [TestMethod]
        public void CheckIfUserCanRunProcessingWithoutAnyDirectories()
        {
            var directorySelectorMock = new Mock<IDirectorySelector>();

            var ViewModel = new SortingViewModel(imageSortingMock.Object, directorySelectorMock.Object);
            var canExecuteProcessing = ViewModel.RunProcessing.CanExecute(null);
            Assert.IsFalse(canExecuteProcessing);
        }

        [TestMethod]
        public void CheckIfUserCanRunProcessingWithDirectories()
        {
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1)
                .Returns(TestOutputPath);

            var ViewModel = new SortingViewModel(imageSortingMock.Object, directorySelectorMock.Object);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.SelectOutputDirectory.Execute(null);
            var canExecuteProcessing = ViewModel.RunProcessing.CanExecute(null);
            Assert.IsTrue(canExecuteProcessing);
        }

        [TestMethod]
        public void RunProcessing()
        {
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1);

            var ViewModel = new SortingViewModel(imageSortingMock.Object, directorySelectorMock.Object);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.RunProcessing.Execute(null);
        }
    }
}
