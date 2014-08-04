using kontur_server_core.Autocompleter;
using kontur_server_core.DictionaryUtils;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace simple_app
{
    /// <summary>
    /// Simple applicatiom implementation
    /// </summary>
    class SimpleApplication : ISimpleApplication
    {
        private IDictionaryParser parser;

        private IAutocompleter autocompleter;

        public void Run(TextReader cin, TextWriter cout)
        {
            IDictionaryGetter getter;
            IDictionaryParser parser;
            IAutocompleter autocompleter;

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
                parser = new DictionaryParser();
                getter = new ProxyGetter(parser.Parse(stream));
                autocompleter = new Autocompleter(getter);
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
                var suggest = autocompleter.Get(w);
                foreach (var s in suggest)
                {
                    cout.WriteLine(s);
                }
                cout.WriteLine();
            }
        }
    }
}
