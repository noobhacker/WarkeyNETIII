using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Warkey.Core;
using System.Threading.Tasks;

namespace Warkey.Tests.Core
{
    /// <summary>
    /// Summary description for SettingsTest
    /// </summary>
    [TestClass]
    public class SettingsTest
    {
        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public async Task TestLoading()
        {
            var settings = new Settings();
            await settings.LoadAsync();
        }

        [TestMethod]
        public async Task TestIfPropertyProperlySaved()
        {
            var settings = new Settings();
            Settings.IsStartMinimized = true;
            await settings.SaveAsync(new Settings.SettingsModel());
            var result = await settings.LoadAsync();
            Assert.IsTrue(result.IsStartMinimized);
        }
    }
}
