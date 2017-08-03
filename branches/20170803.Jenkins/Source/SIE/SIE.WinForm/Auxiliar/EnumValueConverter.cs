using SIE.Services.Info.Enums;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SIE.WinForm.Auxiliar
{
    public class EnumValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;   
            }                
            return value.ToString() == parameter.ToString();   
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return null;
            }
            return string.Compare(value.ToString(), "true", StringComparison.CurrentCultureIgnoreCase) == 0
                       ? Automatico.Si
                       : Automatico.No;
        }
    }
}
