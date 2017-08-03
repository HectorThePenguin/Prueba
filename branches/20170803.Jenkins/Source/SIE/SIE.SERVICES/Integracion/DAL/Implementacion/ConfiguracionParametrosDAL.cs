using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ConfiguracionParametrosDAL : DALBase
    {
        internal ConfiguracionParametrosInfo ObtenerPorOrganizacionTipoParametroClave(ConfiguracionParametrosInfo parametro)
        {
            try
            {
                Logger.Info();
                var parameters =
                    AuxConfiguracionParametrosDAL.ObtenerParametrosObtenerPorOrganizacionTipoParametroClave(parametro);
                var ds = Retrieve("Parametro_ObtenerPorOrganizacionTipoParametroClave", parameters);
                ConfiguracionParametrosInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionParametrosDAL.ObtenerConfiguracionParametros(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene una lista de parametros por organizacion y tipo de parametros
        /// </summary>
        /// <param name="parametro"></param>
        /// <returns></returns>
        internal IList<ConfiguracionParametrosInfo> ObtenerPorOrganizacionTipoParametro(ConfiguracionParametrosInfo parametro)
        {
            try
            {
                Logger.Info();
                var parameters =
                    AuxConfiguracionParametrosDAL.ObtenerParametrosObtenerPorOrganizacionTipoParametro(parametro);
                var ds = Retrieve("Parametro_ObtenerPorOrganizacionTipoParametro", parameters);
                IList<ConfiguracionParametrosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionParametrosDAL.ObtenerListaConfiguracionParametros(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal IList<ConfiguracionParametrosInfo> ParametroObtenerPorTrampaTipoParametroClave(ConfiguracionParametrosInfo parametro, int trampaID)
        {
            try
            {
                Logger.Info();
                var parameters =
                    AuxConfiguracionParametrosDAL.ObtenerParametrosParametroObtenerPorTrampaTipoParametroClave(parametro, trampaID);
                var ds = Retrieve("Parametro_ObtenerPorTrampaTipoParametroClave", parameters);
                IList<ConfiguracionParametrosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionParametrosDAL.ObtenerListaConfiguracionParametros(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene una lista de paramatros por tipo de parametro de una trampa
        /// </summary>
        /// <param name="parametro">Se debe de proporcionar el tipo de paramtro</param>
        /// <param name="trampaID">Identificador de la trmapa</param>
        /// <returns></returns>
        internal IList<ConfiguracionParametrosInfo> ParametroObtenerPorTrampaTipoParametro(ConfiguracionParametrosInfo parametro, int trampaID)
        {
            try
            {
                Logger.Info();
                var parameters =
                    AuxConfiguracionParametrosDAL.ObtenerParametrosParametroObtenerPorTrampaTipoParametro(parametro, trampaID);
                var ds = Retrieve("Parametro_ObtenerPorTrampaTipoParametro", parameters);
                IList<ConfiguracionParametrosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionParametrosDAL.ObtenerListaConfiguracionParametros(ds);
                }
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
