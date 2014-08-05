using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    public class TrieRoot<T> : TrieNode<T>
        where T : IComparable<T>
    {
        public TrieRoot(int cacheSize)
            :base(0, cacheSize)
        {
        }
    }
}
