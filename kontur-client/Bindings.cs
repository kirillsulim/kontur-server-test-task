using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using kontur_client.Application;
using kontur_server_core.Protocol;

namespace kontur_client
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IProtocolReader>().To<NewLineProtocolReader>();
        }
    }
}
