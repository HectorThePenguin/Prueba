using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace SIE.WinForm.Validacion
{
    public class ValidacionesRegex : ValidationRule
    {
        #region PROPIEDADES
        private string pattern;
        private Regex regex;
        /// <summary>
        /// expresion regular
        /// </summary>
        public string Pattern
        {
            get { return pattern; }
            set
            {
                pattern = value;
                regex = new Regex(pattern, RegexOptions.IgnoreCase);
            }
        }
        /// <summary>
        /// Mensaje de error.
        /// </summary>
        public string ErrorMessage { set; get; }

        #endregion PROPIEDADES

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null || !regex.Match(value.ToString()).Success)
            {
                return new ValidationResult(false, ErrorMessage);
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
