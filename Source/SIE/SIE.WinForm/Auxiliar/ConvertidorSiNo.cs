using System;
using System.Globalization;
using System.Windows.Data;

namespace SIE.WinForm.Auxiliar
{
    public class ConvertidorSiNo : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valor;
            bool.TryParse(value.ToString(), out valor);
            if (valor)
            {
                return Properties.Resources.ConvertidorSINO_Si;
            }
            return Properties.Resources.ConvertidorSINO_No;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool valor;
            bool.TryParse(value.ToString(), out valor);
            if (valor)
            {
                return Properties.Resources.ConvertidorSINO_Si;
            }
            return Properties.Resources.ConvertidorSINO_No;
        }

        #endregion
    }
}