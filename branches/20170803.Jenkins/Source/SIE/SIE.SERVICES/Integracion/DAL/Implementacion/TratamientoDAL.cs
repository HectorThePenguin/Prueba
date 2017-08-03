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
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class TratamientoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Tratamiento
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerParametrosCrear(info);
                int result = Create("Tratamiento_Crear", parameters);
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
        /// Metodo para Crear un registro de Tratamiento
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Centros_Crear(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.Centros_ObtenerParametrosCrear(info);
                int result = Create("TratamientoCentros_Crear", parameters);
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
        /// Metodo para Crear un registro de Tratamiento
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        /// <param name="tratamientoID">Valores de la entidad que será creada</param>
        internal int GuardarTratamientoProducto(TratamientoInfo info, int tratamientoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.GuardarTratamientoProducto(info, tratamientoID);
                int result = Create("Tratamiento_GuardarTratamientoProducto", parameters);
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
        /// Metodo para Crear un registro de Tratamiento
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        /// <param name="tratamientoID">Valores de la entidad que será creada</param>
        internal int Centros_GuardarTratamientoProducto(TratamientoInfo info, int tratamientoID, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.Centros_GuardarTratamientoProducto(info, tratamientoID, organizacionID);
                int result = Create("TratamientoCentros_GuardarTratamientoProducto", parameters);
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
        /// Metodo para actualizar un registro de Tratamiento
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerParametrosActualizar(info);
                Update("Tratamiento_Actualizar", parameters);
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
        /// Metodo para actualizar un registro de Tratamiento
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Centros_Actualizar(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.Centros_ObtenerParametrosActualizar(info);
                Update("TratamientoCentros_Actualizar", parameters);
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
        internal ResultadoInfo<TratamientoInfo> ObtenerPorPagina(PaginacionInfo pagina, TratamientoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Tratamiento_ObtenerPorPagina", parameters);
                ResultadoInfo<TratamientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Tratamiento
        /// </summary>
        /// <returns></returns>
        internal IList<TratamientoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Tratamiento_ObtenerTodos");
                IList<TratamientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerTodos(ds);
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
        internal IList<TratamientoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Tratamiento_ObtenerTodos", parameters);
                IList<TratamientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Tratamiento
        /// </summary>
        /// <param name="tratamientoID">Identificador de la Tratamiento</param>
        /// <returns></returns>
        internal TratamientoInfo ObtenerPorID(int tratamientoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerParametrosPorID(tratamientoID);
                DataSet ds = Retrieve("Tratamiento_ObtenerPorID", parameters);
                TratamientoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Tratamiento
        /// </summary>
        /// <param name="descripcion">Descripción de la Tratamiento</param>
        /// <returns></returns>
        internal TratamientoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Tratamiento_ObtenerPorDescripcion", parameters);
                TratamientoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerPorDescripcion(ds);
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

        internal IList<TratamientoInfo> ObtenerTratamientosCorte(TratamientoInfo tratamientoInfo, Metafilaxia bMetafilaxia)
        {
            try
            {
                IList<TratamientoInfo> result = null;
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerTipoTratamientos(tratamientoInfo, bMetafilaxia);
                DataSet ds = Retrieve("TratamientoGanado_Obtener", parameters);
                
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerTipoTratamientos(ds);
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
        /// Obtiene la lista de tratamientos por tipo reimplante
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <returns></returns>
        internal IList<TratamientoInfo> ObtenerTratamientosPorTipoReimplante(TratamientoInfo tratamientoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerTratamientosPorTipo(tratamientoInfo);
                DataSet ds = Retrieve("TratamientoGanado_ObtenerPorTipoReimplante", parameters);
                IList<TratamientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerTipoTratamientos(ds);
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
        /// Obtiene la lista de tratamientos por tipo reimplante
        /// </summary>
        /// <param name="filtroTratamiento"></param>
        /// <param name="pagina"></param>
        /// <returns></returns>
        internal ResultadoInfo<TratamientoInfo> ObtenerTratamientosPorFiltro(PaginacionInfo pagina, FiltroTratamientoInfo filtroTratamiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerTratamientosPorFiltro(pagina, filtroTratamiento);
                DataSet ds = Retrieve("Tratamiento_ObtenerTratamientoPorPagina", parameters);
                ResultadoInfo<TratamientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerPorFiltro(ds);
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
        /// Obtiene la lista de tratamientos por tipo reimplante
        /// </summary>
        /// <param name="filtroTratamiento"></param>
        /// <param name="pagina"></param>
        /// <returns></returns>
        internal ResultadoInfo<TratamientoInfo> Centros_ObtenerTratamientosPorFiltro(PaginacionInfo pagina, FiltroTratamientoInfo filtroTratamiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.Centros_ObtenerTratamientosPorFiltro(pagina, filtroTratamiento);
                DataSet ds = Retrieve("TratamientoCentros_ObtenerTratamientoPorPagina", parameters);
                ResultadoInfo<TratamientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.Centros_ObtenerPorFiltro(ds);
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
        /// Valida si el código del tratamiento ya existe para esa organización
        /// </summary>
        /// <param name="info"></param>
        internal bool ValidarExisteTratamiento(TratamientoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerParametrosValidaExisteTratamiento(info);
                DataSet ds = Retrieve("Tratamiento_ObtenerPorOrganizacionCodigo", parameters);
                bool result = ValidateDataSet(ds);
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
        /// Obtiene la lista de tratamientos por problema
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <param name="listaProblemas"></param>
        /// <returns></returns>
        internal IList<TratamientoInfo> ObtenerTratamientosPorProblemas(TratamientoInfo tratamientoInfo, List<int> listaProblemas)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxTratamientoDAL.ObtenerParametrosObtenerTratamientosPorProblemas(tratamientoInfo, listaProblemas);
                DataSet ds = Retrieve("TratamientoGanado_ObtenerPorProblemas", parameters);
                IList<TratamientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerTipoTratamientosConDias(ds);
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
        /// OObtiene el costo de un tratamiento en un movimiento
        /// </summary>
        /// <param name="movimiento">se debe de proporcionar Organizacion y animalmovimiento</param>
        /// <param name="tratamientoID">Identificador del producto</param>
        /// <returns></returns>
        internal decimal ObtenerCostoPorMovimiento(AnimalMovimientoInfo movimiento, int tratamientoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerParametrosCostoPorMovimiento(movimiento, tratamientoID);
                DataSet ds = Retrieve("TratamientoGanado_ObtenerCostoTratamientoPorMovimiento", parameters);
                decimal result = 0;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerCosto(ds);
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
        /// OObtiene el costo de un producto de un tratamiento en un movimiento
        /// </summary>
        /// <param name="movimiento">se debe de proporcionar Organizacion y animalmovimiento</param>
        /// <param name="tratamiento">Identificador del producto</param>
        /// <returns></returns>
        internal decimal ObtenerCostoPorMovimientoProducto(AnimalMovimientoInfo movimiento, TratamientoProductoInfo tratamiento)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTratamientoDAL.ObtenerParametrosCostoPorMovimientoProducto(movimiento, tratamiento);
                var result = RetrieveValue<decimal>("TratamientoGanado_ObtenerCostoTratamientoPorMovimientoProducto", parameters);
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
        /// Obtiene la lista de tratamientos por TipoTrtamiento
        /// </summary>
        /// <param name="tratamientoInfo"></param>
        /// <returns></returns>
        public IList<TratamientoInfo> ObtenerTratamientosPorTipo(TratamientoInfo tratamientoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxTratamientoDAL.ObtenerParametrosObtenerTratamientosPorTipo(tratamientoInfo);
                DataSet ds = Retrieve("TratamientoGanado_ObtenerPorTipo", parameters);
                IList<TratamientoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTratamientoDAL.ObtenerTipoTratamientos(ds);
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
