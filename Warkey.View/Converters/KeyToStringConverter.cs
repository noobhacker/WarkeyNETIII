using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Warkey.Core.Converters
{
    public class KeyToStringConverter : IValueConverter
    {
        private static KeyConverter kc = new KeyConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (int)value;
            return kc.ConvertToString(input);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
