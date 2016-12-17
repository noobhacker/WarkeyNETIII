using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace WarkeyNETIII.Services
{
    public static class PostMessageService
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        const uint WM_KEYDOWN = 0x0100;
        const uint WM_KEYUP = 0x0101;

        // LEFT ALT for wParam
        private const int VK_MENU = 0x0012;
        private const int WM_SYSKEYDOWN = 0x0104;

        public static Keys[] VKeys =
        {
            Keys.NumPad7,
            Keys.NumPad8,
            Keys.NumPad4,
            Keys.NumPad5,
            Keys.NumPad1,
            Keys.NumPad2
        };

        private static void sendKeyToHwnd(IntPtr hwnd, int key)
        {
            PostMessage(hwnd, WM_KEYDOWN, key, 0);
            PostMessage(hwnd, WM_KEYUP, key, 0);
        }

        public static void PostItemMessageToWar3(IntPtr hwnd, int slotNumber, bool isAlt)
        {
            if (isAlt)
                PostAltUpToWar3(hwnd);

            sendKeyToHwnd(hwnd, (int)VKeys[slotNumber]);

            if (isAlt)
                PostAltDownToWar3(hwnd);
        }

        public static void PostKeyToWar3(IntPtr hwnd, Keys key)
        {
            sendKeyToHwnd(hwnd, (int)key);
        }

        public static void PostAltUpToWar3(IntPtr hwnd)
        {
            PostMessage(hwnd, WM_KEYUP, VK_MENU, 0);
        }

        public static void PostAltDownToWar3(IntPtr hwnd)
        {
            PostMessage(hwnd, WM_SYSKEYDOWN, VK_MENU, 0);
        }

    }
}
