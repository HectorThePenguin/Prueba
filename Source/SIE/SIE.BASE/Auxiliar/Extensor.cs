using System;

namespace SIE.Base.Auxiliar
{
    public static class Extensor
    {
        /// <summary>
        /// Helper para validar os DBNull de una datatable
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"> </param>
        /// <returns></returns>
        public static string HelperDBNull(this object value, string defaultValue)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return defaultValue;
            }
            return (string)value;
        }

        /// <summary>
        /// Helper para validar os DBNull de una datatable
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"> </param>
        /// <returns></returns>
        public static int HelperDBNull(this object value, int defaultValue)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return defaultValue;
            }
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Helper para validar os DBNull de una datatable
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"> </param>
        /// <returns></returns>
        public static decimal HelperDBNull(this object value, decimal defaultValue)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return defaultValue;
            }
            return Convert.ToDecimal(value);
        }
        
        /// <summary>
        /// Helper para validar os DBNull de una datatable
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"> </param>
        /// <returns></returns>
        public static double HelperDBNull(this object value, double defaultValue )
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return defaultValue;
            }
            return Convert.ToDouble(value);
        }

        /// <summary>
        /// Helper para validar os DBNull de una datatable
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"> </param>
        /// <returns></returns>
        public static char HelperDBNull(this object value, char defaultValue)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return defaultValue;
            }
            return Convert.ToChar(value);
        }

        public static T FromDB<T>(this object value)
        {
            return value == DBNull.Value ? default(T) : (T)value;
        }

        //public static T FromDB<T>(object value)
        //{
        //    return value == DBNull.Value ? default(T) : (T)value;
        //}

        public static object ToDB<T>(T value)
        {
            return value == null ? (object)DBNull.Value : value;
        }
    }
}
