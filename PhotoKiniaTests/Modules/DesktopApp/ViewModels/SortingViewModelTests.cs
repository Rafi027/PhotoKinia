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

        [TestMethod]
        public void TestAddDirectoryMethod()
        {
            var directorySelectorMock = new Mock<IDirectorySelector>();
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1)
                .Returns(TestPath2)
                .Returns(TestPath3);

            var ViewModel = new SortingViewModel(directorySelectorMock.Object);
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
            var directorySelectorMock = new Mock<IDirectorySelector>();
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1)
                .Returns(TestPath1)
                .Returns(TestPath1);

            var ViewModel = new SortingViewModel(directorySelectorMock.Object);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);



            Assert.AreEqual(1, ViewModel.InputDirectories.Count);
            Assert.AreEqual(TestPath1, ViewModel.InputDirectories[0]);
        }

        [TestMethod]
        public void CheckIfUserCanRemoveDirectory()
        {
            var directorySelectorMock = new Mock<IDirectorySelector>();
            directorySelectorMock.SetupSequence(d => d.SelectDirectory())
                .Returns(TestPath1)
                .Returns(TestPath2)
                .Returns(TestPath3);

            var ViewModel = new SortingViewModel(directorySelectorMock.Object);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);
            ViewModel.AddDirectory.Execute(null);

            ViewModel.SelectedDirectory = ViewModel.InputDirectories[1];

            ViewModel.RemoveDirectory.Execute(null);

            Assert.AreEqual(2, ViewModel.InputDirectories.Count);
            Assert.AreEqual(TestPath1, ViewModel.InputDirectories[0]);
            Assert.AreEqual(TestPath3, ViewModel.InputDirectories[1]);
        }
    }
}
