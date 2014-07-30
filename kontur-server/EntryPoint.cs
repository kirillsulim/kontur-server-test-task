using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace kontur_server
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            // Init Ninject kernel
            IKernel nKernel = new StandardKernel();
            nKernel.Load(Assembly.GetExecutingAssembly());

            // Inject application
            var app = nKernel.Get<IServerApplication>();

            // Run application
            app.Start(8666);

            do
            {
                Console.WriteLine("Enter \"exit\" to stop server...");
            }
            while (Console.ReadLine() != "exit");

            app.Stop();

            Console.WriteLine("Server stopped...");
            Console.ReadLine();
        }
    }
}
