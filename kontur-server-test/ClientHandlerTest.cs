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
using kontur_server_core.Autocompleter;
using kontur_server.ServerApplication;

namespace kontur_server_test
{
    [TestClass]
    public class ClientHandlerTest
    {
        private StreamPositionSaver posSaver = new StreamPositionSaver();

        private class ProtocolReaderMock : IProtocolReader
        {
            // Store last writed string
            public string LastWritedString { get; private set; }
            public void WriteString(Stream s, string index)
            {
                LastWritedString = index;
            }

            // Set exit
            private bool exit = false;
            public string ReadString(Stream s)
            {
                var res = exit ? "exit" : "get a";
                exit = true;
                return res;
            }

            // Store last writed string array
            public string[] LastWritedStringArray { get; set; }
            public void WriteStringArray(Stream s, string[] words)
            {
                LastWritedStringArray = words;
            }

            // Throws
            public string[] ReadStringArray(Stream s)
            {
                throw new NotImplementedException();
            }
        }
       
        
        [TestMethod]
        public void ShouldHandleSimpleResponse()
        {
            var stream = Mock.Of<Stream>();

            // return mock-stream
            var client = Mock.Of<ITcpClient>(cl => cl.GetStream() == stream);

            // always return {"a", "ab"} array
            var handler = new Mock<IRequestHandler>();
            handler
                .Setup(h => h.HandleRequestToAutocompleter(It.IsAny<string>()))
                .Returns(new string[] { "a", "ab" });

            var readerMock = new ProtocolReaderMock();

            
            IClientHandler clientHandler = new ClientHandler(readerMock, handler.Object);

            clientHandler.Handle(client);

            CollectionAssert.AreEqual(
                readerMock.LastWritedStringArray,
                new string[] { "a", "ab" });
        }

        [TestMethod]
        public void ShouldReturnEmptyResponse()
        {
            var stream = Mock.Of<Stream>();

            // return mock-stream
            var client = Mock.Of<ITcpClient>(cl => cl.GetStream() == stream);

            // always return {"a", "ab"} array
            var handler = new Mock<IRequestHandler>();
            handler
                .Setup(h => h.HandleRequestToAutocompleter(It.IsAny<string>()))
                .Returns(new string[] {});

            var readerMock = new ProtocolReaderMock();


            IClientHandler clientHandler = new ClientHandler(readerMock, handler.Object);

            clientHandler.Handle(client);

            Assert.AreEqual(0, readerMock.LastWritedStringArray.Length);
        }
    }
}
