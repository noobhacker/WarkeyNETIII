using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WarkeyNETIII.Items;
using WarkeyNETIII.ViewModels;

namespace WarkeyNETIII.Services
{
    public static class MainService
    {
        const int HWND_UPDATE_INTERVAL = 1;

        static IntPtr war3Hwnd;
        public static bool IsWar3Foreground = false;

        public static void InitializeServicesAsync()
        {
            // run services on background
            // to make sure UI smoothness
            Task.Run(() =>
            {
                updateWar3Hwnd();
                var hwndListener = new DispatcherTimer();
                hwndListener.Interval = TimeSpan.FromSeconds(HWND_UPDATE_INTERVAL);
                hwndListener.Tick += (sender, e) => updateWar3Hwnd();
                hwndListener.Start();
            });

            // cannot run in another thread
            // because windows API cannot return hook target with proc name
            // task.run in another thread
            KeyboardHookService.Initialize();
            KeyboardHookService.GlobalKeyDown += KeyboardHookService_GlobalKeyDown;
        }

        static WarkeyViewModel warkeyVm = MainWindow.WarkeyVm;
        static ObservableCollection<AutoChatItem> autochats = MainWindow.AutoChatVm.ListOfAutoChats;

        static HotkeyItem[] warkeys =
        {
            warkeyVm.Slot1,
            warkeyVm.Slot2,
            warkeyVm.Slot3,
            warkeyVm.Slot4,
            warkeyVm.Slot5,
            warkeyVm.Slot6,
        };

        private static void KeyboardHookService_GlobalKeyDown(object sender, HotkeyItem e)
        {
            // check iswar3foreground already in keyboard hook class

            foreach (var i in Enumerable.Range(0, 5))
            {
                // if key is not set
                if (warkeys[i] == null)
                    continue;

                if (warkeys[i].Key == e.Key && warkeys[i].Alt == e.Alt)
                    PostMessageService.PostItemMessageToWar3(war3Hwnd, i, e.Alt);
            }

            foreach (var item in autochats)
            {
                if (item.Key == e.Key && !e.Alt)
                    ChatboxService.SendMessageToChatbox(war3Hwnd, item.Message);
            }
        }

        static bool wentRunning = false;
        private static void updateWar3Hwnd()
        {
            var hwnd = MainWindowHandleService.GetWar3HWND();
            if (hwnd != null)
            {
                war3Hwnd = hwnd.Value;
                MainWindow.vm.Title = "Running";
                wentRunning = true;

                IsWar3Foreground = ForegroundWindowService.IsWar3Foreground(war3Hwnd);
            }
            else
            {
                if (wentRunning && Settings.IsAutoCloseWithWar3)
                    App.Current.MainWindow.Close();

                MainWindow.vm.Title = "";
            }
        }

    }
}
