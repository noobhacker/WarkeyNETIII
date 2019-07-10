using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Warkey.Core.Items;

namespace Warkey.Core.ViewModels
{
    public class AutoChatViewModel : BaseViewModel
    {
        public ObservableCollection<AutoChatItem> ListOfAutoChats { get; set; }

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

        public AutoChatViewModel()
        {
            ListOfAutoChats = new ObservableCollection<AutoChatItem>();
            ExtraCommandVisibility = Visibility.Collapsed;
        }

    }
}
