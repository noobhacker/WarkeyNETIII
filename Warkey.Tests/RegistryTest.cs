using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warkey.Tests
{
    [TestClass]
    public class RegistryTest
    {
        [TestMethod]
        public void TestReadingResolution()
        {
            var resolution = Warkey.Core.Services.RegistryService.ReadResolution();
            Assert.IsNotNull(resolution);
        }
    }
}
