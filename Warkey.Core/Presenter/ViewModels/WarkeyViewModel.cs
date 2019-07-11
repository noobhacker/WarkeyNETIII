﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Warkey.Infrastructure.KeyboardDetector;

namespace Warkey.Core.ViewModels
{
    public class WarkeyViewModel : BaseViewModel
    {
        public ObservableCollection<HotkeyModel> Slots { get; set; }
    }
}