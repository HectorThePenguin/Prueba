using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SIE.Base.Validadores
{
    public class ValidadorRango : ValidationRule
    {
        public string Mensaje { get; set; }
        public decimal RangoInicial { get; set; }
        public decimal RangoFinal { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (value != null && ((string)value).Trim() != string.Empty)
                {
                    decimal valor = Convert.ToDecimal(value);
                    if (valor <  RangoInicial || valor > RangoFinal)
                    {
                        return new ValidationResult(false, Mensaje);    
                    }                    
                }
                else
                {
                    return new ValidationResult(false, Mensaje);    
                }
                return new ValidationResult(true, null);
            }
            catch (Exception)
            {
                return new ValidationResult(false, Mensaje);
            }
        }
    }
}
