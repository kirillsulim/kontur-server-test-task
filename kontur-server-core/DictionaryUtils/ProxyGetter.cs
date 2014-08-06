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
        IEnumerable<DictionaryElement> wordList;

        public ProxyGetter(IEnumerable<DictionaryElement> list)
        {
            this.wordList = list;
        }

        public IEnumerable<DictionaryElement> Get()
        {
            return wordList;
        }
    }
}
