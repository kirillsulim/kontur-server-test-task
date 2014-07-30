using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server
{
    /// <summary>
    /// Interface 
    /// </summary>
    interface IDictionaryGetter
    {
        /// <summary>
        /// Get dictionary with words
        /// </summary>
        /// <returns>dictionary</returns>
        Dictionary<string, int> Get();
    }
}
