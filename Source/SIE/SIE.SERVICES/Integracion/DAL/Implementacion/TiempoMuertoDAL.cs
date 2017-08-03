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
    internal class TiempoMuertoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de TiempoMuerto
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TiempoMuertoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTiempoMuertoDAL.ObtenerParametrosCrear(info);
                int result = Create("TiempoMuerto_Crear", parameters);
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
        /// Metodo para actualizar un registro de TiempoMuerto
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TiempoMuertoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTiempoMuertoDAL.ObtenerParametrosActualizar(info);
                Update("TiempoMuerto_Actualizar", parameters);
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
        internal ResultadoInfo<TiempoMuertoInfo> ObtenerPorPagina(PaginacionInfo pagina, TiempoMuertoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTiempoMuertoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TiempoMuerto_ObtenerPorPagina", parameters);
                ResultadoInfo<TiempoMuertoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTiempoMuertoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de TiempoMuerto
        /// </summary>
        /// <returns></returns>
        internal IList<TiempoMuertoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TiempoMuerto_ObtenerTodos");
                IList<TiempoMuertoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTiempoMuertoDAL.ObtenerTodos(ds);
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
        internal IList<TiempoMuertoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTiempoMuertoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TiempoMuerto_ObtenerTodos", parameters);
                IList<TiempoMuertoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTiempoMuertoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de TiempoMuerto
        /// </summary>
        /// <param name="tiempoMuertoID">Identificador de la TiempoMuerto</param>
        /// <returns></returns>
        internal TiempoMuertoInfo ObtenerPorID(int tiempoMuertoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTiempoMuertoDAL.ObtenerParametrosPorID(tiempoMuertoID);
                DataSet ds = Retrieve("TiempoMuerto_ObtenerPorID", parameters);
                TiempoMuertoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTiempoMuertoDAL.ObtenerPorID(ds);
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
        /// Metodo para Crear un registro de TiempoMuerto
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        /// <param name="produccionDiariaID">Valores de la entidad que será creada</param>
        internal int GuardarTiempoMuerto(ProduccionDiariaInfo info, int produccionDiariaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTiempoMuertoDAL.ObtenerGuardarTiempoMuerto(info, produccionDiariaID);
                int result = Create("ProduccionDiaria_GuardarTiempoMuerto", parameters);
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
        /// Metodo para Crear un registro de TiempoMuerto
        /// </summary>
        /// <param name="tiempoMuerto">Valores de la entidad que será creada</param>
        /// <param name="produccionDiariaID">Valores de la entidad que será creada</param>
        internal int GuardarTiempoMuertoReparto(List<TiempoMuertoInfo> tiempoMuerto, int produccionDiariaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTiempoMuertoDAL.ObtenerGuardarTiempoMuertoReparto(tiempoMuerto, produccionDiariaID);
                int result = Create("RepartoAlimento_GuardarTiempoMuerto", parameters);
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

