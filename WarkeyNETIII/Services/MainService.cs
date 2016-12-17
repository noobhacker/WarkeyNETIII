using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WarkeyNETIII.Items;

namespace WarkeyNETIII.Services
{
    public static class MainService
    {
        const int HWND_UPDATE_INTERVAL = 1;

        static IntPtr war3Hwnd;
        static bool isWar3Foreground = false;

        public static void InitializeServicesAsync()
        {        
            // run services on background
            // to make sure UI smoothness
            //Task.Run(() =>
            //{
                updateWar3Hwnd();
                var hwndListener = new DispatcherTimer();
                hwndListener.Interval = TimeSpan.FromSeconds(HWND_UPDATE_INTERVAL);
                hwndListener.Tick += (sender, e) => updateWar3Hwnd();
                hwndListener.Start();
            //});

            // cannot run in another thread
            // because windows API cannot return hook target with proc name
            // task.run in another thread
            KeyboardHookService.Initialize();
            KeyboardHookService.GlobalKeyDown += KeyboardHookService_GlobalKeyDown;
          
        }

        private static void KeyboardHookService_GlobalKeyDown(object sender, HotkeyItem e)
        {
            if (isWar3Foreground)
            {
                var warkeyVm = MainWindow.WarkeyVm;
                HotkeyItem[] warkeys =
                {
                    warkeyVm.Slot1,
                    warkeyVm.Slot2,
                    warkeyVm.Slot3,
                    warkeyVm.Slot4,
                    warkeyVm.Slot5,
                    warkeyVm.Slot6,
                };

                foreach (var i in Enumerable.Range(0, 5))
                {
                    // if key is not set
                    if (warkeys[i] == null)
                        continue;
                    
                    if (warkeys[i].Key == e.Key && e.Alt == warkeys[i].Alt)
                        PostMessageService.PostMessageToWar3(war3Hwnd, i, e.Alt);
                }

                var autochats = MainWindow.autoChatVm.ListOfAutoChats;
            }
        }

        private static void updateWar3Hwnd()
        {
            var hwnd = MainWindowHandleService.GetWar3HWND();
            if (hwnd != null)
            {
                war3Hwnd = hwnd.Value;
                MainWindow.vm.Title = "Running";

                isWar3Foreground = ForegroundWindowService.IsWar3Foreground(war3Hwnd);
            }
            else
            {
                MainWindow.vm.Title = "";
            }            
        }

    }
}
