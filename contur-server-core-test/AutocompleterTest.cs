using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using kontur_server_core;
using System.Collections.Generic;

namespace contur_server_core_test
{
    [TestClass]
    public class AutocompleterTest
    {
        private static IAutocompleter autocompleter;

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            autocompleter = new Autocompleter();
        }

        [TestMethod]
        public void ShouldSortByFrequency()
        {
            var d = new Dictionary<string,int>();

            d.Add("ax", 10);
            d.Add("ar", 20);
            d.Add("az", 30);

            autocompleter.Init(d);

            var result = autocompleter.get("a");

            CollectionAssert.AreEqual(new string[]{"az","ar","ax"}, result);
        }

        [TestMethod]
        public void ShouldGetOnlyTen()
        {
            var d = new Dictionary<string, int>();

            d.Add("ab", 11);
            d.Add("ac", 12);
            d.Add("ad", 13); 
            d.Add("ae", 14);
            d.Add("af", 15);
            d.Add("ag", 16);
            d.Add("ah", 17);
            d.Add("ai", 18);
            d.Add("aj", 19);
            d.Add("ak", 20);
            d.Add("al", 1);

            autocompleter.Init(d);

            var result = autocompleter.get("a");

            Assert.AreEqual(10, result.Length);
            CollectionAssert.AreEqual(
                new string[] { "ak", "aj", "ai", "ah", "ag", "af", "ae", "ad", "ac", "ab"}, 
                result);
        }

        [TestMethod]
        public void ShouldSortInAlphabeticalOrder()
        {
            var d = new Dictionary<string, int>();

            d.Add("ab", 1);
            d.Add("ad", 1);
            d.Add("ac", 1);
            d.Add("aa", 1);

            autocompleter.Init(d);

            var result = autocompleter.get("a");

            CollectionAssert.AreEqual(
                new string[] { "aa", "ab", "ac", "ad"},
                result);
        }

        [TestMethod]
        public void ShouldExcludeIndexMismatch()
        {
            var d = new Dictionary<string, int>();

            d.Add("aa", 1);
            d.Add("xx", 1);
            d.Add("ab", 1);            
            d.Add("yy", 1);

            autocompleter.Init(d);

            var result = autocompleter.get("a");

            Assert.AreEqual(2, result.Length);
            CollectionAssert.AreEqual(
                new string[] { "aa", "ab"},
                result);
        }
    }
}
