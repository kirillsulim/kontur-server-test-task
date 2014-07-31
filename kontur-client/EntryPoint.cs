using kontur_client.Application;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace kontur_client
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            IPAddress ip = ResolveIp(args[0]);
            int port = ResolvePort(args[1]);


            // Init Ninject kernel
            IKernel nKernel = new StandardKernel();
            nKernel.Load(Assembly.GetExecutingAssembly());

            nKernel.Bind<IClientApplication>().To<ClientApplication>()
                .WithConstructorArgument<IPAddress>(ip)
                .WithConstructorArgument<int>(port)
                .WithConstructorArgument<TextReader>(Console.In)
                .WithConstructorArgument<TextWriter>(Console.Out);

            var app = nKernel.Get<IClientApplication>();

            app.Run();
        }

        private static int ResolvePort(string p)
        {
            int port;
            bool result = int.TryParse(p, out port);

            if (!result || port < 0 || port > UInt16.MaxValue)
            {
                throw new ArgumentException(p + " is not valid port value.");
            }
            return port;            
        }

        private static IPAddress ResolveIp(string p)
        {
            var addresses = Dns.GetHostAddresses(p);

            IPAddress ip = null;
            foreach (var i in addresses)
            {
                if (i.AddressFamily == AddressFamily.InterNetwork)
                {
                    ip = i;
                    break;
                }
            }

            if (ip == null)
            {
                throw new ArgumentException("Cannot resolve address " + p);
            }

            return ip;
        }
    }
}
