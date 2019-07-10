using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Warkey.Infrastructure.Game
{
    public class Window
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

        private static readonly string[] _processNames = { "war3", "War3" };

        public IntPtr? GetWar3HWND()
        {
            foreach (var name in _processNames)
            {
                var process = Process.GetProcessesByName(name);
                if (process.Count() != 0)
                {
                    return process[0].MainWindowHandle;
                }
            }

            return null;
        }
    }
}
