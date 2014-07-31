using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace kontur_client.Application
{
    /// <summary>
    /// Interface for client application
    /// </summary>
    public interface IClientApplication
    {
        /// <summary>
        /// Run and connect to server
        /// </summary>
        void Run();
    }
}
