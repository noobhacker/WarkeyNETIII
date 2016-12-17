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
                Hotkey = new HotkeyItem()
                {
                    Alt = true,
                    Key = Key.A
                },
                Message = "-refresh"
            });
            ListOfAutoChats.Add(new AutoChatItem()
            {
                Hotkey = new HotkeyItem()
                {
                    Key = Key.Oem3
                },
                Message = "-spawncreeps"
            });
            ListOfAutoChats.Add(new AutoChatItem()
            {
                Hotkey = new HotkeyItem()
                {
                    Key = Key.NumPad9
                },
                Message = "-killall"
            });
            ListOfAutoChats.Add(new AutoChatItem()
            {
                Hotkey = new HotkeyItem()
                {
                    Key = Key.Oem5
                },
                Message = "-wtf"
            });

        }

    }
}
