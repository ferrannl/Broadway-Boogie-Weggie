using System;
using System.Globalization;
using System.Windows.Data;

namespace Broadway_Boogie_Weggie.Converters
{
    public class GalleryPositionToRenderPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double position = (double)value * (800 / 53);
            return position;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
