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
    internal class TipoMovimientoDAL : DALBase
    {
        /// <summary>
        ///     Metodo para Crear un nuevo registro de TipoMovimiento
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TipoMovimientoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxTipoMovimientoDAL.ObtenerParametrosCrear(info);
                int resultado = Create("TipoMovimiento_Crear", parametros);
                return resultado;
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
        ///     Metodo para Actualizar un nuevo registro de TipoMovimiento
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TipoMovimientoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxTipoMovimientoDAL.ObtenerParametrosActualizar(info);
                Update("TipoMovimiento_Actualizar", parametros);
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
        internal ResultadoInfo<TipoMovimientoInfo> ObtenerPorPagina(PaginacionInfo pagina, TipoMovimientoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parametros = AuxTipoMovimientoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("[dbo].[TipoMovimiento_ObtenerPorPagina]", parametros);
                ResultadoInfo<TipoMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoMovimientoDAL.ObtenerPorPagina(ds);
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
        internal IList<TipoMovimientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoMovimiento_ObtenerTodos");
                IList<TipoMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoMovimientoDAL.ObtenerTodos(ds);
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
        internal IList<TipoMovimientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxTipoMovimientoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TipoMovimiento_ObtenerTodos", parametros);
                IList<TipoMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoMovimientoDAL.ObtenerTodos(ds);
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
        ///     Obtiene un registro de TipoMovimiento
        /// </summary>
        /// <param name="tipoMovimientoID">Identificador de la TipoMovimiento</param>
        /// <returns></returns>
        internal TipoMovimientoInfo ObtenerPorID(int tipoMovimientoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxTipoMovimientoDAL.ObtenerParametrosPorID(tipoMovimientoID);
                DataSet ds = Retrieve("TipoMovimiento_ObtenerPorID", parametros);
                TipoMovimientoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoMovimientoDAL.ObtenerPorID(ds);
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
        ///     Obtiene un registro de TipoMovimiento
        /// </summary>
        /// <param name="tipoMovimientoID">Identificador de la TipoMovimiento</param>
        /// <returns></returns>
        internal TipoMovimientoInfo ObtenerSoloTipoMovimiento(int tipoMovimientoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxTipoMovimientoDAL.ObtenerParametrosPorID(tipoMovimientoID);
                DataSet ds = Retrieve("SalidaIndividualRecuperacion_ObtenerTipoMovimiento", parametros);
                TipoMovimientoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoMovimientoDAL.ObtenerSoloTipoMovimiento(ds);
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
        /// Obtiene un registro de TipoMovimiento
        /// </summary>
        /// <param name="descripcion">Descripción de la TipoMovimiento</param>
        /// <returns></returns>
        internal TipoMovimientoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxTipoMovimientoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TipoMovimiento_ObtenerPorDescripcion", parametros);
                TipoMovimientoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoMovimientoDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene los elementos que se mostraran
        /// para la pantalla de calidad pase proceso
        /// </summary>
        /// <returns></returns>
        internal IList<TipoMovimientoInfo> ObtenerTipoMovimientoCalidadPasaeProceso()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TipoMovimiento_ObtenerTipoMovimientoCalidadPasaeProceso");
                IList<TipoMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoMovimientoDAL.ObtenerTipoMovimientoCalidadPasaeProceso(ds);
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
        /// Obtiene los tipos de movimientos de productos
        /// </summary>
        /// <returns></returns>
        internal IList<TipoMovimientoInfo> ObtenerMovimientosProducto()
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros = AuxTipoMovimientoDAL.ObtenerParametrosTipoMovimientoProductos();
                DataSet ds = Retrieve("TipoMovimiento_ObtenerMovimientosProductos", parametros);
                IList<TipoMovimientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTipoMovimientoDAL.ObtenerTodos(ds);
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

