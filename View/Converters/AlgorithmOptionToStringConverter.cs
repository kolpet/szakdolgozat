using System;
using System.Globalization;
using System.Windows.Data;
using Szakdolgozat.ViewModel.Controls;

namespace Szakdolgozat.View.Converters
{
    public class AlgorithmOptionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is AlgorithmOptionGaleShapley)
                return "galeshapley";
            if(value is AlgorithmOptionGenetic)
                return "genetic";
            else
                return "none";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case "galeshapley":
                    return typeof(AlgorithmOptionGaleShapley);
                case "genetic":
                    return typeof(AlgorithmOptionGenetic);
                default:
                    return null;
            }
        }
    }
}
