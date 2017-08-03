using System.Collections.Generic;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class IndicadorObjetivoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de IndicadorObjetivo
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(IndicadorObjetivoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorObjetivoDAL.ObtenerParametrosCrear(info);
                int result = Create("IndicadorObjetivo_Crear", parameters);
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
        /// Metodo para actualizar un registro de IndicadorObjetivo
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(IndicadorObjetivoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorObjetivoDAL.ObtenerParametrosActualizar(info);
                Update("IndicadorObjetivo_Actualizar", parameters);
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
        public ResultadoInfo<IndicadorObjetivoInfo> ObtenerPorPagina(PaginacionInfo pagina, IndicadorObjetivoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxIndicadorObjetivoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("IndicadorObjetivo_ObtenerPorPagina", parameters);
                ResultadoInfo<IndicadorObjetivoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorObjetivoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de IndicadorObjetivo
        /// </summary>
        /// <returns></returns>
        public IList<IndicadorObjetivoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("IndicadorObjetivo_ObtenerTodos");
                IList<IndicadorObjetivoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorObjetivoDAL.ObtenerTodos(ds);
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
        public IList<IndicadorObjetivoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorObjetivoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("IndicadorObjetivo_ObtenerTodos", parameters);
                IList<IndicadorObjetivoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorObjetivoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de IndicadorObjetivo
        /// </summary>
        /// <param name="indicadorObjetivoID">Identificador de la IndicadorObjetivo</param>
        /// <returns></returns>
        public IndicadorObjetivoInfo ObtenerPorID(int indicadorObjetivoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorObjetivoDAL.ObtenerParametrosPorID(indicadorObjetivoID);
                DataSet ds = Retrieve("IndicadorObjetivo_ObtenerPorID", parameters);
                IndicadorObjetivoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorObjetivoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de IndicadorObjetivo
        /// </summary>
        /// <param name="descripcion">Descripción de la IndicadorObjetivo</param>
        /// <returns></returns>
        public IndicadorObjetivoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorObjetivoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("IndicadorObjetivo_ObtenerPorDescripcion", parameters);
                IndicadorObjetivoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorObjetivoDAL.ObtenerPorDescripcion(ds);
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
        /// Metodo para Obtener el semaforo
        /// </summary>
        internal List<IndicadorObjetivoSemaforoInfo> ObtenerSemaforo(int pedidoID, int productoID, int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorObjetivoDAL.ObtenerSemaforo(pedidoID, productoID, organizacionID);
                var ds = Retrieve("IndicadorObjetivo_ObtenerSemaforo", parameters);
                List<IndicadorObjetivoSemaforoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorObjetivoDAL.ObtenerSemaforo(ds);
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
        /// Obtiene un registro de IndicadorObjetivo
        /// </summary>
        /// <param name="filtros">Descripción de la IndicadorObjetivo</param>
        /// <returns></returns>
        public IndicadorObjetivoInfo ObtenerPorFiltros(IndicadorObjetivoInfo filtros)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxIndicadorObjetivoDAL.ObtenerParametrosPorFiltros(filtros);
                DataSet ds = Retrieve("IndicadorObjetivo_ObtenerPorFiltros", parameters);
                IndicadorObjetivoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapIndicadorObjetivoDAL.ObtenerPorFiltros(ds);
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

