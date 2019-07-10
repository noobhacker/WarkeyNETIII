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
        private bool alt;
        private Key key;

        public bool Alt
        {
            get
            {
                return alt;
            }

            set
            {
                alt = value;
                OnPropertyChanged();
            }
        }

        public Key Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
                OnPropertyChanged();
            }
        }

        public HotkeyItem()
        {
            Alt = false;
        }
    }
}
