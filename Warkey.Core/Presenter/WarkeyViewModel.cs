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
    public class WarkeyViewModel : BaseViewModel
    {
        public ObservableCollection<HotkeyModel> Slots { get; set; }
        public WarkeyViewModel()
        {
            Slots = new ObservableCollection<HotkeyModel>();
        }

    }
}
