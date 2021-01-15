using System;
using ImageSortingModule.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoKiniaTests.Modules.ImageSortingModule
{
    [TestClass]
    public class MonthToStringTestFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            var converter = new MonthToString();
            var result = converter.Convert(1);
            Assert.AreEqual("1. Styczeń", result);

            result = converter.Convert(12);
            Assert.AreEqual("12. Grudzień", result);
            
            result = converter.Convert(13);
            Assert.IsNull(result);
        }
    }
}
