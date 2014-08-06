using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Common.Tries.Extended
{
    public class PrioritySparseCharacterTrie<TKey, TValue> 
        : PrioritySparseTrie<TKey, char, TValue>
        where TValue : class, IComparable<TValue>
    {
        public PrioritySparseCharacterTrie(Func<TKey, IEnumerable<char>> keyMapper, int count)
            : base(keyMapper, count) { }

        protected override ITrieNode<char, TValue> CreateRoot(char key)
        {
            return new PrioritySparseCharacterTrieNode<TValue>(null, key, this.storedValuesCount);
        }
    }
}
