using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
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
    internal class RetencionDAL : DALBase
    {
        /// <summary>
        ///     Metodo para Crear un nuevo registro de Retencion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal void Crear(RetencionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRetencionDAL.ObtenerParametrosCrear(info);
                Create("Retencion_Crear", parameters);
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
        ///     Metodo para Actualizar un nuevo registro de Retencion
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(RetencionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRetencionDAL.ObtenerParametrosActualizar(info);
                Update("Retencion_Actualizar", parameters);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<RetencionInfo> ObtenerPorPagina(PaginacionInfo pagina, RetencionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxRetencionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[Retencion_ObtenerPorPagina]", parameters);
                ResultadoInfo<RetencionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRetencionDAL.ObtenerPorPagina(ds);
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
        ///     Obtiene un registro de Entrada Ganado Costeo por su EntradaId
        /// </summary>
        /// <returns></returns>
        internal IList<RetencionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Retencion_ObtenerTodos");
                IList<RetencionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRetencionDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista de Retencion filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<RetencionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCamionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Retencion_ObtenerTodos", parameters);
                IList<RetencionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRetencionDAL.ObtenerTodos(ds);
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
        ///     Obtiene un registro de Retencion
        /// </summary>
        /// <param name="retencionID">Identificador de la Retencion</param>
        /// <returns></returns>
        internal RetencionInfo ObtenerPorID(int retencionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRetencionDAL.ObtenerParametrosPorID(retencionID);
                DataSet ds = Retrieve("Retencion_ObtenerPorID", parameters);
                RetencionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRetencionDAL.ObtenerPorID(ds);
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
        ///     Obtiene un lista de Retenciones con Costo Id
        /// </summary>
        /// <returns></returns>
        internal IList<RetencionInfo> ObtenerRetencionesConCosto(IList<int> costos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRetencionDAL.ObtenerParametrosRetencionesConCosto(costos);
                DataSet ds = Retrieve("Retencion_ObtenerRetencionesConCosto", parameters);
                IList<RetencionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRetencionDAL.ObtenerRetencionesConCosto(ds);
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

