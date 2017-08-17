using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            var encryptedPassword = Crypto.HashPassword("P@ssW0rd1");

            Assert.IsTrue(Crypto.VerifyHashedPassword(encryptedPassword, "P@ssW0rd1"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsTrue(Crypto.VerifyHashedPassword("ACrU3bj/owsT9aBcft3ydGZZ57GtDVev7lGHs1m/jY3IunmzRSPsVK4pPzIbYDOasQ==", "P@ssW0rd1"));
        }
    }
}
