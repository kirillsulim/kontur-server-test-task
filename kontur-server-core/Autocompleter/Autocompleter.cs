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

        /// <summary>
        /// Get dictionary using getter
        /// </summary>
        /// <param name="getter"></param>
        public Autocompleter(IDictionaryGetter getter)
        {
            d = new Dictionary<string, int>(getter.Get());
        }

        public string[] Get(string index)
        {
            // TODO: Add optimization if have some time
            var res = (from x in d
                      where x.Key.StartsWith(index)
                      orderby x.Value descending, x.Key
                      select x.Key).Take(10);

            return res.ToArray();
        }
    }
}
