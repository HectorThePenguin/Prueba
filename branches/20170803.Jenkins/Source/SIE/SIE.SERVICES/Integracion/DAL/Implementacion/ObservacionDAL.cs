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
    internal class ObservacionDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Observacion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(ObservacionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxObservacionDAL.ObtenerParametrosCrear(info);
                int result = Create("Observacion_Crear", parameters);
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
        /// Metodo para actualizar un registro de Observacion
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(ObservacionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxObservacionDAL.ObtenerParametrosActualizar(info);
                Update("Observacion_Actualizar", parameters);
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
        internal ResultadoInfo<ObservacionInfo> ObtenerPorPagina(PaginacionInfo pagina, ObservacionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxObservacionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Observacion_ObtenerPorPagina", parameters);
                ResultadoInfo<ObservacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapObservacionDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Observacion
        /// </summary>
        /// <returns></returns>
        internal IList<ObservacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Observacion_ObtenerTodos");
                IList<ObservacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapObservacionDAL.ObtenerTodos(ds);
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
        internal IList<ObservacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxObservacionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Observacion_ObtenerTodos", parameters);
                IList<ObservacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapObservacionDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Observacion
        /// </summary>
        /// <param name="observacionID">Identificador de la Observacion</param>
        /// <returns></returns>
        internal ObservacionInfo ObtenerPorID(int observacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxObservacionDAL.ObtenerParametrosPorID(observacionID);
                DataSet ds = Retrieve("Observacion_ObtenerPorID", parameters);
                ObservacionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapObservacionDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Observacion
        /// </summary>
        /// <param name="descripcion">Descripción de la Observacion</param>
        /// <returns></returns>
        internal ObservacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxObservacionDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Observacion_ObtenerPorDescripcion", parameters);
                ObservacionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapObservacionDAL.ObtenerPorDescripcion(ds);
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

