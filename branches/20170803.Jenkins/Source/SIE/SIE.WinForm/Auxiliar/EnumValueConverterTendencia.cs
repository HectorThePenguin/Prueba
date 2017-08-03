using System;
using System.Globalization;
using System.Windows.Data;
using SIE.Services.Info.Enums;

namespace SIE.WinForm.Auxiliar
{
    internal class EnumValueConverterTendencia : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object resultado;
            if (value == null || parameter == null)
            {
                return false;
            }
            if (string.Compare(value.ToString(), "igual", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                resultado = Tendencia.Igual;
            }
            else
            {
                if (string.Compare(value.ToString(), "mayor", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    resultado = Tendencia.Mayor;
                }
                else
                {
                    resultado = Tendencia.Menor;
                }
            }
            return resultado;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object resultado;
            if (value == null || parameter == null)
            {
                return null;
            }
            if (string.Compare(value.ToString(), "igual", StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                resultado = Tendencia.Igual;
            }
            else
            {
                if (string.Compare(value.ToString(), "mayor", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    resultado = Tendencia.Mayor;
                }
                else
                {
                    resultado = Tendencia.Menor;
                }
            }
            return resultado;
        }
    }
}
