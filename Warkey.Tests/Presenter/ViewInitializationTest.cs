using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.View;
using Warkey.View.Pages;

namespace Warkey.Tests.Presenter
{
    [TestClass]
    public class ViewInitializationTest
    {
        [TestMethod] 
        public void TestMainWindow()
        {
            var mainWindow = new MainWindow();
            new WarkeyPage(mainWindow._services);
        }
    }
}
