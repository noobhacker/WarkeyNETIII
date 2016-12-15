using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WarkeyNETIII.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string title;

        public string Title
        {
            get
            {
                if (title == "")
                    return "Warkey.NET III";
                else
                    return $"Warkey.NET III - {title}";
            }

            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

    }
}
