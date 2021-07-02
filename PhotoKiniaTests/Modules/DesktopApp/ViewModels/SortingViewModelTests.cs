using ImageSortingModule;
using ImageSortingModule.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoKinia.Contracts;
using PhotoKinia.Modules.ImageSortingModule;
using PhotoKinia.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        private Mock<IFileListGenerator> subDirectoriesSearchMock;
        private Mock<IDialogHostWrapper> progressControlHostWrapperMock;

        [TestInitialize]
        public void Init()
        {
            imageSortingMock = new Mock<IImageSorter>();
            directorySelectorMock = new Mock<IDirectorySelector>();
            subDirectoriesSearchMock = new Mock<IFileListGenerator>();
            subDirectoriesSearchMock.Setup(s => s.GetFiles(It.Is<IEnumerable<string>>(input => input.Count() > 0)))
                .Returns(new List<string>() { Path.Combine(TestPath1, "img1.jpg"), Path.Combine(TestPath2, "img2.jpg")});
            progressControlHostWrapperMock = new Mock<IDialogHostWrapper>();
            progressControlHostWrapperMock.Setup(p => p.ShowAsync(It.IsAny<ViewModelBase>(), It.IsAny<string>(), null, null));
        }

        [TestMethod]
        public void TestAddDirectoryMethod()
        {
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1)
                .Returns(TestPath2)
                .Returns(TestPath3);

            var ViewModel = new SortingViewModel(subDirectoriesSearchMock.Object, imageSortingMock.Object, directorySelectorMock.Object, progressControlHostWrapperMock.Object);
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

            var ViewModel = new SortingViewModel(subDirectoriesSearchMock.Object, imageSortingMock.Object, directorySelectorMock.Object, progressControlHostWrapperMock.Object);
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

            var ViewModel = new SortingViewModel(subDirectoriesSearchMock.Object, imageSortingMock.Object, directorySelectorMock.Object, progressControlHostWrapperMock.Object);
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

            var ViewModel = new SortingViewModel(subDirectoriesSearchMock.Object, imageSortingMock.Object, directorySelectorMock.Object, progressControlHostWrapperMock.Object);
            var canExecuteProcessing = ViewModel.RunProcessing.CanExecute(null);
            Assert.IsFalse(canExecuteProcessing);
        }

        [TestMethod]
        public void CheckIfUserCanRunProcessingWithDirectories()
        {
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1)
                .Returns(TestOutputPath);

            var ViewModel = new SortingViewModel(subDirectoriesSearchMock.Object, imageSortingMock.Object, directorySelectorMock.Object, progressControlHostWrapperMock.Object);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.SelectOutputDirectory.Execute(null);
            var canExecuteProcessing = ViewModel.RunProcessing.CanExecute(null);
            Assert.IsTrue(canExecuteProcessing);
        }

        [TestMethod]
        public void CheckIfSortingWasCalledAfterClickingRunProcessingButton()
        {
            int calls = 0;
            imageSortingMock.Setup(
                i => i.Sort(
                    It.Is<IEnumerable<string>>(input => input.Count() > 0), 
                    It.Is<string>(output => !string.IsNullOrEmpty(output)),
                    It.Is<IFileOperation>(operation => operation != null)))
                .Callback(() => calls++);

            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1);


            var ViewModel = new SortingViewModel(subDirectoriesSearchMock.Object, imageSortingMock.Object, directorySelectorMock.Object, progressControlHostWrapperMock.Object);
            ViewModel.OutputDirectory = TestOutputPath;
            ViewModel.AddDirectory.Execute(null);
            ViewModel.RunProcessing.Execute(null);
            Assert.AreEqual(1, calls);
        }

        [TestMethod]
        public void CheckFileCopyModeProcessingSelection()
        {
            int calls = 0;
            imageSortingMock.Setup(
                i => i.Sort(
                    It.Is<IEnumerable<string>>(input => input.Count() > 0),
                    It.Is<string>(output => !string.IsNullOrEmpty(output)),
                    It.Is<IFileOperation>(operation => operation is FileCopyOperation)))
                .Callback(() => calls++);

            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1);


            var ViewModel = new SortingViewModel(subDirectoriesSearchMock.Object, imageSortingMock.Object, directorySelectorMock.Object, progressControlHostWrapperMock.Object);
            ViewModel.OutputDirectory = TestOutputPath;
            ViewModel.FileMode = PhotoKinia.Models.FileOperationMode.Copy;
            ViewModel.AddDirectory.Execute(null);
            ViewModel.RunProcessing.Execute(null);
            Assert.AreEqual(1, calls);
        }

        [TestMethod]
        public void CheckFileMoveModeProcessingSelection()
        {
            int calls = 0;
            imageSortingMock.Setup(
                i => i.Sort(
                    It.Is<IEnumerable<string>>(input => input.Count() > 0),
                    It.Is<string>(output => !string.IsNullOrEmpty(output)),
                    It.Is<IFileOperation>(operation => operation is FileMoveOperation)))
                .Callback(() => calls++);

            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1);


            var ViewModel = new SortingViewModel(subDirectoriesSearchMock.Object, imageSortingMock.Object, directorySelectorMock.Object, progressControlHostWrapperMock.Object);
            ViewModel.OutputDirectory = TestOutputPath;
            ViewModel.FileMode = PhotoKinia.Models.FileOperationMode.Move;
            ViewModel.AddDirectory.Execute(null);
            ViewModel.RunProcessing.Execute(null);
            Assert.AreEqual(1, calls);
        }
    }
}
