using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VDS.Common.Tries;

namespace kontur_server_core.TrieAdapters
{
    public abstract class GenericVdsStringTrieAdapter<T, VDSRealisation> 
        : ITrieAdapter<T>
        where T : class
        where VDSRealisation : class, ITrie<string, char, T>
    {
        protected ITrie<string, char, T> root;

        public void Add(string key, T t)
        {
            root.Add(key, t);
        }

        public IEnumerable<T> Get(string key)
        {
            var node = root.Find(key);
            if (node == null)
            {
                return new T[] { };
            }
            else
            {
                return node.Values.ToArray();
            }
        }

        virtual public bool Sorted
        {
            get { return false; }
        }
    }
}
