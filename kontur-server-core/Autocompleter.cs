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

        public void Init(Dictionary<string, int> d)
        {
            this.d = d;
        }

        public string[] get(string index)
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
