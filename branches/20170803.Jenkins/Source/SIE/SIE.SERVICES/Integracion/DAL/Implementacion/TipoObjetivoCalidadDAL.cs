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
    public class TipoObjetivoCalidadDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de TipoObjetivoCalidad
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(TipoObjetivoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoObjetivoCalidadDAL.ObtenerParametrosCrear(info);
                int result = Create("TipoObjetivoCalidad_Crear", parameters);
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
        /// Metodo para actualizar un registro de TipoObjetivoCalidad
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(TipoObjetivoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoObjetivoCalidadDAL.ObtenerParametrosActualizar(info);
                Update("TipoObjetivoCalidad_Actualizar", parameters);
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
        public ResultadoInfo<TipoObjetivoCalidadInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoObjetivoCalidadInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoObjetivoCalidadDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TipoObjetivoCalidad_ObtenerPorPagina", parameters);
                ResultadoInfo<TipoObjetivoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoObjetivoCalidadDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de TipoObjetivoCalidad
        /// </summary>
        /// <returns></returns>
        public IList<TipoObjetivoCalidadInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoObjetivoCalidad_ObtenerTodos");
                IList<TipoObjetivoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoObjetivoCalidadDAL.ObtenerTodos(ds);
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
        public IList<TipoObjetivoCalidadInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoObjetivoCalidadDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoObjetivoCalidad_ObtenerTodos", parameters);
                IList<TipoObjetivoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoObjetivoCalidadDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de TipoObjetivoCalidad
        /// </summary>
        /// <param name="tipoObjetivoCalidadID">Identificador de la TipoObjetivoCalidad</param>
        /// <returns></returns>
        public TipoObjetivoCalidadInfo ObtenerPorID(int tipoObjetivoCalidadID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoObjetivoCalidadDAL.ObtenerParametrosPorID(tipoObjetivoCalidadID);
                DataSet ds = Retrieve("TipoObjetivoCalidad_ObtenerPorID", parameters);
                TipoObjetivoCalidadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoObjetivoCalidadDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de TipoObjetivoCalidad
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoObjetivoCalidad</param>
        /// <returns></returns>
        public TipoObjetivoCalidadInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoObjetivoCalidadDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoObjetivoCalidad_ObtenerPorDescripcion", parameters);
                TipoObjetivoCalidadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoObjetivoCalidadDAL.ObtenerPorDescripcion(ds);
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

