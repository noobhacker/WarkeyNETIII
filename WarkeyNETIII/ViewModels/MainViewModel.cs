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
        public const string MenuHighlightedColor = "#FFBEE6FD";

        private string status;
        private string warkeyBackground;
        private string autoChatBackground;
        private string optimizeBackground;
        private string aboutBackground;

        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
                OnPropertyChanged();
            }
        }

        public string WarkeyBackground
        {
            get
            {
                return warkeyBackground;
            }

            set
            {
                warkeyBackground = value;
                OnPropertyChanged();
            }
        }

        public string AutoChatBackground
        {
            get
            {
                return autoChatBackground;
            }

            set
            {
                autoChatBackground = value;
                OnPropertyChanged();
            }
        }

        public string OptimizeBackground
        {
            get
            {
                return optimizeBackground;
            }

            set
            {
                optimizeBackground = value;
                OnPropertyChanged();
            }
        }

        public string AboutBackground
        {
            get
            {
                return aboutBackground;
            }

            set
            {
                aboutBackground = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Status = "Idle";
        }
               
    }
}
