using kontur_server_core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_app
{
    /// <summary>
    /// Simple applicatiom implementation
    /// </summary>
    class SimpleApplication : ISimpleApplication
    {
        private IDictionaryParser parser;

        private IAutocompleter autocompleter;

        public SimpleApplication(IDictionaryParser parser, IAutocompleter autocompleter)
        {
            this.parser = parser;
            this.autocompleter = autocompleter;
        }

        public void Run(TextReader cin, TextWriter cout)
        {
            using (Stream stream = new MemoryStream())
            {
                // Get dictioary word count
                int wordsCount = int.Parse(cin.ReadLine().Trim());

                // Fill dictionary stream
                for (int i = 0; i < wordsCount; ++i)
                {
                    byte[] line = Encoding.UTF8.GetBytes(cin.ReadLine() + "\n");
                    stream.Write(line, 0, line.Length);
                }
                stream.Position = 0;

                // Init autocompleter
                autocompleter.Init(parser.Parse(stream));
            }

            // Read user words
            int userWordsCount = int.Parse(cin.ReadLine());
            List<string> userWords = new List<string>();
            for (int i = 0; i < userWordsCount; ++i)
            {
                userWords.Add(cin.ReadLine().Trim());
            }

            // Autocomplete
            foreach (var w in userWords)
            {
                var suggest = autocompleter.get(w);
                foreach (var s in suggest)
                {
                    cout.WriteLine(s);
                }
                cout.WriteLine();
            }
        }
    }
}
