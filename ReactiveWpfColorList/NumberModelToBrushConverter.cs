using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ReactiveWpfColorList
{
    public class NumberModelToBrushConverter : IMultiValueConverter
    {
        private static SolidColorBrush _farNegativeBrush = Brushes.Red;
        private static SolidColorBrush _nearNegativeBrush = Brushes.Pink;
        private static SolidColorBrush _nearBrush = Brushes.White;
        private static SolidColorBrush _nearPositiveBrush = Brushes.LightGreen;
        private static SolidColorBrush _farPositiveBrush = Brushes.Green;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
                throw new ArgumentException();

            if (values[0] is not int n)
                throw new ArgumentException();

            if (values[1] is not double average)
                throw new ArgumentException();

            return (n - average) switch
            {
                <= -10 => _farNegativeBrush,
                > -10 and <= -2.5 => _nearNegativeBrush,
                > -2.5 and <= 2.5 => _nearBrush,
                > 2.5 and <= 10 => _nearPositiveBrush,
                >= 10 => _farPositiveBrush,
                _ => _nearBrush,
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
