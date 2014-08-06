using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VDS.Common.Tries.Extended;
using VDS.Common.Tries;

namespace kontur_server_core.TrieAdapters
{
    public class VdsExtendedStringTrieAdapter<T> 
        : GenericVdsStringTrieAdapter<T, PrioritySparseStringTrie<T>>
        where T : class, IComparable<T>
    {
        public VdsExtendedStringTrieAdapter(int count)
        {
            root = new PrioritySparseStringTrie<T>(count);
        }

        override public bool Sorted
        {
            get { return true; }
        }
        
    }
}
