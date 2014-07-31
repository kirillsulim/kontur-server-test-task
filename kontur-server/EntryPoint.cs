using kontur_server_core.DictionaryUtils;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using NLog;
using System.Threading;

namespace kontur_server
{
    class EntryPoint
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            IServerApplication app = null;
            try
            {
                var filename = ResolveFileName(args[0]);
                var port = ResolvePort(args[1]);

                // Init Ninject kernel
                IKernel nKernel = new StandardKernel();
                nKernel.Load(Assembly.GetExecutingAssembly());

                nKernel.Bind<IDictionaryGetter>().To<FileGetter>()
                    .InSingletonScope().WithConstructorArgument<string>(filename);

                // Inject application
                app = nKernel.Get<IServerApplication>();

                // Run application
                ThreadPool.QueueUserWorkItem(
                    (o) => 
                    {
                        try
                        {
                            app.Start(port);
                        }
                        catch (Exception e)
                        {
                            logger.Fatal("Cannot start application: " + e.Message);
                        }
                    });

                do
                {
                    Console.WriteLine("Enter \"exit\" to stop server.");
                }
                while (Console.ReadLine() != "exit");

                
            }
            catch (Exception e)
            {
                logger.Fatal("Server crashed. " + e.Message);
            }
            finally
            {
                if(app != null)
                    app.Stop();
                Console.WriteLine("Server stopped.");
            }
        }

        /// <summary>
        /// Check and parse port value
        /// </summary>
        /// <param name="p">port as a string</param>
        /// <returns>port number</returns>
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

        /// <summary>
        /// Check if path is valid
        /// </summary>
        /// <param name="p">path string</param>
        /// <returns>checked path</returns>
        private static string ResolveFileName(string p)
        {
            bool valid = Uri.IsWellFormedUriString(p, UriKind.RelativeOrAbsolute);

            if (!valid)
            {
                throw new ArgumentException(p + " is not valid path.");
            }
            return p;
        }
    }
}
