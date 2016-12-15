using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WarkeyNETIII.Services
{
    public static class ForegroundWindowService
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static bool IsWar3Foreground(IntPtr hwnd)
        {
            if (GetForegroundWindow() == hwnd)
                return true;

            return false;
        }
    }
}
