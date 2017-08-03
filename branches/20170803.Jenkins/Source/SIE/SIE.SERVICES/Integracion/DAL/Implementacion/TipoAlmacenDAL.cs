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
    internal class TipoAlmacenDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de TipoAlmacen
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TipoAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoAlmacenDAL.ObtenerParametrosCrear(info);
                int result = Create("TipoAlmacen_Crear", parameters);
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
        /// Metodo para actualizar un registro de TipoAlmacen
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TipoAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoAlmacenDAL.ObtenerParametrosActualizar(info);
                Update("TipoAlmacen_Actualizar", parameters);
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
        internal ResultadoInfo<TipoAlmacenInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoAlmacenInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoAlmacenDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TipoAlmacen_ObtenerPorPagina", parameters);
                ResultadoInfo<TipoAlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoAlmacenDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de TipoAlmacen
        /// </summary>
        /// <returns></returns>
        internal IList<TipoAlmacenInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoAlmacen_ObtenerTodos");
                IList<TipoAlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoAlmacenDAL.ObtenerTodos(ds);
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
        internal IList<TipoAlmacenInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoAlmacenDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoAlmacen_ObtenerTodos", parameters);
                IList<TipoAlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoAlmacenDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de TipoAlmacen
        /// </summary>
        /// <param name="tipoAlmacenID">Identificador de la TipoAlmacen</param>
        /// <returns></returns>
        internal TipoAlmacenInfo ObtenerPorID(int tipoAlmacenID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoAlmacenDAL.ObtenerParametrosPorID(tipoAlmacenID);
                DataSet ds = Retrieve("TipoAlmacen_ObtenerPorID", parameters);
                TipoAlmacenInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoAlmacenDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de TipoAlmacen
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoAlmacen</param>
        /// <returns></returns>
        internal TipoAlmacenInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoAlmacenDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoAlmacen_ObtenerPorDescripcion", parameters);
                TipoAlmacenInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoAlmacenDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoAlmacenInfo> ObtenerPorPaginaTiposAlmacen(PaginacionInfo pagina, TipoAlmacenInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoAlmacenDAL.ObtenerParametrosPorPaginaTiposAlmacen(pagina, filtro);
                DataSet ds = Retrieve("TipoAlmacen_ObtenerPorPaginaTiposAlmacen", parameters);
                ResultadoInfo<TipoAlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoAlmacenDAL.ObtenerPorPaginaTipoAlmacen(ds);
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

