using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Team4_YelpProject
{
    public class BoolVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool hasText = (bool)values[0];
            bool hasFocus = (bool)values[1];

            if(!hasText || hasFocus)
            {
                return Visibility.Hidden; //.Collapsed
            }

            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
