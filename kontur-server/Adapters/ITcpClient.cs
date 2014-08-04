using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server.Adapters
{
    /// <summary>
    /// Interface for tcp client
    /// This interface used to simplify testing
    /// </summary>
    public interface ITcpClient : IDisposable
    {
        /// <summary>
        /// Get tcp client stream
        /// </summary>
        /// <returns>stream</returns>
        Stream GetStream();
    }
}
