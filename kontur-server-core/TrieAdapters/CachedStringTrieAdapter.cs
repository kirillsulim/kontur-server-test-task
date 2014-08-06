using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CachedTrie;

namespace kontur_server_core.TrieAdapters
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CachedStringTrieAdapter<T> 
        : ITrieAdapter<T> 
        where T : IComparable<T>
    {
        private CachedTrie.CachedTrieRoot<T> root;

        public CachedStringTrieAdapter(int size)
        {
            root = new CachedTrieRoot<T>(size);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        public void Add(string key, T t)
        {
            root.Add(key, t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<T> Get(string key)
        {
            return root.Get(key);
        }

        public bool Sorted
        {
            get { return true; }
        }
    }
}
