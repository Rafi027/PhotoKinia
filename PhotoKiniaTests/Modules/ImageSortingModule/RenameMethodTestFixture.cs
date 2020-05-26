using System;
using ImageSortingModule.Classification.RenameMethod;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoKiniaTests.Modules.ImageSortingModule
{
    [TestClass]
    public class RenameMethodTestFixture
    {
        [TestMethod]
        public void TestFileNameIncrementation()
        {
            var incrementalRename = new IncrementalRename();
            var incrementedName = incrementalRename.GetNewFileName("Fooo1.jpg");
            Assert.AreEqual("Fooo1(1).jpg", incrementedName);
            
            incrementedName = incrementalRename.GetNewFileName("Fooo1(1).jpg");
            Assert.AreEqual("Fooo1(2).jpg", incrementedName);

            incrementedName = incrementalRename.GetNewFileName("Fooo1(2).jpg");
            Assert.AreEqual("Fooo1(3).jpg", incrementedName);

            incrementedName = incrementalRename.GetNewFileName("Fooo1(9).jpg");
            Assert.AreEqual("Fooo1(10).jpg", incrementedName);

            incrementedName = incrementalRename.GetNewFileName("Fooo(_a)_1.jpg");
            Assert.AreEqual("Fooo(_a)_1(1).jpg", incrementedName);

            incrementedName = incrementalRename.GetNewFileName("Fooo(a).jpg");
            Assert.AreEqual("Fooo(a)(1).jpg", incrementedName);

            incrementedName = incrementalRename.GetNewFileName("Fooo_(1.jpg");
            Assert.AreEqual("Fooo_(1(1).jpg", incrementedName);

            incrementedName = incrementalRename.GetNewFileName("Fooo1).jpg");
            Assert.AreEqual("Fooo1)(1).jpg", incrementedName);

            incrementedName = incrementalRename.GetNewFileName("Fooo(1_1).jpg");
            Assert.AreEqual("Fooo(1_1)(1).jpg", incrementedName);

            incrementedName = incrementalRename.GetNewFileName("Fooo(11_).jpg");
            Assert.AreEqual("Fooo(11_)(1).jpg", incrementedName);

            incrementedName = incrementalRename.GetNewFileName("Fooo(11a).jpg");
            Assert.AreEqual("Fooo(11a)(1).jpg", incrementedName);
        }
    }
}
