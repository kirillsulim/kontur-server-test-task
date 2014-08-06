using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace kontur_server_core.DictionaryUtils
{
    /// <summary>
    /// Get dictionary from file
    /// </summary>
    public class FileGetter : IDictionaryGetter
    {
        private IDictionaryParser parser;

        IEnumerable<DictionaryElement> dict;

        /// <summary>
        /// Constructor. Get dictionary from file and parse it
        /// </summary>
        /// <param name="file">path to file</param>
        /// <param name="parser">parser</param>
        public FileGetter(string file, IDictionaryParser iParser)
        {
            parser = iParser;

            if (!File.Exists(file))
            {
                throw new FileNotFoundException("Cannot find file \"" + file + "\"");
            }

            using (Stream s = File.OpenRead(file))
            {
                this.dict = parser.Parse(s);
            }
        }

        public IEnumerable<DictionaryElement> Get()
        {
            return dict;
        }
    }
}
