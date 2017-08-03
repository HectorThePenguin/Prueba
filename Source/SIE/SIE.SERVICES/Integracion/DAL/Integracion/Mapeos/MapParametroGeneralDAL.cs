using System;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapParametroGeneralDAL
    {
        /// <summary>
        /// Obtiene una entidad de parametro general
        /// por su clave de parametro
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        internal static ParametroGeneralInfo ObtenerPorClaveParametro(IDataReader reader)
        {
            try
            {
                Logger.Info();
                ParametroGeneralInfo entidad = null;
                while (reader.Read())
                {
                    entidad =
                        new ParametroGeneralInfo
                        {
                            ParametroGeneralID = Convert.ToInt32(reader["ParametroGeneralID"]),
                            ParametroID = Convert.ToInt32(reader["ParametroID"]),
                            Valor = Convert.ToString(reader["Valor"]),
                            Activo = Convert.ToBoolean(reader["Activo"]).BoolAEnum(),
                        };
                }
                return entidad;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
