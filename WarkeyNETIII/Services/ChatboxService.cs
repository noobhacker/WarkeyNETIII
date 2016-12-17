using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarkeyNETIII.Services
{
    public static class ChatboxService
    {
        public static void SendMessageToChatbox(IntPtr hwnd, string message)
        {
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(message);
            SendKeys.SendWait("{ENTER}");
        }
    }
}
