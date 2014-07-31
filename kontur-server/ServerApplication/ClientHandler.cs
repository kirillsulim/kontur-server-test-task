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

namespace kontur_server
{
    public class ClientHandler : IClientHandler
    {
        private static Encoding encoding = Encoding.ASCII;

        private IAutocompleter autocompleter;

        private IProtocolReader pReader;

        public ClientHandler(IAutocompleter autocompleter, IProtocolReader reader)
        {
            if (autocompleter == null)
                throw new ArgumentNullException();
            this.autocompleter = autocompleter;

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
                catch (Exception)
                {
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
            Regex pattern = new Regex("get [a-z]+");
            if (!pattern.IsMatch(request))
            {
                throw new ProcessingException();
            }

            return autocompleter.Get(request.Substring(4));
        }
    }

    public class ProcessingException : Exception
    {
    }
}
