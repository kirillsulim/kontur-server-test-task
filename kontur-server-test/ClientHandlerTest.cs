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

namespace kontur_server_test
{
    [TestClass]
    public class ClientHandlerTest : BaseTest
    {
        private static IKernel nKernel;

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
        }

        [TestMethod]
        public void ShouldHandleSimpleResponse()
        {
            // Arrange
            Mock<ITcpClient> client = new Mock<ITcpClient>();
            Stream stream = new MemoryStream();
            client.Setup(c => c.GetStream()).Returns(() => stream);

            byte[] request = Encoding.UTF8.GetBytes("get aaa");
            stream.Write(request, 0, request.Length);
            stream.Position = 0;

            IClientHandler handler = nKernel.Get<IClientHandler>();

            // Act
            handler.Handle(client.Object);
            
            // Assert
            stream.Position = 7;
            StreamReader reader = new StreamReader(stream);
            string response = reader.ReadToEnd();

            Assert.AreEqual("aaa\r\naaab\r\n\r\n", response);
        }

        [TestMethod]
        public void ShouldReturnEmptyResponse()
        {
            // Arrange
            Mock<ITcpClient> client = new Mock<ITcpClient>();
            Stream stream = new MemoryStream();
            client.Setup(c => c.GetStream()).Returns(() => stream);

            byte[] request = Encoding.UTF8.GetBytes("get xyz");
            stream.Write(request, 0, request.Length);
            stream.Position = 0;

            IClientHandler handler = nKernel.Get<IClientHandler>();

            // Act
            handler.Handle(client.Object);

            // Assert
            stream.Position = 7;
            StreamReader reader = new StreamReader(stream);
            string response = reader.ReadToEnd();

            Assert.AreEqual("\r\n", response);
        }
    }
}
