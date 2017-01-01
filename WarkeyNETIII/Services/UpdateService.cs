using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WarkeyNETIII.Services
{
    public static class UpdateService
    {
        public const int Version = 160101;
        const string server = "http://leesong.azurewebsites.net/";
        const string updateFile = "WarkeyNETIII.exe";
        const string versionFile = "version.txt";
        const string updateScript = "/C timeout 2 & move WarkeyNETIII.exe_ WarkeyNETIII.exe";
        //@"/K 
        //  echo.Updating WarkeyNET III
        //  & timeout 3
        //  & taskkill /IM WarkeyNETIII.exe /F
        //  & move WarkeyNETIII.exe_ WarkeyNETIII.exe /Y
        //  & echo.WarkeyNET III updated sucessfully
        //  & pause";

        static bool gotUpdate = false;

        public static async void InitializeAsync()
        {
            try
            {
                var hc = new HttpClient();
                var uri = new Uri(server + versionFile);
                var result = await hc.GetStringAsync(uri);
                if (int.Parse(result) > Version)
                {
                    var wc = new WebClient();
                    var fileUri = new Uri(server + updateFile);

                    if (File.Exists("WarkeyNETIII.exe_"))
                        File.Delete("WarkeyNETIII.exe_");

                    wc.DownloadFileAsync(fileUri, "WarkeyNETIII.exe_");
                    wc.DownloadFileCompleted += (sender, e) => gotUpdate = true;
                }
            }
            catch
            {
                // no internet connection
            }
        }

        public static void Dispose()
        {
            if (!gotUpdate)
                return;

            var info = new ProcessStartInfo("cmd.exe", updateScript);
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            info.UseShellExecute = false;

            var p = new Process();
            p.StartInfo = info;
            p.Start();
        }

    }
}
