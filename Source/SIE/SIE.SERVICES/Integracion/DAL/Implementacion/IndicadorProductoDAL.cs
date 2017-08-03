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
    internal class IndicadorProductoDAL : DALBase 
    {
        /// <summary>
        /// Metodo que obtiene un listado de indicadoresproducto por producto id
        /// </summary>
        /// <returns></returns>
        internal List<IndicadorProductoInfo> ObtenerPorProductoId(ProductoInfo productoInfo, EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                var parameters = AuxIndicadorProductoDAL.ObtenerParametrosObtenerPorProductoId(productoInfo, estatus);
                var ds = Retrieve("IndicadorProducto_ObtenerPorProductoId", parameters);
                List<IndicadorProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoDAL.ObtenerPorProductoId(ds);
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
        /// Metodo para Crear un registro de IndicadorProducto
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(IndicadorProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorProductoDAL.ObtenerParametrosCrear(info);
                int result = Create("IndicadorProducto_Crear", parameters);
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
        /// Metodo para actualizar un registro de IndicadorProducto
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(IndicadorProductoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorProductoDAL.ObtenerParametrosActualizar(info);
                Update("IndicadorProducto_Actualizar", parameters);
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
        public ResultadoInfo<IndicadorProductoInfo> ObtenerPorPagina(PaginacionInfo pagina, IndicadorProductoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxIndicadorProductoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("IndicadorProducto_ObtenerPorPagina", parameters);
                ResultadoInfo<IndicadorProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de IndicadorProducto
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("IndicadorProducto_ObtenerTodos");
                IList<IndicadorProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoDAL.ObtenerTodos(ds);
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
        public IList<IndicadorProductoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorProductoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("IndicadorProducto_ObtenerTodos", parameters);
                IList<IndicadorProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoDAL.ObtenerTodos(ds);
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
        /// Obtiene un indicador producto por clave
        /// de indicador y clave de producto
        /// </summary>
        /// <param name="indicadorProductoInfo"></param>
        /// <returns></returns>
        internal IndicadorProductoInfo ObtenerPorIndicadorProducto(IndicadorProductoInfo indicadorProductoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorProductoDAL.ObtenerPorIndicadorProducto(indicadorProductoInfo);
                DataSet ds = Retrieve("IndicadorProducto_ObtenerPorIndicadorProducto", parameters);
                IndicadorProductoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoDAL.ObtenerPorIndicadorProducto(ds);
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
