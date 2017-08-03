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
    public class MonedaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Moneda
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(MonedaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMonedaDAL.ObtenerParametrosCrear(info);
                int result = Create("Moneda_Crear", parameters);
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
        /// Metodo para actualizar un registro de Moneda
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(MonedaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMonedaDAL.ObtenerParametrosActualizar(info);
                Update("Moneda_Actualizar", parameters);
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
        public ResultadoInfo<MonedaInfo> ObtenerPorPagina(PaginacionInfo pagina, MonedaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxMonedaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Moneda_ObtenerPorPagina", parameters);
                ResultadoInfo<MonedaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMonedaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Moneda
        /// </summary>
        /// <returns></returns>
        public IList<MonedaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Moneda_ObtenerTodos");
                IList<MonedaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMonedaDAL.ObtenerTodos(ds);
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
        public IList<MonedaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMonedaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Moneda_ObtenerTodos", parameters);
                IList<MonedaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMonedaDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Moneda
        /// </summary>
        /// <param name="monedaID">Identificador de la Moneda</param>
        /// <returns></returns>
        public MonedaInfo ObtenerPorID(int monedaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMonedaDAL.ObtenerParametrosPorID(monedaID);
                DataSet ds = Retrieve("Moneda_ObtenerPorID", parameters);
                MonedaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMonedaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Moneda
        /// </summary>
        /// <param name="descripcion">Descripción de la Moneda</param>
        /// <returns></returns>
        public MonedaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMonedaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Moneda_ObtenerPorDescripcion", parameters);
                MonedaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMonedaDAL.ObtenerPorDescripcion(ds);
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

