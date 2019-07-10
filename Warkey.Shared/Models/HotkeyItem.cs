using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Warkey.Core.ViewModels;

namespace Warkey.Core.Items
{
    public class HotkeyItem : BaseViewModel
    {
        public bool Alt { get; set; }
        public Key Key { get; set; }

        public HotkeyItem()
        {
            Alt = false;
        }
    }
}
