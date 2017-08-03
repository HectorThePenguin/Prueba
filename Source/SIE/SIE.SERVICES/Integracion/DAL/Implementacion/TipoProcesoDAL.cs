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
    internal class TipoProcesoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de TipoProceso
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TipoProcesoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoProcesoDAL.ObtenerParametrosCrear(info);
                int result = Create("TipoProceso_Crear", parameters);
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
        /// Metodo para actualizar un registro de TipoProceso
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TipoProcesoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoProcesoDAL.ObtenerParametrosActualizar(info);
                Update("TipoProceso_Actualizar", parameters);
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
        internal ResultadoInfo<TipoProcesoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoProcesoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoProcesoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TipoProceso_ObtenerPorPagina", parameters);
                ResultadoInfo<TipoProcesoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoProcesoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de TipoProceso
        /// </summary>
        /// <returns></returns>
        internal IList<TipoProcesoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoProceso_ObtenerTodos");
                IList<TipoProcesoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoProcesoDAL.ObtenerTodos(ds);
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
        internal IList<TipoProcesoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoProcesoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoProceso_ObtenerTodos", parameters);
                IList<TipoProcesoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoProcesoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de TipoProceso
        /// </summary>
        /// <param name="tipoProcesoID">Identificador de la TipoProceso</param>
        /// <returns></returns>
        internal TipoProcesoInfo ObtenerPorID(int tipoProcesoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoProcesoDAL.ObtenerParametrosPorID(tipoProcesoID);
                DataSet ds = Retrieve("TipoProceso_ObtenerPorID", parameters);
                TipoProcesoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoProcesoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de TipoProceso
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoProceso</param>
        /// <returns></returns>
        internal TipoProcesoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoProcesoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoProceso_ObtenerPorDescripcion", parameters);
                TipoProcesoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoProcesoDAL.ObtenerPorDescripcion(ds);
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

        internal int ObtenerPorOrganizacion(int organizacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoProcesoDAL.ObtenerParametrosObtenerPorOrganizacion(organizacionId);
                int result = Create("CorteTransferencia_ObtenerTipoProceso", parameters);
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

