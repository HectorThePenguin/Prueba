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
    internal class CancelacionMovimientoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CancelacionMovimiento
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal CancelacionMovimientoInfo Crear(CancelacionMovimientoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCancelacionMovimientoDAL.ObtenerParametrosCrear(info);
                DataSet ds = Retrieve("CancelacionMovimiento_Crear", parameters);
                CancelacionMovimientoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCancelacionMovimientoDAL.ObtenerCrear(ds);
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
        /// Metodo para actualizar un registro de CancelacionMovimiento
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CancelacionMovimientoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCancelacionMovimientoDAL.ObtenerParametrosActualizar(info);
                Update("CancelacionMovimiento_Actualizar", parameters);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<CancelacionMovimientoInfo> ObtenerPorPagina(PaginacionInfo pagina, CancelacionMovimientoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCancelacionMovimientoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CancelacionMovimiento_ObtenerPorPagina", parameters);
                ResultadoInfo<CancelacionMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCancelacionMovimientoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CancelacionMovimiento
        /// </summary>
        /// <returns></returns>
        internal IList<CancelacionMovimientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CancelacionMovimiento_ObtenerTodos");
                IList<CancelacionMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCancelacionMovimientoDAL.ObtenerTodos(ds);
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
        /// Obtiene una lista filtrando por el estatus Activo = 1, Inactivo = 0
        /// </summary>
        /// <returns></returns>
        internal IList<CancelacionMovimientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCancelacionMovimientoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CancelacionMovimiento_ObtenerTodos", parameters);
                IList<CancelacionMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCancelacionMovimientoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CancelacionMovimiento
        /// </summary>
        /// <param name="cancelacionMovimientoID">Identificador de la CancelacionMovimiento</param>
        /// <returns></returns>
        internal CancelacionMovimientoInfo ObtenerPorID(int cancelacionMovimientoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCancelacionMovimientoDAL.ObtenerParametrosPorID(cancelacionMovimientoID);
                DataSet ds = Retrieve("CancelacionMovimiento_ObtenerPorID", parameters);
                CancelacionMovimientoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCancelacionMovimientoDAL.ObtenerPorID(ds);
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

