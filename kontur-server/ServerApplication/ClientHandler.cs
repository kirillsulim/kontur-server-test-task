using kontur_server.Adapters;
using kontur_server_core;
using kontur_server_core.Protocol;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using kontur_server_core.Autocompleter;
using NLog;

namespace kontur_server.ServerApplication
{
    public class ClientHandler : IClientHandler
    {
        private static Encoding encoding = Encoding.ASCII;

        private Logger logger = LogManager.GetCurrentClassLogger();        

        private IRequestHandler requestHandler;

        private IProtocolReader pReader;

        public ClientHandler(IProtocolReader reader, IRequestHandler requestHandler)
        {
            if (requestHandler == null)
                throw new ArgumentNullException();
            this.requestHandler = requestHandler;

            if(reader == null)
                throw new ArgumentNullException();
            this.pReader = reader;
        }

        public void Handle(ITcpClient client)
        {
            Stream stream = client.GetStream();
            while (true)
            {
                var request = pReader.ReadString(stream);

                if (request == "exit")
                    break;

                string[] response;
                try
                {
                    response = ProcessRequest(request);
                }
                catch (ProcessingException)
                {
                    response = new string[] { "ERROR!!! Error on processing request. \"" + request + "\" is not correct request.\n" };
                }
                catch (Exception e)
                {
                    logger.Error(e);
                    response = new string[] { "ERROR!!! Error on processing request\n" };
                }

                pReader.WriteStringArray(stream, response);
            }
        }

        /// <summary>
        /// Process 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string[] ProcessRequest(string request)
        {
            return requestHandler.HandleRequestToAutocompleter(request);            
        }
    }

    public class ProcessingException : Exception
    {
    }
}
