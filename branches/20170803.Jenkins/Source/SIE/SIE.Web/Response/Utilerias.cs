using System;
using System.Web.Script.Serialization;

namespace utilerias
{
	/// <summary>
	/// Descripción breve de Utilerias.
	/// </summary>
	public class Utilerias
	{
		public Utilerias()
		{
		}
		
        public static string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }


        private static bool ColumnaIgual(object A, object B)
        {

            if (A == DBNull.Value && B == DBNull.Value)
                return true;
            if (A == DBNull.Value || B == DBNull.Value)
                return false;
            return (A.Equals(B));
        }
		public static string Serializar(object instancia)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(instancia);
        }
        public static T Deserializar<T>(string cadena)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(cadena);
        }

    }
}
