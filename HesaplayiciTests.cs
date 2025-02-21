using Microsoft.VisualStudio.TestTools.UnitTesting;
using son_atik_takip;

namespace son_atik_takip.Tests
{
    [TestClass]
    public class HesaplayiciTests
    {
        [TestMethod]
        public void Topla_ReturnsCorrectSum()
        {
            int a = 3;
            int b = 5;
            int expected = 8;
            int result = Hesaplayici.Topla(a, b);
            Assert.AreEqual(expected, result, "Hesaplayici.Topla metodu beklenen sonucu vermiyor.");
        }
    }
}
