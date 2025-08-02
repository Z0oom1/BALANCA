using System;
using System.Globalization;
using System.Windows.Data;

namespace BALANÇA
{
    public class BooleanToLightDarkConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked)
            {
                return isChecked ? "Dark" : "Light";
            }
            return "Light";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}