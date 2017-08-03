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
    public class IndicadorProductoCalidadDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de IndicadorProductoCalidad
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(IndicadorProductoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorProductoCalidadDAL.ObtenerParametrosCrear(info);
                int result = Create("IndicadorProductoCalidad_Crear", parameters);
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
        /// Metodo para actualizar un registro de IndicadorProductoCalidad
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(IndicadorProductoCalidadInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorProductoCalidadDAL.ObtenerParametrosActualizar(info);
                Update("IndicadorProductoCalidad_Actualizar", parameters);
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
        public ResultadoInfo<IndicadorProductoCalidadInfo> ObtenerPorPagina(PaginacionInfo pagina, IndicadorProductoCalidadInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxIndicadorProductoCalidadDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("IndicadorProductoCalidad_ObtenerPorPagina", parameters);
                ResultadoInfo<IndicadorProductoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoCalidadDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de IndicadorProductoCalidad
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorProductoCalidadInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("IndicadorProductoCalidad_ObtenerTodos");
                IList<IndicadorProductoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoCalidadDAL.ObtenerTodos(ds);
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
        public IList<IndicadorProductoCalidadInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorProductoCalidadDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("IndicadorProductoCalidad_ObtenerTodos", parameters);
                IList<IndicadorProductoCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoCalidadDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de IndicadorProductoCalidad
        /// </summary>
        /// <param name="indicadorProductoCalidadID">Identificador de la IndicadorProductoCalidad</param>
        /// <returns></returns>
        public IndicadorProductoCalidadInfo ObtenerPorID(int indicadorProductoCalidadID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorProductoCalidadDAL.ObtenerParametrosPorID(indicadorProductoCalidadID);
                DataSet ds = Retrieve("IndicadorProductoCalidad_ObtenerPorID", parameters);
                IndicadorProductoCalidadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoCalidadDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de IndicadorProductoCalidad
        /// </summary>
        /// <param name="descripcion">Descripción de la IndicadorProductoCalidad</param>
        /// <returns></returns>
        public IndicadorProductoCalidadInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorProductoCalidadDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("IndicadorProductoCalidad_ObtenerPorDescripcion", parameters);
                IndicadorProductoCalidadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoCalidadDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un indicador producto calidad
        /// </summary>
        /// <param name="indicadorProductoCalidad"></param>
        /// <returns></returns>
        internal IndicadorProductoCalidadInfo ObtenerPorIndicadorProducto(IndicadorProductoCalidadInfo indicadorProductoCalidad)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxIndicadorProductoCalidadDAL.ObtenerPorIndicadorProducto(indicadorProductoCalidad);
                DataSet ds = Retrieve("IndicadorProductoCalidad_ObtenerPorIndicadorProducto", parameters);
                IndicadorProductoCalidadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoCalidadDAL.ObtenerPorIndicadorProducto(ds);
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
        /// Obtiene un indicador producto calidad
        /// </summary>
        /// <param name="indicadorID"></param>
        /// <returns></returns>
        internal List<ProductoInfo> ObtenerProductosPorIndicador(int indicadorID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxIndicadorProductoCalidadDAL.ObtenerProductoPorInidicador(indicadorID);
                DataSet ds = Retrieve("IndicadorProductoCalidad_ObtenerProductosPorIndicador", parameters);
                List<ProductoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorProductoCalidadDAL.ObtenerProductosPorIndicador(ds);
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

