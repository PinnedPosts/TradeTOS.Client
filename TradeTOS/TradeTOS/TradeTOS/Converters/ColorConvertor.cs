using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TradeTOS.Converters
{
    public class ColorConvertor : IValueConverter
    {

        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Color)
            {
                return ColorTool.DarkerColor((Color)value);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ColorTool
    {
        static Dictionary<int, string> colors = new Dictionary<int, string>
        {
            { 0, "#BC215B" },
            { 1, "#AB1958" },
            { 2, "#86124F" },
            { 3, "#493954" },
            { 4, "#554663" },
            { 5, "#5B5487" },
            { 6, "#4D75A8" },
            { 7, "#5199CB" },
            { 8, "#58BAF2" }
        };

        public static Color GetMetroColor(int index)
        {
            index = index % 9;
            string hexCode = colors[index];
            return Color.FromHex(hexCode);
        }

        public static Color DarkerColor(Color color)
        {
            return Color.FromRgba(
                color.R * 0.75, color.G, color.B, color.A);
        }


    }
}
