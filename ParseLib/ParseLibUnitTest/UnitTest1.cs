using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseLib.Avito;

namespace ParseLibUnitTest
{
    [TestClass]
    public class AvitoUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void TestGetFieldAvito()
        {
            AvitoHandler aH=new AvitoHandler();
            var res=aH.GetFieldAvito("http://m.avito.ru/pskov/mebel_i_interer/stol_dlya_shkolnika_220533018");
            Assert.AreEqual(null, res);
        }
    }
}
