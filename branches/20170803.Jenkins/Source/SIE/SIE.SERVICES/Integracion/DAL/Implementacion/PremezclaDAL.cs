using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System.Data;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class PremezclaDAL : DALBase
    {
        /// <summary>
        /// Obtiene la lista de premezclas por organizacion
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        internal List<PremezclaInfo> ObtenerPorOrganizacion(OrganizacionInfo organizacion)
        {
            try
            {
                List<PremezclaInfo> result = null;
                try
                {
                    Logger.Info();
                    Dictionary<string, object> parametros =
                        AuxPremezclaDAL.ObtenerParametrosPorOrganizacion(organizacion);
                    DataSet ds = Retrieve("Premezcla_ObtenerPorOrganizacionID", parametros);
                    if (ValidateDataSet(ds))
                    {
                        result = MapPremezclaDAL.ObtenerTodos(ds);
                    }
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
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtiene la lista de premezclas por organizacion
        /// </summary>
        /// <param name="premezclaInfo"></param>
        /// <returns></returns>
        internal PremezclaInfo ObtenerPorProductoIdOrganizacionId(PremezclaInfo premezclaInfo)
        {
            try
            {
                PremezclaInfo result = null;
                try
                {
                    Logger.Info();
                    Dictionary<string, object> parametros =
                        AuxPremezclaDAL.ObtenerParametrosObtenerPorProductoIdPorOrganizacionId(premezclaInfo);
                    DataSet ds = Retrieve("Premezcla_ObtenerPorProductoIDOrganizacionID", parametros);
                    if (ValidateDataSet(ds))
                    {
                        result = MapPremezclaDAL.ObtenerPorProductoIdOrganizacionId(ds);
                    }
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
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Crea un registro premezcla
        /// </summary>
        /// <returns></returns>
        internal int Crear(PremezclaInfo premezclaInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxPremezclaDAL.ObtenerParametrosCrearPremezcla(premezclaInfo);
                int result = Create("Premezcla_Crear", parameters);
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
        /// Obtiene una lista de premezclas con sus subproductos
        /// </summary>
        /// <param name="organizacion"></param>
        /// <returns></returns>
        internal List<PremezclaInfo> ObtenerPorOrganizacionDetalle(OrganizacionInfo organizacion)
        {
            List<PremezclaInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxPremezclaDAL.ObtenerParametrosPorOrganizacionDetalle(organizacion);
                DataSet ds = Retrieve("Premezcla_ObtenerPorOrganizacion", parametros);
                if (ValidateDataSet(ds))
                {
                    result = MapPremezclaDAL.ObtenerPorOrganizacionDetalle(ds);
                }
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
            return result;
        }
    }
}
