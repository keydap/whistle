using NUnit.Framework;
using System;

namespace whistle
{
    [TestFixture()]
    public class TokenTest
    {
        [Test()]
        public void TestCase()
        {
            Token t = Token.createFromTotpUrl("otpauth://totp/Example:alice@google.com?secret=JBSWY3DPEHPK3PXP&issuer=Example");
            Assert.IsNull(t);
        }
    }
}
