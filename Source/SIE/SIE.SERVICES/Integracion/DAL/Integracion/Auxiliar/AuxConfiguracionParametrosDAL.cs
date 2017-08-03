using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    /// <summary>
    /// Clase para el la obtencion de parametros necesarios para la ejecucion de procedimientos
    /// para obtener la configuracion de parametros
    /// </summary>
    internal class AuxConfiguracionParametrosDAL
    {
        /// <summary>
        /// Obtiene los parametros para ejecutar ObtenerPorOrganizacionTipoParametroClave
        /// </summary>
        /// <param name="parametro">Es necesario proporcionar los datos:
        /// OrganizacionID
        /// TipoParametro
        /// Clave</param>
        /// <returns>Diccionario de parametros</returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorOrganizacionTipoParametroClave(ConfiguracionParametrosInfo parametro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", parametro.OrganizacionID},
                            {"@TipoParametroID", parametro.TipoParametro},
                            {"@Clave", parametro.Clave}
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
        /// Obtiene los parametros para ejecutar ObtenerPorOrganizacionTipoParametro
        /// </summary>
        /// <param name="parametro">Es necesario proporcionar los datos:
        /// OrganizacionID
        /// TipoParametro</param>
        /// <returns>Diccionario de parametros</returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorOrganizacionTipoParametro(ConfiguracionParametrosInfo parametro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", parametro.OrganizacionID},
                            {"@TipoParametroID", parametro.TipoParametro}
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
        /// Obtiene los parametros para ejecutar ObtenerPorTrampaTipoParametroClave
        /// </summary>
        /// <param name="parametro">Es necesario proporcionar los datos
        /// TipoParametro
        /// Clave</param>
        /// <param name="trampaID">Trampa</param>
        /// <returns>Diccionario de parametros</returns>
        internal static Dictionary<string, object> ObtenerParametrosParametroObtenerPorTrampaTipoParametroClave(ConfiguracionParametrosInfo parametro, int trampaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TrampaID", trampaID},
                            {"@TipoParametroID", parametro.TipoParametro},
                            {"@Clave", parametro.Clave}
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
        /// Obtiene los parametros para ejecutar ObtenerPorTrampaTipoParametro
        /// </summary>
        /// <param name="parametro">Es necesario proporcionar los datos
        /// TrampaID
        /// TipoParametro</param>
        /// <param name="trampaID">Trampa</param>
        /// <returns>Diccionario de parametros</returns>
        internal static Dictionary<string, object> ObtenerParametrosParametroObtenerPorTrampaTipoParametro(ConfiguracionParametrosInfo parametro, int trampaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@TrampaID", trampaID},
                            {"@TipoParametroID", parametro.TipoParametro}
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
