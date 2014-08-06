using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Common.Tries.Extended
{
    public class PrioritySparseTrieNode<TKeyBit, TValue>
        : AbstractSparseTrieNode<TKeyBit, TValue> 
        where TValue : class, IComparable<TValue>
        where TKeyBit: struct, IEquatable<TKeyBit>
    {
        /// <summary>
        /// Max count of stored values
        /// </summary>
        protected int storedValuesCount;

        /// <summary>
        /// Set to store values
        /// </summary>
        protected SortedSet<TValue> storedValues;

        public PrioritySparseTrieNode(ITrieNode<TKeyBit, TValue> parent, TKeyBit key, int count)
            :base(parent, key)
        {
            this.storedValuesCount = count;
            storedValues = new SortedSet<TValue>();
        }

        public void AddToCollection(TValue value)
        {
            storedValues.Add(value);
            if (storedValues.Count > storedValuesCount)
            {
                storedValues.Remove(storedValues.Last());
            }
        }

        private Nullable<TKeyBit> _singleton;
        private ITrieNode<TKeyBit, TValue> _singletonNode;

        /// <summary>
        /// Gets whether the given key bit matches the current singleton
        /// </summary>
        /// <param name="key">Key Bit</param>
        /// <returns>True if it matches, false otherwise</returns>
        protected override bool MatchesSingleton(TKeyBit key)
        {
            return this._singleton.HasValue && this._singleton.Value.Equals(key);
        }

        /// <summary>
        /// Clears the singleton
        /// </summary>
        protected override void ClearSingleton()
        {
            this._singleton = null;
            this._singletonNode = null;
        }

        /// <summary>
        /// Creates a new child in the trie
        /// </summary>
        /// <param name="key">Key Bit</param>
        /// <returns>New Child</returns>
        protected override ITrieNode<TKeyBit, TValue> CreateNewChild(TKeyBit key)
        {
            return new PrioritySparseTrieNode<TKeyBit, TValue>(this, key, this.storedValuesCount);
        }

        /// <summary>
        /// Gets/Sets the singleton child node
        /// </summary>
        protected internal override ITrieNode<TKeyBit, TValue> SingletonChild
        {
            get
            {
                return this._singletonNode;
            }
            set
            {
                this._singleton = value.KeyBit;
                this._singletonNode = value;
            }
        }

        /// <summary>
        /// Return cached collection
        /// </summary>
        override public IEnumerable<TValue> Values
        {
            get
            {
                return storedValues;
            }
        }
    }
}
