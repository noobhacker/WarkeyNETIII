using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarkeyNETIII.ViewModels
{
    public class WarkeyViewModel : BaseViewModel
    {
        private string slot1;
        private string slot2;
        private string slot3;
        private string slot4;
        private string slot5;
        private string slot6;

        public string Slot1
        {
            get
            {
                return slot1;
            }

            set
            {
                slot1 = value;
                OnPropertyChanged();
            }
        }

        public string Slot2
        {
            get
            {
                return slot2;
            }

            set
            {
                slot2 = value;
                OnPropertyChanged();
            }
        }

        public string Slot3
        {
            get
            {
                return slot3;
            }

            set
            {
                slot3 = value;
                OnPropertyChanged();
            }
        }

        public string Slot4
        {
            get
            {
                return slot4;
            }

            set
            {
                slot4 = value;
                OnPropertyChanged();
            }
        }

        public string Slot5
        {
            get
            {
                return slot5;
            }

            set
            {
                slot5 = value;
                OnPropertyChanged();
            }
        }

        public string Slot6
        {
            get
            {
                return slot6;
            }

            set
            {
                slot6 = value;
                OnPropertyChanged();
            }
        }
    }
}
