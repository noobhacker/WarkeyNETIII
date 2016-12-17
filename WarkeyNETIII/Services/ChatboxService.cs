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
        public static async void SendMessageToChatbox(IntPtr hwnd, string message, bool isAlt)
        {
            if (isAlt)
            {
                
                PostMessageService.PostKeyToWar3(hwnd, Keys.Enter);
                PostMessageService.PostAltUpToWar3(hwnd);
                await Task.Delay(150);

                SendKeys.SendWait(message);
                
                    await Task.Delay(100);

                PostMessageService.PostKeyToWar3(hwnd, Keys.Enter);
                
                    PostMessageService.PostAltDownToWar3(hwnd);
            }
            else
            {
                SendKeys.SendWait("{ENTER}");
                SendKeys.SendWait(message);
                SendKeys.SendWait("{ENTER}");
            }
        }
    }
}
