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
    internal class TipoParametroDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de TipoParametro
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TipoParametroInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoParametroDAL.ObtenerParametrosCrear(info);
                int result = Create("TipoParametro_Crear", parameters);
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
        /// Metodo para actualizar un registro de TipoParametro
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TipoParametroInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoParametroDAL.ObtenerParametrosActualizar(info);
                Update("TipoParametro_Actualizar", parameters);
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
        /// Obtiene un lista paginada de tipos de parametros
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoParametroInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoParametroInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoParametroDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TipoParametro_ObtenerPorPagina", parameters);
                ResultadoInfo<TipoParametroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoParametroDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de TipoParametro
        /// </summary>
        /// <returns></returns>
        internal IList<TipoParametroInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoParametro_ObtenerTodos");
                IList<TipoParametroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoParametroDAL.ObtenerTodos(ds);
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
        internal IList<TipoParametroInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoParametroDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoParametro_ObtenerTodos", parameters);
                IList<TipoParametroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoParametroDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de TipoParametro
        /// </summary>
        /// <param name="tipoParametroID">Identificador de la TipoParametro</param>
        /// <returns></returns>
        internal TipoParametroInfo ObtenerPorID(int tipoParametroID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoParametroDAL.ObtenerParametrosPorID(tipoParametroID);
                DataSet ds = Retrieve("TipoParametro_ObtenerPorID", parameters);
                TipoParametroInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoParametroDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de TipoParametro
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoParametro</param>
        /// <returns></returns>
        internal TipoParametroInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoParametroDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoParametro_ObtenerPorDescripcion", parameters);
                TipoParametroInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoParametroDAL.ObtenerPorDescripcion(ds);
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

