using System;
using System.Globalization;
using System.Windows.Controls;

namespace SIE.Base.Validadores
{
    public class TextValidador : ValidationRule
    {
        public string Mensaje { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
               if (value != null && ((string) value).Trim() == string.Empty)
                {
                    return new ValidationResult(false, Mensaje);
                }
                return new ValidationResult(true, null);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Caracteres Invalidos" + e.Message);
            }
        }
    }
}