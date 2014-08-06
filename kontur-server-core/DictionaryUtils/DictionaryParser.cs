using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core.DictionaryUtils
{
    /// <summary>
    /// DictionaryParser realisation
    /// </summary>
    public class DictionaryParser : IDictionaryParser
    {
        public IEnumerable<DictionaryElement> Parse(Stream s)
        {
            var list = new List<DictionaryElement>();

            StreamReader reader = new StreamReader(s, Encoding.UTF8);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] pair = line.Split(' ');

                // Skip all short
                if (pair.Length < 2)
                    continue;
                
                // And lines without numbers
                int count;
                if (!int.TryParse(pair[1], out count))
                    continue;

                string word = pair[0];
                
                list.Add(new DictionaryElement(word, count));
            }

            return list;
        }
    }
}
