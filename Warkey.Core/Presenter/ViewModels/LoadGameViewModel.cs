using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Warkey.Infrastructure.GameSaves;

namespace Warkey.Core.ViewModels
{
    public class LoadGameViewModel : BaseViewModel
    {
        public ObservableCollection<TkokSave> Saves { get; set; }

        public LoadGameViewModel()
        {
            Saves = new ObservableCollection<TkokSave>();
        }
    }
}
