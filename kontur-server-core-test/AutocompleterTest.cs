using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using kontur_server_core;
using System.Collections.Generic;
using Moq;
using kontur_server_core.DictionaryUtils;
using kontur_server_core.Autocompleter;
using kontur_server_core.DictionaryElement;

namespace contur_server_core_test
{
    [TestClass]
    public class AutocompleterTest
    {        
        [TestMethod]
        public void ShouldSortByFrequency()
        {
            var list = new List<DictionaryElement>();
            list.Add(new DictionaryElement("ax", 10));
            list.Add(new DictionaryElement("ar", 20));
            list.Add(new DictionaryElement("az", 30));

            Mock<IDictionaryGetter> getter = new Mock<IDictionaryGetter>();
            getter.Setup(x => x.Get()).Returns(() => list);

            IAutocompleter ac = new Autocompleter(getter.Object);

            var result = ac.Get("a");

            CollectionAssert.AreEqual(new string[]{"az","ar","ax"}, result);
        }

        [TestMethod]
        public void ShouldGetOnlyTen()
        {
            var list = new List<DictionaryElement>();
            list.Add(new DictionaryElement("ab", 11));
            list.Add(new DictionaryElement("ac", 12));
            list.Add(new DictionaryElement("ad", 13)); 
            list.Add(new DictionaryElement("ae", 14));
            list.Add(new DictionaryElement("af", 15));
            list.Add(new DictionaryElement("ag", 16));
            list.Add(new DictionaryElement("ah", 17));
            list.Add(new DictionaryElement("ai", 18));
            list.Add(new DictionaryElement("aj", 19));
            list.Add(new DictionaryElement("ak", 20));
            list.Add(new DictionaryElement("al", 1));

            Mock<IDictionaryGetter> getter = new Mock<IDictionaryGetter>();
            getter.Setup(x => x.Get()).Returns(() => list);

            IAutocompleter ac = new Autocompleter(getter.Object);

            var result = ac.Get("a");

            Assert.AreEqual(10, result.Length);
            CollectionAssert.AreEqual(
                new string[] { "ak", "aj", "ai", "ah", "ag", "af", "ae", "ad", "ac", "ab"}, 
                result);
        }

        [TestMethod]
        public void ShouldSortInAlphabeticalOrder()
        {
            var d = new List<DictionaryElement>();
            d.Add(new DictionaryElement("ab", 1));
            d.Add(new DictionaryElement("ad", 1));
            d.Add(new DictionaryElement("ac", 1));
            d.Add(new DictionaryElement("aa", 1));

            Mock<IDictionaryGetter> getter = new Mock<IDictionaryGetter>();
            getter.Setup(x => x.Get()).Returns(() => d);

            IAutocompleter ac = new Autocompleter(getter.Object);

            var result = ac.Get("a");

            CollectionAssert.AreEqual(
                new string[] { "aa", "ab", "ac", "ad"},
                result);
        }

        [TestMethod]
        public void ShouldExcludeIndexMismatch()
        {
            var d = new List<DictionaryElement>();
            d.Add(new DictionaryElement("aa", 1));
            d.Add(new DictionaryElement("xx", 1));
            d.Add(new DictionaryElement("ab", 1));            
            d.Add(new DictionaryElement("yy", 1));

            Mock<IDictionaryGetter> getter = new Mock<IDictionaryGetter>();
            getter.Setup(x => x.Get()).Returns(() => d);

            IAutocompleter ac = new Autocompleter(getter.Object);

            var result = ac.Get("a");

            Assert.AreEqual(2, result.Length);
            CollectionAssert.AreEqual(
                new string[] { "aa", "ab"},
                result);
        }
    }
}
