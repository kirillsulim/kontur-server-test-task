using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.DictionaryUtils
{
    /// <summary>
    /// Gets dictionary in constructor
    /// Returns this dictionary on call Get
    /// </summary>
    public class ProxyGetter : IDictionaryGetter
    {
        private Dictionary<string, int> d;

        public ProxyGetter(Dictionary<string, int> d)
        {
            this.d = d;
        }

        public Dictionary<string, int> Get()
        {
            return d;
        }
    }
}
