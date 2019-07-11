using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Warkey.Core.Presenter
{
    public class BaseViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }    
}
