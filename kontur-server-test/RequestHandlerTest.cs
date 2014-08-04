using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using kontur_server.ServerApplication;
using kontur_server_core.Autocompleter;
using Moq;
using System.Linq;
using System.Collections;

namespace kontur_server_test
{
    [TestClass]
    public class RequestHandlerTest
    {
        [TestMethod]
        public void ShouldHandleRequest()
        {
            Mock<IAutocompleter> autocompleter = new Mock<IAutocompleter>();
            autocompleter.Setup(ac => ac.Get(It.IsAny<string>())).Returns(new string[]{"a","ab"});

            IRequestHandler handler = new RequestHandler(autocompleter.Object);

            char[] letters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            foreach (var c in letters)
            {
                CollectionAssert.AreEqual(new string[] { "a", "ab" }, handler.HandleRequestToAutocompleter("get " + c));
            }
        }
    }
}
