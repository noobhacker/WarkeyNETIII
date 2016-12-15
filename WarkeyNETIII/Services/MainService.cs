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
        static IntPtr War3Hwnd;

        static DispatcherTimer hwndListener = new DispatcherTimer();

        public static void InitializeServices()
        {
            updateWar3Hwnd();
            hwndListener.Interval = TimeSpan.FromSeconds(5);
            hwndListener.Tick += (sender, e) => updateWar3Hwnd();
            hwndListener.Start();
        }

        private static void updateWar3Hwnd()
        {
            War3Hwnd = MainWindowHandleService.GetWar3HWND().Value;
            if (War3Hwnd != null)
                MainWindow.vm.Title = "Running";
            else
                MainWindow.vm.Title = "Idle";
        }
    }
}
