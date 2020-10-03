using System;
using ImageSortingModule.Utils.RecursionHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PhotoKiniaTests.Modules.ImageSortingModule
{
    [TestClass]
    public class TrampolineTestFixture
    {
        [TestMethod]
        public void TestTrampolineRecursion()
        {
            var factorial = Trampoline.Start(Iteration, 1, 2);
            Assert.AreEqual(2, factorial);

            factorial = Trampoline.Start(Iteration, 1, 3);
            Assert.AreEqual(6, factorial);

            factorial = Trampoline.Start(Iteration, 1, 4);
            Assert.AreNotEqual(23, factorial);
        }

        private Bounce<int, int, int> Iteration(int currentValue, int n)
        {
            return n == 0 ? Bounce<int, int, int>.End(currentValue) :
                Bounce<int, int, int>.Continue(currentValue * n, n - 1);
        }
    }
}
