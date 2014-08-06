using kontur_server_core.Autocompleter;
using kontur_server_core.DictionaryUtils;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Ninject;

namespace simple_app
{
    /// <summary>
    /// Simple applicatiom implementation
    /// </summary>
    class SimpleApplication : ISimpleApplication
    {
        private IKernel kernel;

        public SimpleApplication(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public void Run(TextReader cin, TextWriter cout)
        {
            ProxyGetter getter;
            DictionaryParser parser;
            IAutocompleter autocompleter;


            using (Stream stream = new MemoryStream())
            {
                // Get dictioary word takeCount
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

                kernel.Bind<IDictionaryParser>().ToConstant<DictionaryParser>(parser);
                kernel.Bind<IDictionaryGetter>().ToConstant<ProxyGetter>(getter);

                autocompleter = kernel.Get<IAutocompleter>();
            }

            // Read user words
            int userWordsCount = int.Parse(cin.ReadLine());
            for (int i = 0; i < userWordsCount; ++i)
            {
                var userWord = cin.ReadLine().Trim();
                var suggest = autocompleter.Get(userWord);
                foreach (var s in suggest)
                {
                    cout.WriteLine(s);
                }
                cout.WriteLine();
            }
        }
    }
}
