using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WarkeyNETIII.Items;

namespace WarkeyNETIII.ViewModels
{
    public class AutoChatViewModel : BaseViewModel
    {
        public ObservableCollection<AutoChatItem> ListOfAutoChats { get; set; }

        public AutoChatViewModel()
        {
            ListOfAutoChats = new ObservableCollection<AutoChatItem>();

            ListOfAutoChats.Add(new AutoChatItem()
            {
                Hotkey = new HotkeyItem(),
                Message = "aaa"
            });
            ListOfAutoChats.Add(new AutoChatItem()
            {
                Hotkey = new HotkeyItem(),
                Message = "aaa"
            });

        }

    }
}
