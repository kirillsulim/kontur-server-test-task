using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.CommonReference
{
    /// <summary>
    /// In some cases (like in interface ITrie) you can use only reference type.
    /// But Int32 is not reference type so you need to wrap it
    /// </summary>
    public class CommonReference<T>
    {
        public T Value { get; set; }

        public CommonReference(T t)
        {
            this.Value = t;
        }

        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
