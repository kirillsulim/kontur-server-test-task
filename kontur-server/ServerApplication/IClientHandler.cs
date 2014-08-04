using kontur_server.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server.ServerApplication
{
    /// <summary>
    /// Handler
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// Handle response from client
        /// </summary>
        /// <param name="client">client</param>
        void Handle(ITcpClient client);
    }
}
