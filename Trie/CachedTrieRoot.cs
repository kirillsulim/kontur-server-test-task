using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachedTrie
{
    public class CachedTrieRoot<T> : CachedTrieNode<T>
        where T : IComparable<T>
    {
        public CachedTrieRoot(int cacheSize)
            :base(0, cacheSize)
        {
        }
    }
}
