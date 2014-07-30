using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject.Modules;
using kontur_server_core;

namespace kontur_server
{
    /// <summary>
    /// Bindings for Ninject
    /// </summary>
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IServerApplication>().To<ServerApplication>();
            Bind<IAutocompleter>().To<Autocompleter>();
            Bind<IDictionaryParser>().To<DictionaryParser>().InSingletonScope();
            Bind<IClientHandler>().To<ClientHandler>().InThreadScope();
            Bind<IDictionaryGetter>().To<FileGetter>().InSingletonScope();

            Bind<Encoding>().ToConstant<Encoding>(Encoding.ASCII);
        }
    }
}
