using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Common.Tries.Extended
{
    public class PrioritySparseStringTrie<TValue> 
        : PrioritySparseCharacterTrie<string, TValue> 
        where TValue : class, IComparable<TValue>
    {
        public PrioritySparseStringTrie(int count)
            : base(StringTrie<TValue>.KeyMapper, count) { }
    }
}
