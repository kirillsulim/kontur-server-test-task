﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.DictionaryElement
{
    public class DictionaryElement
    {
        public string Word { get; private set; }
        public int Frequency { get; private set; }

        public DictionaryElement(string w, int f)
        {
            this.Word = w;
            this.Frequency = f;
        }        

        public override string ToString()
        {
            return string.Format("{{{0}:{1}}}", Word, Frequency);
        }

        /// <summary>
        /// IClonable is not recomended in API's so we create here custom function name
        /// http://msdn.microsoft.com/en-us/library/System.ICloneable(v=vs.110).aspx
        /// </summary>
        /// <returns>Deep copy of object</returns>
        public DictionaryElement DeepClone()
        {
            // String passed by ref and int passed by value so we need copy only string
            return new DictionaryElement(string.Copy(this.Word), this.Frequency);
        }

        public override int GetHashCode()
        {
            return Word.GetHashCode() ^ Frequency.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var o = obj as DictionaryElement;
            if (o == null)
                return false;
            return this.Frequency.Equals(o.Frequency) && this.Word.Equals(o.Word);
        }
    }
}
