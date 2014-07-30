using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core
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
        Dictionary<string, int> Parse(Stream s);
    }
}
