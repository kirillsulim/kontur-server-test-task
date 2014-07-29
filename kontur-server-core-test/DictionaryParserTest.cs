using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using kontur_server_core;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace contur_server_core_test
{
    [TestClass]
    public class DictionaryParserTest
    {
        private static IDictionaryParser parser;
        private static Encoding utf8 = Encoding.UTF8;

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            parser = new DictionaryParser();
        }

        [TestMethod]
        public void ShouldParseOneString()
        {
            Stream stream = new MemoryStream();

            byte[] sample = utf8.GetBytes("wordsample 42");

            stream.Write(sample, 0, sample.Length);
            stream.Position = 0;

            Dictionary<string, int> d = parser.Parse(stream);

            Assert.AreEqual(1, d.Count);
            Assert.IsTrue(d.ContainsKey("wordsample"));
            Assert.AreEqual(42, d["wordsample"]);
        }

        [TestMethod]
        public void ShouldParseManyString()
        {
            Stream stream = new MemoryStream();

            string words = 
@"word1 12
word2 13
someword 22
anotherword 33
lastword 123";

            byte[] sample = utf8.GetBytes(words);

            stream.Write(sample, 0, sample.Length);
            stream.Position = 0;

            Dictionary<string, int> d = parser.Parse(stream);

            Assert.AreEqual(5, d.Count);
            Assert.IsTrue(d.ContainsKey("word1"));
            Assert.IsTrue(d.ContainsKey("word2"));
            Assert.IsTrue(d.ContainsKey("someword"));
            Assert.IsTrue(d.ContainsKey("anotherword"));
            Assert.IsTrue(d.ContainsKey("lastword"));
            Assert.AreEqual(12, d["word1"]);
            Assert.AreEqual(13, d["word2"]);
            Assert.AreEqual(22, d["someword"]);
            Assert.AreEqual(33, d["anotherword"]);
            Assert.AreEqual(123, d["lastword"]);
        }
    }
}
