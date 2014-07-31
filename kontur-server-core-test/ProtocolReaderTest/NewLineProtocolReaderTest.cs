using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using kontur_server_core.Protocol;
using System.Collections.Generic;

namespace contur_server_core_test
{
    [TestClass]
    public class NewLineProtocolReaderTest : BaseProtocolReaderTest
    {
        protected override IProtocolReader GetReader()
        {
            return new NewLineProtocolReader();
        }
    }
}
