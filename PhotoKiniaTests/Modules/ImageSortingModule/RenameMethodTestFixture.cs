using System;
using ImageSortingModule.Classification.RenameMethod;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoKiniaTests.Modules.ImageSortingModule
{
    [TestClass]
    public class RenameMethodTestFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            var incrementalRename = new IncrementalRename();
            var incrementedName = incrementalRename.GetNewFileName("Fooo_1.jpg");
            Assert.AreEqual("Fooo_2.jpg", incrementalRename);
            
            incrementedName = incrementalRename.GetNewFileName("Fooo_2.jpg");
            Assert.AreEqual("Fooo_3.jpg", incrementalRename);

            incrementedName = incrementalRename.GetNewFileName("Fooo_22.jpg");
            Assert.AreEqual("Fooo_23.jpg", incrementalRename);

            incrementedName = incrementalRename.GetNewFileName("Fooo_a.jpg");
            Assert.AreEqual("Fooo_a_1.jpg", incrementalRename);

            incrementedName = incrementalRename.GetNewFileName("Fooo_a_a.jpg");
            Assert.AreEqual("Fooo_a_a_1.jpg", incrementalRename);

            incrementedName = incrementalRename.GetNewFileName("Fooo_1_a.jpg");
            Assert.AreEqual("Fooo_1_a_1.jpg", incrementalRename);

            incrementedName = incrementalRename.GetNewFileName("Fooo_a1_.jpg");
            Assert.AreEqual("Fooo_a_1__1.jpg", incrementalRename);

            incrementedName = incrementalRename.GetNewFileName("Fooo_a1.jpg");
            Assert.AreEqual("Fooo_a1_1.jpg", incrementalRename);
        }
    }
}
