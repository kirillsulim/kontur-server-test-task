using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using kontur_server_core.Protocol;
using System.IO;
using System.Collections.Generic;

namespace contur_server_core_test
{
    [TestClass]
    public class ProtocolReaderTest
    {
        

        [TestMethod]
        public void ShoudWriteAndReadIndex()
        {
            Stream stream = new MemoryStream();
            IProtocolReader reader = new NumberedProtocolReader();
            reader.Connect(stream);

            SavePosition(stream);

            reader.WriteString("someword");

            RestorePosition(stream);

            string index = reader.ReadString();

            reader.Disconnect();

            Assert.AreEqual("someword", index);
        }

        [TestMethod]
        public void ShoudWriteAndReadStringArray()
        {
            Stream stream = new MemoryStream();
            IProtocolReader reader = new NumberedProtocolReader();
            reader.Connect(stream);
            

            SavePosition(stream);

            string[] strs = new string[] { "first", "second", "third" };

            reader.WriteStringArray(strs);

            RestorePosition(stream);

            var result = reader.ReadStringArray();

            reader.Disconnect();

            CollectionAssert.AreEqual(strs, result);
        }

        private Dictionary<Stream, long> posMap = new Dictionary<Stream, long>();

        private void SavePosition(Stream s)
        {
            posMap[s] = s.Position;
        }

        private void RestorePosition(Stream s)
        {
            s.Position = posMap[s];
        }
    }
}
