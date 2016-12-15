using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarkeyNETIII.Items;

namespace WarkeyNETIII.ViewModels
{
    public class WarkeyViewModel : BaseViewModel
    {
        private HotkeyItem slot1;
        private HotkeyItem slot2;
        private HotkeyItem slot3;
        private HotkeyItem slot4;
        private HotkeyItem slot5;
        private HotkeyItem slot6;

        public HotkeyItem Slot1
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

        public HotkeyItem Slot2
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

        public HotkeyItem Slot3
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

        public HotkeyItem Slot4
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

        public HotkeyItem Slot5
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

        public HotkeyItem Slot6
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
