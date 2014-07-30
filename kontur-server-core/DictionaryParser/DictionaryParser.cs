using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core
{
    /// <summary>
    /// DictionaryParser realisation
    /// </summary>
    public class DictionaryParser : IDictionaryParser
    {
        public Dictionary<string, int> Parse(Stream s)
        {
            var d = new Dictionary<string, int>();

            StreamReader reader = new StreamReader(s, Encoding.UTF8);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] pair = line.Split(' ');

                string word = pair[0];
                int count = int.Parse(pair[1]);

                d.Add(word, count);
            }

            return d;
        }
    }
}
