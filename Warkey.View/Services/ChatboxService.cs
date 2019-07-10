using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Warkey.Core.Services
{
    public static class ChatboxService
    {
        public static bool IsChatting = false;
        public static void SendMessageToChatbox(IntPtr hwnd, string message)
        {
            IsChatting = true;
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(message);
            SendKeys.SendWait("{ENTER}");
            IsChatting = false;
        }
    }
}
