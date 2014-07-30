using kontur_server.Adapters;
using kontur_server_core;
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

        public ClientHandler(IAutocompleter autocompleter)
        {
            if (autocompleter == null)
                throw new ArgumentNullException();
            this.autocompleter = autocompleter;
        }

        public void Handle(ITcpClient client)
        {
            Stream stream = client.GetStream();
            var reader = new StreamReader(stream, encoding);
            var request = reader.ReadToEnd();

            string response;
            try
            {
                response = ProcessRequest(request);
            }
            catch (ProcessingException)
            {
                response = "ERROR!!! Error on processing request. \"" + request + "\" is not correct request.\n";
            }
            catch (Exception)
            {
                response = "ERROR!!! Error on processing request\n";
            }                 

            var writer = new StreamWriter(stream, encoding);
            writer.WriteLine(response);
            writer.Flush();
        }

        /// <summary>
        /// Process 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string ProcessRequest(string request)
        {
            Regex pattern = new Regex("get [a-z]+");
            if (!pattern.IsMatch(request))
            {
                throw new ProcessingException();
            }

            var strings = autocompleter.Get(request.Substring(4));

            var builder = new StringBuilder();
            foreach (var s in strings)
            {
                builder.AppendLine(s);
            }
            return builder.ToString();
        }
    }

    public class ProcessingException : Exception
    {
    }
}
