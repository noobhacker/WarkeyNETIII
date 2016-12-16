using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarkeyNETIII.ViewModels;

namespace WarkeyNETIII.Items
{
    public class AutoChatItem : BaseViewModel
    {
        public ObservableCollection<HotkeyItem> ListOfAutoChats { get; set; }
    }
}
