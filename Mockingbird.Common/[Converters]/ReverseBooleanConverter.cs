namespace Mockingbird.Common
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class ReverseBooleanConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool flag)
            {
                return !flag;
            }

            throw new ArgumentException(nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"ConvertBack is not supported for {GetType().FullName}");
        }
    }
}
