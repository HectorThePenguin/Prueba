using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class FleteInternoDAL : DALBase
    {
        /// <summary>
        /// Crea un registro flete interno
        /// </summary>
        /// <returns></returns>
        internal int Crear(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteInternoDAL.ObtenerParametrosCrearFleteInterno(fleteInternoInfo);
                int result = Create("FleteInterno_Crear", parameters);
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
        /// Metodo para obtener todos los contratos (activos o inactivos)
        /// </summary>
        /// <returns></returns>
        internal List<FleteInternoInfo> ObtenerFletesPorEstado(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parameters = AuxFleteInternoDAL.ObtenerParametrosObtenerFletesPorEstado(estatus);
                var ds = Retrieve("FleteInterno_ObtenerPorEstado", parameters);
                List<FleteInternoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteInternoDAL.ObtenerFletesPorEstado(ds);
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
        /// Obtiene una lista paginada de contrato
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<FleteInternoInfo> ObtenerPorPaginaFiltroDescripcionOrganizacion(PaginacionInfo pagina, FleteInternoInfo filtro)
        {
            ResultadoInfo<FleteInternoInfo> fleteInternoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxFleteInternoDAL.ObtenerParametrosObtenerPorPaginaFiltroDescripcionOrganizacion(pagina, filtro);
                DataSet ds = Retrieve("FleteInterno_ObtenerPorPaginaFiltroDescripcionOrganizacion", parameters);
                if (ValidateDataSet(ds))
                {
                    fleteInternoLista = MapFleteInternoDAL.ObtenerPorPaginaFiltroDescripcionOrganizacion(ds);
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
            return fleteInternoLista;
        }

        /// <summary>
        /// Actualizar flete interno detalle
        /// </summary>
        internal void ActualizarEstado(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxFleteInternoDAL.ObtenerParametrosActualizarEstado(fleteInternoInfo);
                Update("FleteInterno_ActualizarEstado", parameters);
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
        /// Obtiene un flete interno info por configuracion
        /// </summary>
        /// <returns></returns>
        internal FleteInternoInfo ObtenerPorConfiguracion(FleteInternoInfo fleteInternoInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxFleteInternoDAL.ObtenerParametrosObtenerPorConfiguracion(fleteInternoInfo);
                var ds = Retrieve("FleteInterno_ObtenerPorConfiguracion", parameters);
                FleteInternoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteInternoDAL.ObtenerPorConfiguracion(ds);
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
        /// Obtener costos por flete
        /// </summary>
        /// <returns></returns>
        internal List<FleteInternoCostoInfo> ObtenerCostosPorFleteConfiguracion(FleteInternoInfo fleteInternoInfo, ProveedorInfo proveedorInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxFleteInternoDAL.ObtenerCostosPorFleteConfiguracion(fleteInternoInfo, proveedorInfo);
                var ds = Retrieve("FleteInterno_ObtenerCostosPorFleteConfiguracion", parameters);
                List<FleteInternoCostoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapFleteInternoDAL.ObtenerCostosPorFleteConfiguracion(ds);
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
