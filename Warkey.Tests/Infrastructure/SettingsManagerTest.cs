using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Infrastructure;

namespace Warkey.Tests.Infrastructure
{
    [TestClass]
    public class SettingsManagerTest
    {
        [TestMethod]
        public async Task TestSaveGeneratesFile()
        {
            var manager = new SettingsManager("test.json");
            await manager.SaveAsync(new SaveModel());
            Assert.IsTrue(File.Exists("test.json"));
        }

        [TestMethod]
        public async Task TestSavesWithContent()
        {
            var manager = new SettingsManager("test.json");
            await manager.SaveAsync(new SaveModel());
            var content = "";
            using (var streamReader = new StreamReader("test.json"))
            {
                content = await streamReader.ReadToEndAsync();
            }
            Assert.IsTrue(content != "");
        }

        [TestMethod]
        public async Task TestLoad()
        {
            var manager = new SettingsManager("test.json");
            await manager.SaveAsync(new SaveModel());
            var result = await manager.GetAsync<SaveModel>();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestLoadWithCorruptedFile()
        {
            using (var sw = new StreamWriter("test.json"))
            {
                await sw.WriteAsync("fdsajlhfsdja00");
            }

            var manager = new SettingsManager("test.json");
            var result = await manager.GetAsync<SaveModel>();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task TestLoadWithFileThatDoesntExist()
        {
            var manager = new SettingsManager("testDoesntExist.json");
            var result = await manager.GetAsync<SaveModel>();
            Assert.IsNotNull(result);
        }

        public class SaveModel
        {
            public int TestInteger { get; set; }
            public string TestString { get; set; }
        }
    }
}
