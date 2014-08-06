using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Common.Tries.Extended
{
    /// <summary>
    /// Sparse trie with cache. Optimized for get first elements sorted by some priority
    /// </summary>
    /// <typeparam name="TKey">Type of keys</typeparam>
    /// <typeparam name="TKeyBit">Type of key bits</typeparam>
    /// <typeparam name="TValue">Type of values</typeparam>
    public class PrioritySparseTrie<TKey, TKeyBit, TValue> 
        : AbstractTrie<TKey, TKeyBit, TValue> 
        where TValue : class,  IComparable<TValue>
        where TKeyBit: struct, IEquatable<TKeyBit>
    {
        /// <summary>
        /// Max count of stored values
        /// </summary>
        protected int storedValuesCount;

        public PrioritySparseTrie(Func<TKey, IEnumerable<TKeyBit>> keyMapper, int count) 
            : base(keyMapper)
        {
            this.storedValuesCount = count;
            this._root = CreateRoot(default(TKeyBit)); //Reinit hack
        }

        protected override ITrieNode<TKeyBit, TValue> CreateRoot(TKeyBit key)
        {
            return new PrioritySparseTrieNode<TKeyBit, TValue>(null, key, this.storedValuesCount);
        }

        /// <summary>
        /// Adds a new key value pair, overwriting the existing value if the given key is already in use
        /// </summary>
        /// <param name="key">Key to search for value by</param>
        /// <param name="value">Value associated with key</param>
        override public void Add(TKey key, TValue value)
        {
            PrioritySparseTrieNode<TKeyBit, TValue> node = 
                _root as PrioritySparseTrieNode<TKeyBit, TValue>;
            IEnumerable<TKeyBit> bs = this._keyMapper(key);
            foreach (TKeyBit b in bs)
            {
                node = node.MoveToChild(b) as PrioritySparseTrieNode<TKeyBit, TValue>;
                node.AddToCollection(value);
            }
            node.Value = value;
        }

        /// <summary>
        /// Not implemendet for current project
        /// </summary>
        /// <param name="key">Key of removed value</param>
        override public void Remove(TKey key)
        {
            throw new NotImplementedException("Remove is not implemented.");
        }
    }
}
