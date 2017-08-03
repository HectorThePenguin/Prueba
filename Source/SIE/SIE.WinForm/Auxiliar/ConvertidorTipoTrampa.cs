using System;
using System.Globalization;
using System.Windows.Data;
using SIE.Services.Info.Enums;

namespace SIE.WinForm.Auxiliar
{
    public class ConvertidorTipoTrampa : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (TipoTrampa) char.Parse(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((TipoTrampa) value).GetHashCode();
        }

        #endregion
    }
}