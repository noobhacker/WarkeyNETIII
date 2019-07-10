using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Warkey.Core.ViewModels;

namespace Warkey.Core.Items
{
    public class AutoChatItem : BaseViewModel
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
