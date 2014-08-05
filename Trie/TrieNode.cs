using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    public class TrieNode<T> where T: IComparable<T>
    {
        private Dictionary<char, TrieNode<T>> children;

        private T value;

        private int level;

        private SortedSet<T> cache;
        private int cacheSize;

        protected TrieNode(int level, int cacheSize)
        {
            this.level = level;
            this.cacheSize = cacheSize;

            cache = new SortedSet<T>();
            children = new Dictionary<char, TrieNode<T>>();
        }

        public void Add(string key, T value)
        {
            cache.Add(value);
            if (cache.Count > cacheSize)
                cache.Remove(cache.Last());

            if (key.Length == level)
            {
                this.value = value;
            }
            else
            {
                char keyLevel = key[level];
                if(!children.ContainsKey(keyLevel))
                {
                    children[keyLevel] = new TrieNode<T>(this.level + 1, this.cacheSize);
                }
                children[keyLevel].Add(key, value);
            }
        }

        public IEnumerable<T> Get(string key)
        {
            if (key.Length == level)
            {
                return cache;
            }
            else
            {
                var keyLevel = key[level];
                if (children.ContainsKey(keyLevel))
                {
                    return children[keyLevel].Get(key);
                }
                else
                {
                    return new T[] { };
                }
            }
        }
    }
}
