using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarkeyNETIII.Items;
using WarkeyNETIII.Models;
using WarkeyNETIII.Services;
using WarkeyNETIII.ViewModels;

namespace WarkeyNETIII
{
    public static class Settings
    {
        public static bool IsStartMinimized { get; set; }
        public static bool IsAutoStartWar3 { get; set; }
        public static bool IsAutoCloseWithWar3 { get; set; }

        private const string FILENAME = "WarkeyNETIII.json";

        public static async Task InitializeAsync()
        {
            if (File.Exists(FILENAME))
            {
                var sr = new StreamReader(FILENAME);
                var json = await sr.ReadToEndAsync();
                sr.Dispose();

                SettingsModel obj;
                try
                {
                    // whole block to prevent broken json setting file
                    obj = JsonConvert.DeserializeObject<SettingsModel>(json);
                    MainWindow.WarkeyVm = obj.WarkeyVm;

                    MainWindow.AutoChatVm = new AutoChatViewModel();
                    MainWindow.AutoChatVm.ListOfAutoChats = obj.Autochats;

                    IsStartMinimized = obj.IsStartMinimized;
                    IsAutoStartWar3 = obj.IsAutoStartWar3;
                    IsAutoCloseWithWar3 = obj.IsAutoCloseWithWar3;
                }
                catch
                {
                    InitializeViewModels();
                    return;
                }                

                if (IsAutoStartWar3)
                    if (File.Exists("war3.exe"))
                        if (MainWindowHandleService.GetWar3HWND() == null)
                            Process.Start("war3.exe");
            }
            else
            {
                InitializeViewModels();
            }
        }

        public static async Task SaveSettingsAsync()
        {
            if (File.Exists(FILENAME))
                File.Delete(FILENAME);

            var sw = new StreamWriter(FILENAME);
            var obj = JsonConvert.SerializeObject(new SettingsModel()
            {
                WarkeyVm = MainWindow.WarkeyVm,
                Autochats = MainWindow.AutoChatVm.ListOfAutoChats,
                IsStartMinimized = IsStartMinimized,
                IsAutoStartWar3 = IsAutoStartWar3,
                IsAutoCloseWithWar3 = IsAutoCloseWithWar3
            });
            await sw.WriteAsync(obj);
            sw.Dispose();
        }

        private static void InitializeViewModels()
        {
            MainWindow.WarkeyVm = new WarkeyViewModel();
            MainWindow.AutoChatVm = new AutoChatViewModel();

            IsStartMinimized = false;
            IsAutoStartWar3 = false;
            IsAutoCloseWithWar3 = false;
        }
    }
}
