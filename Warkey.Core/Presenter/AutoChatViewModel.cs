using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Warkey.Core.Presenter
{
    public class AutoChatViewModel : BaseViewModel
    {
        public Key Key { get; set; }
        public string Message { get; set; }
    }
}
