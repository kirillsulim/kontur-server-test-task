using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.DictionaryElement
{
    public class DictionaryElement
    {
        public DictionaryElement(string w, int f)
        {
            this.Word = w;
            this.Frequency = f;
        }

        public string Word {get; private set;}
        public int Frequency { get; private set; }

        public override string ToString()
        {
            return string.Format("{{{0}:{1}}}", Word, Frequency);
        }
    }
}
