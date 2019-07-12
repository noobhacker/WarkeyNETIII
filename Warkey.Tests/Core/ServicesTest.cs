using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warkey.Core;

namespace Warkey.Tests.Core
{
    [TestClass]
    public class ServicesTest
    {
        [TestMethod]
        public async Task TestInitialization()
        {
            var services = new Services();
            await services.InitializeAsync();
            services.Dispose();            
        }
    }
}
