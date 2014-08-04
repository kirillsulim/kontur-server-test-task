using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using kontur_server_core;
using System.IO;
using System.Text;
using System.Collections.Generic;
using kontur_server_core.DictionaryUtils;
using kontur_server_core.DictionaryElement;
using System.Linq;

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

            List<DictionaryElement> d = parser.Parse(stream).ToList();

            Assert.AreEqual(1, d.Count);
            Assert.IsTrue(d.Contains(new DictionaryElement("wordsample", 42)));
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

            List<DictionaryElement> d = parser.Parse(stream).ToList();

            Assert.AreEqual(5, d.Count);
            Assert.IsTrue(d.Contains(new DictionaryElement("word1", 12)));
            Assert.IsTrue(d.Contains(new DictionaryElement("word2", 13)));
            Assert.IsTrue(d.Contains(new DictionaryElement("someword", 22)));
            Assert.IsTrue(d.Contains(new DictionaryElement("anotherword", 33)));
            Assert.IsTrue(d.Contains(new DictionaryElement("lastword", 123)));
            
        }
    }
}
