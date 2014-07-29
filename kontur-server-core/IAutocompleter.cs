using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server_core
{
    /// <summary>
    /// Interface for autocomplete service
    /// </summary>
    public interface IAutocompleter
    {
        /// <summary>
        /// Init autocompleter with dictionary
        /// </summary>
        /// <param name="d">Key-value collection with words and its frequencies</param>
        void Init(Dictionary<string, int> d);

        /// <summary>
        /// Get 10 (or less) most frequent words starts with index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>10 or less most frequent words</returns>
        string[] get(string index);
    }
}
