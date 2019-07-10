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
        public Key Key { get; set; }
        public string Message { get; set; }
    }
}
