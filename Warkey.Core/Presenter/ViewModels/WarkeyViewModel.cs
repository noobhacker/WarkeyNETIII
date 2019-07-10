using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warkey.Core.Items;

namespace Warkey.Core.ViewModels
{
    public class WarkeyViewModel : BaseViewModel
    {
        public HotkeyItem Slot1 { get; set; }
        public HotkeyItem Slot2 { get; set; }
        public HotkeyItem Slot3 { get; set; }
        public HotkeyItem Slot4 { get; set; }
        public HotkeyItem Slot5 { get; set; }
        public HotkeyItem Slot6 { get; set; }
    }
}
