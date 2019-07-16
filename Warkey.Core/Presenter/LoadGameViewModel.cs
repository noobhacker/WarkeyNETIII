using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Warkey.Infrastructure.GameSaves;

namespace Warkey.Core.Presenter
{
    [AddINotifyPropertyChangedInterface]
    public class LoadGameViewModel
    {
        public ObservableCollection<TkokSave> Saves { get; set; }
        public Visibility ExtraCommandVisibility { get; set; }

        public LoadGameViewModel()
        {
            Saves = new ObservableCollection<TkokSave>();
        }
    }
}
