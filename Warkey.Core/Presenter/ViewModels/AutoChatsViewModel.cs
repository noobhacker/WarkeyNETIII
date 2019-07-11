using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Warkey.Core.ViewModels
{
    public class AutoChat : BaseViewModel
    {
        public ObservableCollection<AutoChatViewModel> ListOfAutoChats { get; set; }
        
        public AutoChat()
        {
            ListOfAutoChats = new ObservableCollection<AutoChatViewModel>();
        }

    }
}
