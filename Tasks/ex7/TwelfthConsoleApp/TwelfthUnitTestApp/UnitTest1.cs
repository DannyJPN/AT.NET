using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwelfthConsoleApp;

namespace TwelfthUnitTestApp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ICOtest()
        {

            //int ICO = 75598817;
            int ICO = 04408697;
            Assert.AreEqual(Valider.Validate(ICO), false);

        }
    }
}
