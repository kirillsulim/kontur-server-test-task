using kontur_server_core.DictionaryUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.Common.Tries;
using kontur_server_core.DictionaryElement;

namespace kontur_server_core.Autocompleter
{
    /// <summary>
    /// Autocompleter realisation
    /// </summary>
    public class Autocompleter : IAutocompleter
    {
        private Dictionary<string, string[]> cache1Letter;
        private Dictionary<string, string[]> cache2Letters;

        private int takeCount;

        ITrie<string, char, DictionaryElement.DictionaryElement> trie;

        /// <summary>
        /// Get dictionary using getter
        /// </summary>
        /// <param name="getter"></param>
        public Autocompleter(IDictionaryGetter getter, int count = 10)
        {
            this.takeCount = count;

            trie = new SparseStringTrie<DictionaryElement.DictionaryElement>();             

            cache1Letter = new Dictionary<string, string[]>();
            cache2Letters = new Dictionary<string, string[]>();

            InitTrie(getter.Get());
        }

        private void InitTrie(Dictionary<string, int> dictionary)
        {
            foreach (var el in dictionary)
            {
                this.trie.Add(el.Key, new DictionaryElement.DictionaryElement(el.Key, el.Value));
            }
        }

        public string[] Get(string index)
        {
            if (index.Length == 1)
                return GetFrom1Cache(index);
            if (index.Length == 2)
                return GetFrom2Cache(index);

            return GetFromDictionary(index);
        }

        private string[] GetFrom1Cache(string index)
        {
            if (!cache1Letter.ContainsKey(index))
                cache1Letter[index] = GetFromDictionary(index);
            return cache1Letter[index];
        }

        private string[] GetFrom2Cache(string index)
        {
            if (!cache2Letters.ContainsKey(index))
                cache2Letters[index] = GetFromDictionary(index);
            return cache2Letters[index];
        }

        private string[] GetFromDictionary(string index)
        {
            var node = trie.Find(index);
            if (node != null)
            {
                return node.Values
                    .OrderByDescending(x => x.Frequency)
                    .ThenBy(x => x.Word)
                    .Take(takeCount)
                    .Select(x => x.Word)
                    .ToArray();
            }
            else
            {
                return new string[]{};
            }
        }
    }
}
