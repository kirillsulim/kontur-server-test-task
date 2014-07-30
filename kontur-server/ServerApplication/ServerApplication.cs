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

namespace kontur_server
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
        volatile private bool stopped;

        private IKernel kernel;

        public ServerApplication(IKernel kernel)
        {
            this.kernel = kernel;            
        }

        public void Start(int port)
        {
            this.stopCommand = false;
            this.stopped = false;

            IPAddress address = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener listner = new TcpListener(address, port);

            try
            {
                listner.Start();
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
                stopped = true;
                logger.Info("Server stopped.");
            }
        }

        public void Stop()
        {
            stopCommand = true;
            logger.Info("Sending stop signal.");
            while (!stopped)
            {
                Thread.Sleep(50);
            }
        }

        private void Handle(Object o)
        {
            var client = o as TcpClient;
        }
    }
}
