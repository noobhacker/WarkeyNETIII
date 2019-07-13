using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Warkey.Infrastructure
{
    public class GameWindow
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public bool IsWar3Foreground(IntPtr hwnd)
        {
            if (GetForegroundWindow() == hwnd)
            {
                return true;
            }

            return false;
        }

        public IntPtr? GetWar3HWND()
        {
            var processes = Process.GetProcesses();
            var process = processes.FirstOrDefault(x => x.ProcessName == "war3" || x.ProcessName == "War3");
            if (process != null)
            {
                return process.MainWindowHandle;
            }

            return null;
        }

        // this is slower because calling GetProcessByName Twice
        // Tested on project, 200 times, 1901ms vs 3422ms
        // 269 vs 550 compute units in visual studio profiling tool
        //public IntPtr? GetWar3HWND()
        //{
        //    foreach (var name in _processNames)
        //    {
        //        var process = Process.GetProcessesByName(name);
        //        if (process.Count() != 0)
        //        {
        //            return process[0].MainWindowHandle;
        //        }
        //    }

        //    return null;
        //}
    }
}
