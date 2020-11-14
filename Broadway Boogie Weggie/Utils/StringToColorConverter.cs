using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Drawing;
using System.Globalization;

namespace Broadway_Boogie_Weggie.Utils
{
    public class StringToColorConverter : IValueConverter
    {
        private readonly Dictionary<string, string> colors = new Dictionary<string, string>() {
            { "yellow", Color.Yellow.Name },
            { "blue", Color.Blue.Name },
            { "orange", Color.Orange.Name },
            { "grey", Color.Gray.Name },
            { "purple", Color.Purple.Name },
            { "brown", Color.Brown.Name },
            { "pink", Color.Pink.Name },
            { "black", Color.Black.Name },
            { "red", Color.Red.Name },
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = (string)value;
            if (colors.ContainsKey(color))
            {
                return colors[color];
            }
            else
            {
                return colors["brown"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
