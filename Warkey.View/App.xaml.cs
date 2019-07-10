using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace Warkey.Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var currentProcess = Process.GetCurrentProcess();
            if (Process.GetProcessesByName(currentProcess.ProcessName).Length > 1)
            {
                Application.Current.Shutdown();
                return;
            }

            base.OnStartup(e);

            //if (!File.Exists("Newtonsoft.Json.dll"))
            //{
            //    try
            //    {
            //        var wb = new WebClient();
            //        wb.DownloadFile("http://leesong.azurewebsites.net/Newtonsoft.Json.dll", "Newtonsoft.Json.dll");
            //        wb.DownloadFileCompleted += (sender, _e) =>
            //        {
            //            base.OnStartup(e);
            //        };
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Newtonsoft.Json.dll is missing and no internet connection available.");
            //        Application.Current.Shutdown();
            //        return;
            //    }
            //}
            //else
            //{
            //    base.OnStartup(e);
            //}

        }
    }
}
