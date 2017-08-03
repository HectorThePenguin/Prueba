using System;
using System.Globalization;
using System.Windows.Data;
using SIE.Services.Info.Enums;

namespace SIE.WinForm.Auxiliar 
{
    class ConvertidorActivo : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((EstatusEnum) value).ValorBooleanoDesdeEnum();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return bool.Parse(value.ToString()) ? EstatusEnum.Activo : EstatusEnum.Inactivo;
        }

        #endregion
    }
}