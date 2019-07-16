using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Warkey.Infrastructure.KeyboardDetector;

namespace Warkey.Core.Presenter
{
    [AddINotifyPropertyChangedInterface]
    public class WarkeyViewModel
    {
        public ObservableCollection<HotkeyModel> Slots { get; set; }
        public WarkeyViewModel()
        {
            Slots = new ObservableCollection<HotkeyModel>();
        }

    }
}
