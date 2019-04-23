using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mrk.wordyformatprovider.test
{
    [TestClass]
    public class WordyFormatProviderITATest
    {
        [TestMethod]
        public void NumberIsConvertedCorrectly()
        {
            var f = WordyFormatProvider.Create("it-IT");
            var s = string.Format(f, "{0:W}", 5);
            Assert.AreEqual("cinque", s);
        }

        [TestMethod]
        public void NumberIsConvertedCorrectly_2()
        {
            var f = WordyFormatProvider.Create("it-IT");
            var s = string.Format(f, "{0:W}", 116);
            Assert.AreEqual("centosedici", s);
        }

        [TestMethod]
        public void NumberIsConvertedCorrectly_3()
        {
            var f = WordyFormatProvider.Create("it-IT");
            var s = string.Format(f, "{0:W}", 1513);
            Assert.AreEqual("millecinquecentotredici", s);
        }


        [TestMethod]
        public void NumberIsConvertedCorrectly_4()
        {
            var f = WordyFormatProvider.Create("it-IT");
            var s = string.Format(f, "{0:W}", 0);
            Assert.AreEqual("zero", s);
        }

        // NOT SUPPORTED YET
        //[TestMethod]
        //public void NumberIsConvertedCorrectly_5()
        //{
        //    var f = WordyFormatProvider.Create("it-IT");
        //    var s = string.Format(f, "{0:W}", -1);
        //    Assert.AreEqual("-uno", s);
        //}
    }
}
