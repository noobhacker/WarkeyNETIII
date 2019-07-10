using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Core.Items;
using Warkey.Core.ViewModels;

namespace Warkey.Core.Models
{
    public class SettingsModel
    {
        public WarkeyViewModel WarkeyVm { get; set; }
        public ObservableCollection<AutoChatItem> Autochats { get; set; }
        public bool IsStartMinimized { get; set; }
        public bool IsAutoStartWar3 { get; set; }
        public bool IsAutoCloseWithWar3 { get; set; }
    }
}
