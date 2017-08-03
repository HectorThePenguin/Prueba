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
    internal class TipoCambioDAL : DALBase
    {

        /// <summary>
        /// Metodo para Crear un registro de TipoCambio
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TipoCambioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCambioDAL.ObtenerParametrosCrear(info);
                int result = Create("TipoCambio_Crear", parameters);
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
        /// Metodo para actualizar un registro de TipoCambio
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TipoCambioInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCambioDAL.ObtenerParametrosActualizar(info);
                Update("TipoCambio_Actualizar", parameters);
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
        internal ResultadoInfo<TipoCambioInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoCambioInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoCambioDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TipoCambio_ObtenerPorPagina", parameters);
                ResultadoInfo<TipoCambioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCambioDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de TipoCambio
        /// </summary>
        /// <returns></returns>
        internal IList<TipoCambioInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoCambio_ObtenerTodos");
                IList<TipoCambioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCambioDAL.ObtenerTodos(ds);
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
        internal IList<TipoCambioInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCambioDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoCambio_ObtenerTodos", parameters);
                IList<TipoCambioInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCambioDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de TipoCambio
        /// </summary>
        /// <param name="tipoCambioID">Identificador de la TipoCambio</param>
        /// <returns></returns>
        internal TipoCambioInfo ObtenerPorID(int tipoCambioID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCambioDAL.ObtenerParametrosPorID(tipoCambioID);
                DataSet ds = Retrieve("TipoCambio_ObtenerPorID", parameters);
                TipoCambioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCambioDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de TipoCambio
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoCambio</param>
        /// <returns></returns>
        internal TipoCambioInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCambioDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoCambio_ObtenerPorDescripcion", parameters);
                TipoCambioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCambioDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una Lista con todos los tipos de cambio
        /// </summary>
        /// <returns></returns>
        internal List<TipoCambioInfo> ObtenerPorEstado(EstatusEnum estatus)
        {
            List<TipoCambioInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxTipoCambioDAL.ObtenerParametrosObtenerTipoCambioPorEstado(estatus);
                var ds = Retrieve("TipoCambio_ObtenerPorEstado", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCambioDAL.ObtenerTodos(ds);
                }
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
            return result;
        }

        /// <summary>
        /// Obtiene un tipo de cambio por id
        /// </summary>
        /// <param name="tipoCambioId"></param>
        /// <returns></returns>
        internal TipoCambioInfo ObtenerPorId(int tipoCambioId)
        {
            try
            {
                Logger.Info();
                var parameters = AuxTipoCambioDAL.ObtenerParametrosObtenerTipoCambioPorId(tipoCambioId);
                using (IDataReader reader = RetrieveReader("TipoCambio_ObtenerPorID", parameters))
                {
                    TipoCambioInfo result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapTipoCambioDAL.ObtenerTipoCambioPorId(reader);
                    }
                    return result;
                }
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
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Obtiene un listado de tipo cambio por fecha actual
        /// </summary>
        /// <returns></returns>
        internal List<TipoCambioInfo> ObtenerPorFechaActual()
        {
            List<TipoCambioInfo> result = null;
            try
            {
                Logger.Info();
                var parameters = AuxTipoCambioDAL.ObtenerParametrosObtenerTipoCambioPorEstado(EstatusEnum.Activo);
                var ds = Retrieve("TipoCambio_ObtenerPorFechaActual", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCambioDAL.ObtenerTodos(ds);
                }
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
            return result;
        }

        /// <summary>
        /// Obtiene un registro de TipoCambio
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoCambio</param>
        /// /// <param name="fecha">Fecha del Tipo de Cambio</param>
        /// <returns></returns>
        internal TipoCambioInfo ObtenerPorDescripcionFecha(string descripcion, DateTime fecha)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoCambioDAL.ObtenerParametrosPorDescripcionFecha(descripcion, fecha);
                DataSet ds = Retrieve("TipoCambio_ObtenerPorDescripcionFecha", parameters);
                TipoCambioInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoCambioDAL.ObtenerPorDescripcionFecha(ds);
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
