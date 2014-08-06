using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kontur_server_core;

namespace kontur_server_core.DictionaryUtils
{
    /// <summary>
    /// Parse stream with dictionary
    /// </summary>
    public interface IDictionaryParser
    {
        /// <summary>
        /// Parse dictionary with words and frequencies from UTF-8 encoded stream
        /// </summary>
        /// <param name="s">Stream</param>
        /// <returns>Dictionary with words and frequencies</returns>
        IEnumerable<DictionaryElement> Parse(Stream s);
    }
}
