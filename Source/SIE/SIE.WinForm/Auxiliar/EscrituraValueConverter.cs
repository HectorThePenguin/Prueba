using System;
using System.Globalization;
using System.Windows.Data;

namespace SIE.WinForm.Auxiliar
{
    internal class EscrituraValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object resultado = true;
            if (value == null || parameter == null)
            {
                return false;
            }
            int valorEntero;
            int.TryParse(value.ToString(), out valorEntero);
            if (valorEntero != 1)
            {
                resultado = false;
            }
            return resultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return null;
            }
            bool valor;
            bool.TryParse(value.ToString(), out valor);
            return valor ? 1 : 0;
        }
    }
}
