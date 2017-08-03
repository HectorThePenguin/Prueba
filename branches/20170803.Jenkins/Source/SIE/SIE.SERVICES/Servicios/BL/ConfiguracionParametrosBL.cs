using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    /// <summary>
    /// Capa de negocios para el tratamiento de los parametros de configuracion
    /// </summary>
    internal class ConfiguracionParametrosBL
    {
        /// <summary>
        /// Obtine parametros por organizacion, tipo parametro y clave
        /// </summary>
        /// <param name="parametro">Es necesario los datos OrganizacionID,TipoParametro y Clave</param>
        /// <returns>Configuracion</returns>
        internal ConfiguracionParametrosInfo ObtenerPorOrganizacionTipoParametroClave(ConfiguracionParametrosInfo parametro)
        {
            ConfiguracionParametrosInfo result;
            try
            {
                Logger.Info();
                var configuracionDal = new ConfiguracionParametrosDAL();
                result = configuracionDal.ObtenerPorOrganizacionTipoParametroClave(parametro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        /// <summary>
        /// Obtiene la configuracion por organizacion y tipo de parametros
        /// </summary>
        /// <param name="parametro">Es necesario proporcionar OrganizacionID y TipoParametros</param>
        /// <returns>Configuracion</returns>
        internal IList<ConfiguracionParametrosInfo> ObtenerPorOrganizacionTipoParametro(ConfiguracionParametrosInfo parametro)
        {
            IList<ConfiguracionParametrosInfo> result;
            try
            {
                Logger.Info();
                var configuracionDal = new ConfiguracionParametrosDAL();
                result = configuracionDal.ObtenerPorOrganizacionTipoParametro(parametro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
        /// <summary>
        /// Obtiene la lista de configuracion para una trampa, tipo de parametro y clave
        /// </summary>
        /// <param name="parametro">Es necesario proporcionar TipoParametro, clave</param>
        /// <param name="trampaID">Trampa</param>
        /// <returns>Configuracion</returns>
        internal IList<ConfiguracionParametrosInfo> ParametroObtenerPorTrampaTipoParametroClave(ConfiguracionParametrosInfo parametro, int trampaID)
        {
            IList<ConfiguracionParametrosInfo> result;
            try
            {
                Logger.Info();
                var configuracionDal = new ConfiguracionParametrosDAL();
                result = configuracionDal.ParametroObtenerPorTrampaTipoParametroClave(parametro, trampaID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        internal IList<ConfiguracionParametrosInfo> ParametroObtenerPorTrampaTipoParametro(ConfiguracionParametrosInfo parametro, int trampaID)
        {
            IList<ConfiguracionParametrosInfo> result;
            try
            {
                Logger.Info();
                var configuracionDal = new ConfiguracionParametrosDAL();
                result = configuracionDal.ParametroObtenerPorTrampaTipoParametro(parametro, trampaID);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
    }
}
