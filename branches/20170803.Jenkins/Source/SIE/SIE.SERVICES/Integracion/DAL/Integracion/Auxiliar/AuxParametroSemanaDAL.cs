using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxParametroSemanaDAL
    {
        /// <summary>
        /// Obtener prametro semana por descripcion
        /// </summary>
        /// <param name="parametroSemanaInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerParametroSemanaPorDescripcion(ParametroSemanaInfo parametroSemanaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Descripcion", parametroSemanaInfo.Descripcion}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtener numero de semana
        /// </summary>
        /// <param name="fechaCalcular"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerNumeroSemana(DateTime fechaCalcular)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@FechaInicial", fechaCalcular}
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
