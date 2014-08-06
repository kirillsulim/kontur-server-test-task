using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject.Modules;
using kontur_server_core;
using kontur_server_core.Autocompleter;
using kontur_server_core.DictionaryUtils;
using kontur_server_core.Protocol;
using kontur_server.ServerApplication;

using kontur_server_core.TrieAdapters;

namespace kontur_server
{
    /// <summary>
    /// Bindings for Ninject
    /// </summary>
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IServerApplication>().To<ServerApplication.ServerApplication>();
            Bind<IAutocompleter>().To<Autocompleter>().InThreadScope();
            Bind<IDictionaryParser>().To<DictionaryParser>().InSingletonScope();
            Bind<IRequestHandler>().To<RequestHandler>();
            Bind<IClientHandler>().To<ClientHandler>().InThreadScope();
            Bind<IProtocolReader>().To<NewLineProtocolReader>();
            Bind<ITrieAdapter<DictionaryElement>>().To<CachedStringTrieAdapter<DictionaryElement>>().WithConstructorArgument<int>(10);
        }
    }
}
