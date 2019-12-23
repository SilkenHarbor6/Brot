using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Brot.Converters
{
    public class BoolToStringSiguiendo : Xamarin.Forms.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Siguiendo" : "Seguir";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
