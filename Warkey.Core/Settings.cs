using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Core.Services;
using Warkey.Core.ViewModels;
using Warkey.Infrastructure;

namespace Warkey.Core
{
    public class Settings
    {
        public static bool IsStartMinimized { get; set; }
        public static bool IsAutoStartWar3 { get; set; }
        public static bool IsAutoCloseWithWar3 { get; set; }

        private const string FILENAME = "WarkeyNETIII.json";
        private readonly SettingsManager _manager = new SettingsManager(FILENAME);

        public async Task LoadAsync()
        {
            try
            {
                // whole block to prevent broken json setting file
                var result = await _manager.GetAsync<SettingsModel>();
                MainWindow.WarkeyVm = result.WarkeyVm;
                MainWindow.AutoChatVm = new AutoChatViewModel();
                MainWindow.AutoChatVm.ListOfAutoChats = result.Autochats;

                IsStartMinimized = result.IsStartMinimized;
                IsAutoStartWar3 = result.IsAutoStartWar3;
                IsAutoCloseWithWar3 = result.IsAutoCloseWithWar3;
            }
            catch
            {
                return;
            }

            if (IsAutoStartWar3)
                if (File.Exists("war3.exe"))
                    if (MainWindowHandleService.GetWar3HWND() == null)
                        Process.Start("war3.exe");

        }

        public static async Task Savesync()
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

        //private static void InitializeViewModels()
        //{
        //    MainWindow.WarkeyVm = new WarkeyViewModel();
        //    MainWindow.AutoChatVm = new AutoChatViewModel();

        //    IsStartMinimized = false;
        //    IsAutoStartWar3 = false;
        //    IsAutoCloseWithWar3 = false;
        //}

        public class SettingsModel
        {
            public WarkeyViewModel WarkeyVm { get; set; }
            public ObservableCollection<AutoChatItem> Autochats { get; set; }
            public bool IsStartMinimized { get; set; }
            public bool IsAutoStartWar3 { get; set; }
            public bool IsAutoCloseWithWar3 { get; set; }
        }
    }
}
