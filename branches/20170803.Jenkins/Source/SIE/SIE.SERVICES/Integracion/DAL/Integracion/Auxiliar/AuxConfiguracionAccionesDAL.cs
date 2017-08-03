using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    /// <summary>
    /// Clase auxiliar de configuracion de acciones
    /// </summary>
    internal class AuxConfiguracionAccionesDAL
    {
        /// <summary>
        /// Obtiene los parametros para obterner la acciones por codigo
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorCodigo(AccionesSIAPEnum codigo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@Codigo", codigo.ToString()}
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para actualizacion de la ejecucion
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosParaActualizarEjecucion(ConfiguracionAccionesInfo tarea)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@Codigo", tarea.Codigo},
                    {"@Fecha", tarea.FechaUltimaEjecucion}
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
        /// <summary>
        /// Obtener los parametros para actualizar fecha de ejecucion
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarFechaEjecucion(ConfiguracionAccionesInfo tarea)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@Codigo", tarea.Codigo},
                    {"@FechaEjecucion",tarea.FechaEjecucion}
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
