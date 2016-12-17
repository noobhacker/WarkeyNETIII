using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarkeyNETIII.ViewModels;

namespace WarkeyNETIII.Items
{
    public class AutoChatItem : BaseViewModel
    {
        private HotkeyItem hotkey;
        private string message;

        public HotkeyItem Hotkey
        {
            get
            {
                return hotkey;
            }

            set
            {
                hotkey = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
    }
}
