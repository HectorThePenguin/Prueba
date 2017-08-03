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
    internal class EstadoComederoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de EstadoComedero
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(EstadoComederoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEstadoComederoDAL.ObtenerParametrosCrear(info);
                int result = Create("EstadoComedero_Crear", parameters);
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
        /// Metodo para actualizar un registro de EstadoComedero
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(EstadoComederoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEstadoComederoDAL.ObtenerParametrosActualizar(info);
                Update("EstadoComedero_Actualizar", parameters);
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
        internal ResultadoInfo<EstadoComederoInfo> ObtenerPorPagina(PaginacionInfo pagina, EstadoComederoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxEstadoComederoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("EstadoComedero_ObtenerPorPagina", parameters);
                ResultadoInfo<EstadoComederoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEstadoComederoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de EstadoComedero
        /// </summary>
        /// <returns></returns>
        internal IList<EstadoComederoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("EstadoComedero_ObtenerTodos");
                IList<EstadoComederoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEstadoComederoDAL.ObtenerTodos(ds);
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
        internal IList<EstadoComederoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEstadoComederoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("EstadoComedero_ObtenerTodos", parameters);
                IList<EstadoComederoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEstadoComederoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de EstadoComedero
        /// </summary>
        /// <param name="estadoComederoID">Identificador de la EstadoComedero</param>
        /// <returns></returns>
        internal EstadoComederoInfo ObtenerPorID(int estadoComederoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEstadoComederoDAL.ObtenerParametrosPorID(estadoComederoID);
                DataSet ds = Retrieve("EstadoComedero_ObtenerPorID", parameters);
                EstadoComederoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEstadoComederoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de EstadoComedero
        /// </summary>
        /// <param name="descripcion">Descripción de la EstadoComedero</param>
        /// <returns></returns>
        internal EstadoComederoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxEstadoComederoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("EstadoComedero_ObtenerPorDescripcion", parameters);
                EstadoComederoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapEstadoComederoDAL.ObtenerPorDescripcion(ds);
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

