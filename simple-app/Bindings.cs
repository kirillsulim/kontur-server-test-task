using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;
using Ninject.Modules;

using kontur_server_core;
using kontur_server_core.DictionaryUtils;
using kontur_server_core.Autocompleter;
using kontur_server_core.TrieAdapters;

namespace simple_app
{
    /// <summary>
    ///  Bindings for Ninject IoC
    /// </summary>
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDictionaryParser>().To<DictionaryParser>().InSingletonScope();
            Bind<IAutocompleter>().To<Autocompleter>();
            Bind<ITrieAdapter<DictionaryElement>>()
                //.To<VdsStringTrieAdapter<DictionaryElement>()
                //.To<CachedStringTrieAdapter<DictionaryElement>>()
                .To<VdsExtendedStringTrieAdapter<DictionaryElement>>()
                .WithConstructorArgument<int>(10); // Max cache size
            Bind<ISimpleApplication>().To<SimpleApplication>();
        }
    }
}
