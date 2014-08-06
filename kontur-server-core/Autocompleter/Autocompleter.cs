using kontur_server_core.DictionaryUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kontur_server_core.TrieAdapters;

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

        ITrieAdapter<DictionaryElement> trie;

        /// <summary>
        /// Get dictionary using getter
        /// </summary>
        /// <param name="getter"></param>
        public Autocompleter(IDictionaryGetter getter, ITrieAdapter<DictionaryElement> trie, int count = 10)
        {
            this.takeCount = count;

            this.trie = trie;

            cache1Letter = new Dictionary<string, string[]>();
            cache2Letters = new Dictionary<string, string[]>();

            InitTrie(getter.Get());
        }

        private void InitTrie(IEnumerable<DictionaryElement> dictionary)
        {
            foreach (var el in dictionary)
            {
                trie.Add(el.Word, el.DeepClone());
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
            if (trie.Sorted)
            {
                return trie.Get(index).Select(de => de.Word).ToArray();
            }
            else
            {
                return trie.Get(index)
                    .OrderByDescending(de => de.Frequency)
                    .ThenBy(de => de.Word)
                    .Take(takeCount)
                    .Select(de => de.Word)
                    .ToArray();
            }
        }
    }
}
