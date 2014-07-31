using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using kontur_server_core.Protocol;
using System.IO;
using System.Collections.Generic;
using TestUtils;

namespace contur_server_core_test
{
    [TestClass]
    public class NumberedProtocolReaderTest : BaseProtocolReaderTest
    {
        protected override IProtocolReader GetReader()
        {
            return new NumberedProtocolReader();
        }
    }
}
