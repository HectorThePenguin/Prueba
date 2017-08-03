using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxTipoCostoCentroDAL
    {
        /// <summary>
        /// Obtener parametros para obtener los tipos de costos de centros
        /// </summary>
        /// <param name="estatusEnum"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerTodos(EstatusEnum estatusEnum)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Activo", estatusEnum},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
