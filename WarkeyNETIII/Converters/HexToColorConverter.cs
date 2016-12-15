using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WarkeyNETIII.Converters
{
    public class HexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var color = (string)value;
                if (color != "")
                {
                    return new SolidColorBrush(Color.FromArgb(255,
                                                              System.Convert.ToByte(color.Substring(1, 2), 16),
                                                              System.Convert.ToByte(color.Substring(3, 2), 16),
                                                              System.Convert.ToByte(color.Substring(5, 2), 16)));
                }
                else
                {
                    return new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
