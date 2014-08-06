using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VDS.Common.Tries;

namespace kontur_server_core.TrieAdapters
{
    public class VdsStringTrieAdapter<T> 
        : GenericVdsStringTrieAdapter<T, SparseStringTrie<T>> 
        where T: class
    {
        public VdsStringTrieAdapter()
        {
            this.root = new SparseStringTrie<T>();
        }
    }
}
