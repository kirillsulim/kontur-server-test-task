using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using kontur_server_core;
using kontur_server_core.TrieAdapters;

namespace contur_server_core_test.AutocompleterTest
{
    [TestClass]
    public class CachedStringTrieAutocompleterTest : BaseAutocompleterTest
    {
        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            BaseAutocompleterTest.kernel = new StandardKernel();

            kernel
                .Bind<ITrieAdapter<DictionaryElement>>()
                .To<CachedStringTrieAdapter<DictionaryElement>>()
                .WithConstructorArgument<int>(10);
        }
    }
}
