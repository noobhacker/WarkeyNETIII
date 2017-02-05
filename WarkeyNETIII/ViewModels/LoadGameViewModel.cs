using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WarkeyNETIII.Items.Saves;

namespace WarkeyNETIII.ViewModels
{
    public class LoadGameViewModel : BaseViewModel
    {
        public ObservableCollection<TkokSaveItem> Saves { get; set; }

        public Visibility ExtraCommandVisibility
        {
            get
            {
                return extraCommandVisibility;
            }

            set
            {
                extraCommandVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility extraCommandVisibility;

        public LoadGameViewModel()
        {
            Saves = new ObservableCollection<TkokSaveItem>();

            ExtraCommandVisibility = Visibility.Collapsed;
        }
    }
}
