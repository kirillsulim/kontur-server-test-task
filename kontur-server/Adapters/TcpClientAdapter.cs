using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server.Adapters
{
    public class TcpClientAdapter : ITcpClient
    {
        private TcpClient client;

        public TcpClientAdapter(TcpClient client)
        {
            this.client = client;
        }

        public Stream GetStream()
        {
            return client.GetStream();
        }

        public void Dispose()
        {
            client.Close();
        }
    }
}
