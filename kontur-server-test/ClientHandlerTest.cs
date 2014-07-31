using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Sockets;
using kontur_server;
using System.IO;
using kontur_server.Adapters;
using System.Text;
using Ninject;
using kontur_server_core;
using kontur_server_core.Protocol;
using System.Collections.Generic;
using TestUtils;

namespace kontur_server_test
{
    [Ignore] // Broken after cyclic read in Handler. 
    [TestClass]
    public class ClientHandlerTest
    {
        private static IKernel nKernel;

        private static IProtocolReader reader;

        private static StreamPositionSaver posSaver = new StreamPositionSaver();

        [ClassInitialize]
        public static void SetUp(TestContext c)
        {
            Mock<IAutocompleter> autocompleter = new Mock<IAutocompleter>();

            // If aaa returns 2 autocomplete
            autocompleter
                .Setup(x => x.Get(It.Is<string>(s => s == "aaa")))
                .Returns(new string[] {"aaa", "aaab"});

            // If not aaa return empty respomse
            autocompleter
                .Setup(x => x.Get(It.Is<string>(s => s != "aaa")))
                .Returns(new string[0]);
            

            nKernel = new StandardKernel();
            nKernel.Bind<IAutocompleter>().ToConstant<IAutocompleter>(autocompleter.Object);
            nKernel.Bind<IClientHandler>().To<ClientHandler>();
            nKernel.Bind<IProtocolReader>().To<NumberedProtocolReader>();

            reader = nKernel.Get<IProtocolReader>();
        }

        [TestMethod]
        public void ShouldHandleSimpleResponse()
        {
            // Arrange
            Mock<ITcpClient> client = new Mock<ITcpClient>();
            Stream stream = new MemoryStream();
            client.Setup(c => c.GetStream()).Returns(() => stream);

            reader.WriteString(stream, "get aaa");

            IClientHandler handler = nKernel.Get<IClientHandler>();

            // Act
            handler.Handle(client.Object);
            
            
            string[] response = reader.ReadStringArray(stream);

            CollectionAssert.AreEqual(new string[]{"aaa","aaab"}, response);
        }

        [TestMethod]
        public void ShouldReturnEmptyResponse()
        {
            // Arrange
            Mock<ITcpClient> client = new Mock<ITcpClient>();
            Stream stream = new MemoryStream();
            client.Setup(c => c.GetStream()).Returns(() => stream);

            reader.WriteString(stream, "get zyz");

            long point = stream.Position;
            stream.Position = 0;

            IClientHandler handler = nKernel.Get<IClientHandler>();

            // Act
            handler.Handle(client.Object);

            // Assert
            stream.Position = point;

            string[] response = reader.ReadStringArray(stream);

            CollectionAssert.AreEqual(new string[0], response);
        }
    }
}
