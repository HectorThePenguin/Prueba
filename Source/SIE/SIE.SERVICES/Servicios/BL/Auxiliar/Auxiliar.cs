using System;
using SIE.Services.Info.Enums;
using System.ComponentModel;
namespace SIE.Services.Servicios.BL.Auxiliar
{
    public static class Auxiliar
    {
        /// <summary>
        /// Obtiene el atributo del enumerador
        /// </summary>
        /// <param name="claveCuenta"></param>
        /// <returns></returns>
        public static string ObtenerClaveCuenta(ClaveCuenta claveCuenta)
        {
            var result = string.Empty;
            dynamic customAttributes = claveCuenta.GetType().GetField(claveCuenta.ToString())
                                        .GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (customAttributes.Length > 0)
            {
                result =
                    customAttributes[0].Description;
            }
            return result;
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
    }
}
