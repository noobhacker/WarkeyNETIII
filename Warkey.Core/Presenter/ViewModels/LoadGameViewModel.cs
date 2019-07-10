using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Warkey.Core.Items.Saves;

namespace Warkey.Core.ViewModels
{
    public class LoadGameViewModel : BaseViewModel
    {
        public ObservableCollection<TkokSaveItem> Saves { get; set; }

        public Visibility ExtraCommandVisibility { get; set; }

        public LoadGameViewModel()
        {
            Saves = new ObservableCollection<TkokSaveItem>();

            ExtraCommandVisibility = Visibility.Collapsed;
        }
    }
}
