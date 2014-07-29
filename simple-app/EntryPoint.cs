using kontur_server_core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;
using System.Reflection;

namespace simple_app
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            // Init Ninject kernel
            IKernel nKernel = new StandardKernel();
            nKernel.Load(Assembly.GetExecutingAssembly());

            // Inject application
            var app = nKernel.Get<ISimpleApplication>();

            // Run application
            app.Run(Console.In, Console.Out);
        }
    }
}
