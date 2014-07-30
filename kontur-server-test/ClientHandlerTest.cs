using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Sockets;
using kontur_server;

namespace kontur_server_test
{
    [TestClass]
    public class ClientHandlerTest
    {
        [TestMethod]
        public void ShouldHandleSimpleResponse()
        {
            // Arrange
            Mock<TcpClient> client = new Mock<TcpClient>();

            client.Setup(c => c.GetStream()).Returns(() => null);



            IClientHandler handler = new ClientHandler();

            // Act
            handler.Handle(client.Object);

            // Assert
        }
    }
}
