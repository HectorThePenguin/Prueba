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
    internal class TratamientoProductoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de TratamientoProducto
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TratamientoProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoProductoDAL.ObtenerParametrosCrear(info);
                int result = Create("TratamientoProducto_Crear", parameters);
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
        /// Metodo para actualizar un registro de TratamientoProducto
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TratamientoProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoProductoDAL.ObtenerParametrosActualizar(info);
                Update("TratamientoProducto_Actualizar", parameters);
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
        internal ResultadoInfo<TratamientoProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, TratamientoProductoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTratamientoProductoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("TratamientoProducto_ObtenerPorPagina", parameters);
                ResultadoInfo<TratamientoProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoProductoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de TratamientoProducto
        /// </summary>
        /// <returns></returns>
        internal IList<TratamientoProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("TratamientoProducto_ObtenerTodos");
                IList<TratamientoProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoProductoDAL.ObtenerTodos(ds);
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
        internal IList<TratamientoProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoProductoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("TratamientoProducto_ObtenerTodos", parameters);
                IList<TratamientoProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoProductoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de TratamientoProducto
        /// </summary>
        /// <param name="tratamientoProductoID">Identificador de la TratamientoProducto</param>
        /// <returns></returns>
        internal TratamientoProductoInfo ObtenerPorID(int tratamientoProductoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoProductoDAL.ObtenerParametrosPorID(tratamientoProductoID);
                DataSet ds = Retrieve("TratamientoProducto_ObtenerPorID", parameters);
                TratamientoProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoProductoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de TratamientoProducto
        /// </summary>
        /// <param name="descripcion">Descripción de la TratamientoProducto</param>
        /// <returns></returns>
        internal TratamientoProductoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoProductoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("TratamientoProducto_ObtenerPorDescripcion", parameters);
                TratamientoProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoProductoDAL.ObtenerPorDescripcion(ds);
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
        internal ResultadoInfo<TratamientoProductoInfo> ObtenerPorPaginaTratamientoID(PaginacionInfo pagina, TratamientoProductoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTratamientoProductoDAL.ObtenerParametrosPorPaginaTratamientoID(pagina, filtro);
                DataSet ds = Retrieve("TratamientoProducto_ObtenerPorPaginaTratamiendoID", parameters);
                ResultadoInfo<TratamientoProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoProductoDAL.ObtenerPorPaginaTratamientoID(ds);
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

        internal List<TratamientoProductoInfo> ObtenerPorTratamientoID(TratamientoInfo tratamiento)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTratamientoProductoDAL.ObtenerParametrosObtenerPorTratamientoID(tratamiento);
                DataSet ds = Retrieve("TratamientoProducto_ObtenerPorTratamientoID", parameters);
                List<TratamientoProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoProductoDAL.ObtenerPorTratamientoID(ds);
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

        internal List<HistorialClinicoDetalleInfo> ObtenerTratamientoAplicadoPorMovimientoTratamientoID(AnimalMovimientoInfo animalMovimiento, TratamientoInfo tratamiento)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTratamientoProductoDAL.ObtenerParametrosObtenerPorMovimientoTratamientoID(animalMovimiento, tratamiento);
                DataSet ds = Retrieve("TratamientoGanado_ObtenerTratamientoAplicadoMovimiento", parameters);
                List<HistorialClinicoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoProductoDAL.ObtenerPorMovimientoTratamientoID(ds);
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

