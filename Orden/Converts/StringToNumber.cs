using System;
using System.Globalization;
using System.Windows.Data;

namespace Orden.Converts
{
    public class StringToNumber : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value.ToString() == "" || value.ToString() == "0.00")
                {
                    return 0;
                }
            }
            return value.ToString();
        }
    }
}
