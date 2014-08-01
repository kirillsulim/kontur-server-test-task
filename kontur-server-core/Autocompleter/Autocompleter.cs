using kontur_server_core.DictionaryUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core
{
    /// <summary>
    /// Autocompleter realisation
    /// </summary>
    public class Autocompleter : IAutocompleter
    {
        private Dictionary<string, int> d;

        private Dictionary<string, string[]> cache1Letter;
        private Dictionary<string, string[]> cache2Letters;

        /// <summary>
        /// Get dictionary using getter
        /// </summary>
        /// <param name="getter"></param>
        public Autocompleter(IDictionaryGetter getter)
        {
            d = new Dictionary<string, int>(getter.Get());

            cache1Letter = new Dictionary<string, string[]>();
            cache2Letters = new Dictionary<string, string[]>();
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
            var res = (from x in d
                       where x.Key.StartsWith(index)
                       orderby x.Value descending, x.Key
                       select x.Key).Take(10);

            return res.ToArray();
        }
    }
}
