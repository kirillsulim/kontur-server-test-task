using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using kontur_server_core;

namespace contur_server_core_test
{
    [TestClass]
    public class DictionaryElementTest
    {
        [TestMethod]
        public void ShouldConvertToString()
        {
            DictionaryElement el = new DictionaryElement("text", 42);

            StringAssert.Equals("{text:42}", el.ToString());
        }
    }
}
