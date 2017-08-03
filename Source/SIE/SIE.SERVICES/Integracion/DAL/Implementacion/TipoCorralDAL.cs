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
    internal class TipoCorralDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de TipoCorral
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TipoCorralInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCorralDAL.ObtenerParametrosCrear(info);
                int result = Create("TipoCorral_Crear", parameters);
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
        /// Metodo para actualizar un registro de TipoCorral
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TipoCorralInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCorralDAL.ObtenerParametrosActualizar(info);
                Update("TipoCorral_Actualizar", parameters);
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
        internal ResultadoInfo<TipoCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCorralInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoCorralDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TipoCorral_ObtenerPorPagina", parameters);
                ResultadoInfo<TipoCorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCorralDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de TipoCorral
        /// </summary>
        /// <returns></returns>
        internal IList<TipoCorralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoCorral_ObtenerTodos");
                IList<TipoCorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCorralDAL.ObtenerTodos(ds);
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
        internal IList<TipoCorralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCorralDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoCorral_ObtenerTodos", parameters);
                IList<TipoCorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCorralDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de TipoCorral
        /// </summary>
        /// <param name="tipoCorralID">Identificador de la TipoCorral</param>
        /// <returns></returns>
        internal TipoCorralInfo ObtenerPorID(int tipoCorralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCorralDAL.ObtenerParametrosPorID(tipoCorralID);
                DataSet ds = Retrieve("TipoCorral_ObtenerPorID", parameters);
                TipoCorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCorralDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de TipoCorral
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoCorral</param>
        /// <returns></returns>
        internal TipoCorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCorralDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoCorral_ObtenerPorDescripcion", parameters);
                TipoCorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCorralDAL.ObtenerPorDescripcion(ds);
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

