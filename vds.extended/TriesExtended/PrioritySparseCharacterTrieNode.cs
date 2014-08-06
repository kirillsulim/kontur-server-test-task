using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDS.Common.Tries.Extended
{
    public class PrioritySparseCharacterTrieNode<TValue>
        : PrioritySparseTrieNode<char, TValue>
        where TValue : class, IComparable<TValue>
    {
        private char _singleton = '\0';
        private ITrieNode<char, TValue> _singletonNode;

        public PrioritySparseCharacterTrieNode(ITrieNode<char, TValue> parent, char key, int count)
            : base(parent, key, count) { }

        protected override bool MatchesSingleton(char key)
        {
            return key == this._singleton;
        }

        protected override void ClearSingleton()
        {
            this._singleton = '\0';
            this._singletonNode = null;
        }

        protected override ITrieNode<char, TValue> CreateNewChild(char key)
        {
            return new PrioritySparseCharacterTrieNode<TValue>(this, key, this.storedValuesCount);
        }

        protected internal override ITrieNode<char, TValue> SingletonChild
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
    }
}
