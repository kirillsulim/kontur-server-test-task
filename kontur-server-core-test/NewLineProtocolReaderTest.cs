using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using kontur_server_core.Protocol;
using System.Collections.Generic;

namespace contur_server_core_test
{
    [TestClass]
    public class NewLineProtocolReaderTest
    {
        [TestMethod]
        public void ShouldWriteAndReadString()
        {
            Stream stream = new MemoryStream();
            IProtocolReader reader = new NewLineProtocolReader();

            SavePosition(stream);

            reader.WriteString(stream, "someword");

            RestorePosition(stream);

            string index = reader.ReadString(stream);

            Assert.AreEqual("someword", index);
        }

        [TestMethod]
        public void ShoudWriteAndReadStringArray()
        {
            Stream stream = new MemoryStream();
            IProtocolReader reader = new NewLineProtocolReader();


            SavePosition(stream);

            string[] strs = new string[] { "first", "second", "third" };

            reader.WriteStringArray(stream, strs);

            RestorePosition(stream);

            var result = reader.ReadStringArray(stream);

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
