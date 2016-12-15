using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WarkeyNETIII.Services
{
    public static class PostMessageService
    {
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        const uint WM_KEYDOWN = 0x0100;
        const uint WM_KEYUP = 0x0101;

        public static void PostMessageToWar3(IntPtr hwnd, Key key)
        {
            PostMessage(hwnd, WM_KEYDOWN, (int)key, 0);
            PostMessage(hwnd, WM_KEYUP, (int)key, 0);
        }
    }
}
