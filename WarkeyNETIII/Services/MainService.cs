using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WarkeyNETIII.Services
{
    public static class MainService
    {
        const int HWND_UPDATE_INTERVAL = 4;

        static IntPtr War3Hwnd;
        static bool isWar3Foreground = false;

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
        }

        private static void updateWar3Hwnd()
        {
            var hwnd = MainWindowHandleService.GetWar3HWND();
            if (hwnd != null)
            {
                War3Hwnd = hwnd.Value;
                MainWindow.vm.Title = "Running";

                isWar3Foreground = ForegroundWindowService.IsWar3Foreground(War3Hwnd);
            }
            else
            {
                MainWindow.vm.Title = "";
            }            
        }

    }
}
