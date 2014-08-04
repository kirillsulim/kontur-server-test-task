using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server.ServerApplication
{
    /// <summary>
    /// Interface for request handlers
    /// </summary>
    public interface IRequestHandler
    {
        string[] HandleRequestToAutocompleter(string request);
    }
}
