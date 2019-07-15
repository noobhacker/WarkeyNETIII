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
        internal class NativeMethods
        {
            [DllImport("user32.dll")]
            internal static extern IntPtr GetForegroundWindow();
        }

        public bool IsWar3Foreground(IntPtr hwnd)
        {
            if (NativeMethods.GetForegroundWindow() == hwnd)
            {
                return true;
            }

            return false;
        }

        // if use StringComparison.IgnoreCase, performance is similar, but lack of self explainary
        // use this for better intention to support Warcraft III 1.24e and 1.26
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
        // my assumption is GetProcessByName loops through whole list
        // to retrieve array, while the new algorithm uses FirstOrDefault
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
