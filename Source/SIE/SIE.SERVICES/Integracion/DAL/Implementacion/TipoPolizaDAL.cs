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
    internal class TipoPolizaDAL : DALBase
    {
        /// <summary>
        ///     Metodo para Crear un nuevo registro de TipoPoliza
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TipoPolizaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoPolizaDAL.ObtenerParametrosCrear(info);
                int result = Create("TipoPoliza_Crear", parameters);
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
        ///     Metodo para Actualizar un nuevo registro de TipoPoliza
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TipoPolizaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoPolizaDAL.ObtenerParametrosActualizar(info);
                Update("TipoPoliza_Actualizar", parameters);
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
        ///     Obtiene un lista paginada
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal ResultadoInfo<TipoPolizaInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoPolizaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTipoPolizaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[TipoPoliza_ObtenerPorPagina]", parameters);
                ResultadoInfo<TipoPolizaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoPolizaDAL.ObtenerPorPagina(ds);
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
        ///     Obtiene un registro de Entrada Ganado Costeo por su EntradaId
        /// </summary>
        /// <returns></returns>
        internal IList<TipoPolizaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoPoliza_ObtenerTodos");
                IList<TipoPolizaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoPolizaDAL.ObtenerTodos(ds);
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
        ///     Obtiene un registro de Entrada Ganado Costeo por su EntradaId
        /// </summary>
        /// <returns></returns>
        internal IList<TipoPolizaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoPolizaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoPoliza_ObtenerTodos", parameters);
                IList<TipoPolizaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoPolizaDAL.ObtenerTodos(ds);
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
        ///     Obtiene un registro de TipoPoliza
        /// </summary>
        /// <param name="tipoPolizaID">Identificador de la TipoPoliza</param>
        /// <returns></returns>
        internal TipoPolizaInfo ObtenerPorID(int tipoPolizaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoPolizaDAL.ObtenerParametrosPorID(tipoPolizaID);
                DataSet ds = Retrieve("TipoPoliza_ObtenerPorID", parameters);
                TipoPolizaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoPolizaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de TipoPoliza
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoPoliza</param>
        /// <returns></returns>
        internal TipoPolizaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTipoPolizaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoPoliza_ObtenerPorDescripcion", parameters);
                TipoPolizaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoPolizaDAL.ObtenerPorDescripcion(ds);
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

