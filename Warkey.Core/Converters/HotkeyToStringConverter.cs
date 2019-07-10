using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Warkey.Core.Items;

namespace Warkey.Core.Converters
{
    public class HotkeyToStringConverter : IValueConverter
    {
        private static KeyConverter kc = new KeyConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var str = "";
                var hotkey = (HotkeyItem)value;

                if (hotkey.Alt)
                    str += "ALT+";

                if(hotkey.Key != Key.LeftAlt)
                    str += kc.ConvertToString(hotkey.Key);

                return str;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
