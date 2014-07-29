using kontur_server_core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_app
{
    class SimpleApp
    {
        static void Main(string[] args)
        {
            TextReader cin = Console.In;
            Stream stream = new MemoryStream();
            
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
            IDictionaryParser parser = new DictionaryParser();
            var dictionary = parser.Parse(stream);
            IAutocompleter autocompleter = new Autocompleter();
            autocompleter.Init(dictionary);

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
                    Console.WriteLine(s);
                }
                Console.WriteLine();
            }
        }
    }
}
