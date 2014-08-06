using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.TrieAdapters
{
    public interface ITrieAdapter<T>
    {
        /// <summary>
        /// Add new value to trie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="t"></param>
        void Add(string key, T t);

        /// <summary>
        /// Get values matches key. 
        /// Values order and takeCount depends on concrete realisation
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>array of values</returns>
        IEnumerable<T> Get(string key);

        /// <summary>
        /// 
        /// </summary>
        bool Sorted { get;}
    }
}
