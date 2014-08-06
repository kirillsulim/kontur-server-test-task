using kontur_server.Adapters;
using kontur_server_core;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using kontur_server_core.Autocompleter;

namespace kontur_server.ServerApplication
{
    public class ServerApplication : IServerApplication
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Stop flag
        /// NB: No need to lock, because read/write bool is atomic
        /// Meanwhile using volatile is necessary to avoid optimisation 
        /// </summary>
        volatile private bool stopCommand;
        volatile private bool running;

        private IKernel kernel;

        public ServerApplication(IKernel kernel)
        {
            this.kernel = kernel;            
        }

        /// <summary>
        /// Start server on port
        /// </summary>
        /// <param name="port">port</param>
        public void Start(int port)
        {
            this.stopCommand = false;
            this.running = false;

            IPAddress address = GetLocalIpAddress();
            if (address == null)
            {
                throw new Exception("Cannot resolve ip address");                
            }
            
            TcpListener listner = new TcpListener(address, port);
            try
            {
                listner.Start();
                this.running = true;
                logger.Info("Server started on localhost:" + port.ToString());

                while (!stopCommand)
                {
                    if (!listner.Pending())
                    {
                        Thread.Sleep(50);
                        continue;
                    }
                    ThreadPool.QueueUserWorkItem(
                        (o) => 
                        {
                            using(ITcpClient client = new TcpClientAdapter(o as TcpClient))
                            {
                                try
                                {
                                    lock (logger)
                                    {
                                        logger.Info("Client connected.");
                                    }
                                    IClientHandler handler = kernel.Get<IClientHandler>();
                                    handler.Handle(client);
                                }
                                catch (Exception e)
                                {
                                    lock (logger)
                                    {
                                        logger.Error("Error while processing request: " + e.Message);
                                    }
                                }
                            }
                        }, 
                        listner.AcceptTcpClient()
                    );
                }
            }
            catch (Exception e)
            {
                logger.Fatal("Error: " + e.Message);
            }
            finally
            {
                listner.Stop();
                running = false;
                logger.Info("Server stopped.");
            }
        }

        private static IPAddress GetLocalIpAddress()
        {
            IPAddress address = null;
            var ipList = Dns.GetHostEntry("localhost").AddressList;

            // Get first ip v4 address
            foreach (var ip in ipList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    address = ip;
                    break;
                }
            }
            return address;
        }

        /// <summary>
        /// Set stop flag and wait
        /// </summary>
        public void Stop()
        {
            stopCommand = true;
            logger.Info("Sending stop signal.");

            // Wait listner stop
            while (running)
            {
                Thread.Sleep(50);
            }
        }
    }
}
