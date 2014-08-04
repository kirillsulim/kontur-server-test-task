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
        IEnumerable<DictionaryElement.DictionaryElement> wordList;

        public ProxyGetter(IEnumerable<DictionaryElement.DictionaryElement> list)
        {
            this.wordList = list;
        }

        public IEnumerable<DictionaryElement.DictionaryElement> Get()
        {
            return wordList;
        }
    }
}
