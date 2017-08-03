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
    internal class CondicionDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Condicion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(CondicionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCondicionDAL.ObtenerParametrosCrear(info);
                int result = Create("Condicion_Crear", parameters);
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
        /// Metodo para actualizar un registro de Condicion
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CondicionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCondicionDAL.ObtenerParametrosActualizar(info);
                Update("Condicion_Actualizar", parameters);
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
        internal ResultadoInfo<CondicionInfo> ObtenerPorPagina(PaginacionInfo pagina, CondicionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCondicionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Condicion_ObtenerPorPagina", parameters);
                ResultadoInfo<CondicionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCondicionDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Condicion
        /// </summary>
        /// <returns></returns>
        internal IList<CondicionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Condicion_ObtenerTodos");
                IList<CondicionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCondicionDAL.ObtenerTodos(ds);
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
        internal IList<CondicionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCondicionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Condicion_ObtenerTodos", parameters);
                IList<CondicionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCondicionDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Condicion
        /// </summary>
        /// <param name="condicionID">Identificador de la Condicion</param>
        /// <returns></returns>
        internal CondicionInfo ObtenerPorID(int condicionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCondicionDAL.ObtenerParametrosPorID(condicionID);
                DataSet ds = Retrieve("Condicion_ObtenerPorID", parameters);
                CondicionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCondicionDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Condicion
        /// </summary>
        /// <param name="descripcion">Descripción de la Condicion</param>
        /// <returns></returns>
        internal CondicionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCondicionDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Condicion_ObtenerPorDescripcion", parameters);
                CondicionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCondicionDAL.ObtenerPorDescripcion(ds);
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

