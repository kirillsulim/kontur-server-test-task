using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server.ServerApplication
{
    /// <summary>
    /// Interface for server application
    /// </summary>
    interface IServerApplication
    {
        /// <summary>
        /// Start application on selected port
        /// </summary>
        /// <param name="port">Port</param>
        void Start(int port);

        /// <summary>
        /// Stop application
        /// </summary>
        void Stop();
    }
}
