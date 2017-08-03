using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    /// <summary>
    /// Clase auxiliar para el DAL de Problema
    /// </summary>
    internal class AuxProblemaDAL
    {
        /// <summary>
        /// Obtiene Problema por Id
        /// </summary>
        /// <param name="problemaID">Identificador del problema</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerProblemaPorID(int problemaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                            {"@ProblemaID", problemaID}
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
        /// Obtiene los problemas de una deteccion
        /// </summary>
        /// <param name="deteccion">Se debe de indicar el DetecionID y EstatusDeteccion </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerProblemasDeteccion(AnimalDeteccionInfo deteccion)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                    {
                        {"@DetecionID", deteccion.DeteccionID},
                        {"@EstatusDeteccion", deteccion.EstatusDeteccion}
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
