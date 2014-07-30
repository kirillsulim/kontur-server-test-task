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
        /// Get 10 (or less) most frequent words starts with index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>10 or less most frequent words</returns>
        string[] Get(string index);
    }
}
