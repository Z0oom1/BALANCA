using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BALANÇA
{
    public class CheckedToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var primaryColor = (Color)App.Current.FindResource("PrimaryHueMidColor");
                var defaultColor = (Color)ColorConverter.ConvertFromString("#FFE0E0E0");

                if (value is bool isChecked && isChecked)
                {
                    return new SolidColorBrush(primaryColor);
                }
                return new SolidColorBrush(defaultColor);
            }
            catch
            {
                return new SolidColorBrush(Colors.LightGray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}