﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Warkey.Core.ViewModels
{
    public class EditAutoChatViewModel: BaseViewModel
    {
        public Key Key { get; set; }
        public string Message { get; set; }
    }
}
