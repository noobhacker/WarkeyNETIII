using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warkey.Infrastructure
{
    public class KeyboardSender
    {
        internal class NativeMethods
        {
            [DllImport("user32.dll", EntryPoint = "SendMessageA")]
            internal static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        }

        const uint WM_KEYDOWN = 0x0100;
        const uint WM_KEYUP = 0x0101;

        // LEFT ALT for wParam
        private const int VK_MENU = 0x0012;
        private const int WM_SYSKEYDOWN = 0x0104;

        public Keys[] VKeys =
        {
            Keys.NumPad7,
            Keys.NumPad8,
            Keys.NumPad4,
            Keys.NumPad5,
            Keys.NumPad1,
            Keys.NumPad2
        };

        public void SendItemMessageToHwnd(IntPtr hwnd, int slotNumber, bool isAlt)
        {
            if (isAlt)
                NativeMethods.PostMessage(hwnd, WM_KEYUP, VK_MENU, 0);

            NativeMethods.PostMessage(hwnd, WM_KEYDOWN, (int)VKeys[slotNumber], 0);
            NativeMethods.PostMessage(hwnd, WM_KEYUP, (int)VKeys[slotNumber], 0);

            if (isAlt)
                NativeMethods.PostMessage(hwnd, WM_SYSKEYDOWN, VK_MENU, 0);
        }

        public bool IsChatting = false;
        public void SendMessageToChatbox(string message)
        {
            IsChatting = true;
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(message);
            SendKeys.SendWait("{ENTER}");
            IsChatting = false;
        }
    }
}
