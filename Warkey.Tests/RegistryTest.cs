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
            var resolution = WarkeyNETIII.Services.RegistryService.ReadResolution();
            Assert.IsNotNull(resolution);
        }
    }
}
