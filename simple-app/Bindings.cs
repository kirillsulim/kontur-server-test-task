using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;
using Ninject.Modules;

using kontur_server_core;

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
            Bind<ISimpleApplication>().To<SimpleApplication>();
        }
    }
}
