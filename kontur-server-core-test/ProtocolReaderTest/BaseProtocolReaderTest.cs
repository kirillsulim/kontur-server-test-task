using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtils;
using kontur_server_core.Protocol;
using System.IO;

namespace contur_server_core_test
{
    [TestClass]
    public abstract class BaseProtocolReaderTest
    {
        private StreamPositionSaver posSaver = new StreamPositionSaver();

        private IProtocolReader reader;

        [TestInitialize]
        public void SetUpTest()
        {
            reader = GetReader();            
        }

        /// <summary>
        /// Should be overriden in ancestor
        /// </summary>
        /// <returns>New instance of concret realisation of ProtocolReader</returns>
        protected abstract IProtocolReader GetReader();

        [TestMethod]
        public void ShoudWriteAndReadString()
        {
            Stream stream = new MemoryStream();

            posSaver.SavePosition(stream);
            reader.WriteString(stream, "someword");
            posSaver.RestorePosition(stream);

            string index = reader.ReadString(stream);

            Assert.AreEqual("someword", index);
        }

        [TestMethod]
        public void ShoudWriteAndReadStringArray()
        {
            Stream stream = new MemoryStream();

            posSaver.SavePosition(stream);

            string[] strs = new string[] { "first", "second", "third" };

            reader.WriteStringArray(stream, strs);

            posSaver.RestorePosition(stream);

            var result = reader.ReadStringArray(stream);

            CollectionAssert.AreEqual(strs, result);
        }
    }
}
