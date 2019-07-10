using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Warkey.Core.ViewModels
{
    public class EditAutoChatViewModel: BaseViewModel
    {
        private Key key;
        private string message;

        public Key Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
    }
}
