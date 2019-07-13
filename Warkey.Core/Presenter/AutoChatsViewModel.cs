﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Warkey.Core.Presenter
{
    public class AutoChatsViewModel : BaseViewModel
    {
        public ObservableCollection<AutoChatViewModel> ListOfAutoChats { get; set; }
        public Visibility ExtraCommandVisibility{ get; set; }
        public AutoChatsViewModel()
        {
            ListOfAutoChats = new ObservableCollection<AutoChatViewModel>();
        }

    }
}
