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
        public static void SendMessageToChatbox(string message)
        {
            SendKeys.Send("{ENTER}");
            SendKeys.Send(message);
            SendKeys.Send("{ENTER}");
        }
    }
}
