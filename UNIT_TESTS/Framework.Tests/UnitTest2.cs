using System;
using System.Threading;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KhanyisaIntel.Kbit.Framework.Tests
{
    [TestClass]
    public class UnitTest2
    {
        private IAspNetCryptology _aspNetCryptology;

        public UnitTest2()
        {
            this._aspNetCryptology = new AspNetCryptology();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var encryptedPassword = this._aspNetCryptology.HashPassword("P@ssW0rd1");

            Assert.IsTrue(this._aspNetCryptology.VerifyHashedPassword(encryptedPassword, "P@ssW0rd1"));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsTrue(this._aspNetCryptology.VerifyHashedPassword("ACrU3bj/owsT9aBcft3ydGZZ57GtDVev7lGHs1m/jY3IunmzRSPsVK4pPzIbYDOasQ==", "P@ssW0rd1"));
        }

        [TestMethod]
        public void TestMethod3()
        {
            string adasd = $"CE{DateTime.Now.ToString("yy-MM-dd").Replace("-",string.Empty)}{DateTime.Now.ToString("T").Replace(":",string.Empty)}";

        }
    }
}
