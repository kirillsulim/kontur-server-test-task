using kontur_server_core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kontur_server
{
    public class ServerApplication : IServerApplication
    {
        /// <summary>
        /// Stop flag
        /// NB: No need to lock, because read/write bool is atomic
        /// Meanwhile using volatile is necessary to avoid optimisation 
        /// </summary>
        volatile private bool stopFlag;

        public ServerApplication()
        {
            
        }

        public void Start(int port)
        {
            this.stopFlag = false;

            IPAddress address = Dns.GetHostEntry("localhost").AddressList[0];

            TcpListener listner = new TcpListener(address, port);
            listner.Start();

            while (!stopFlag)
            {
                var client = listner.AcceptTcpClient();

                ThreadPool.QueueUserWorkItem((o) => { this.Handle(o);}, client);
            }

            listner.Stop();
        }

        public void Stop()
        {
            stopFlag = true;
        }

        private void Handle(Object o)
        {
            var client = o as TcpClient;
        }
    }
}
