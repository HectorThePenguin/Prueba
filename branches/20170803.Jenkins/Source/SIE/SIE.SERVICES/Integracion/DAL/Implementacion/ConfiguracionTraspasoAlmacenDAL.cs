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
    public class ConfiguracionTraspasoAlmacenDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de ConfiguracionTraspasoAlmacen
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(ConfiguracionTraspasoAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionTraspasoAlmacenDAL.ObtenerParametrosCrear(info);
                int result = Create("ConfiguracionTraspasoAlmacen_Crear", parameters);
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
        /// Metodo para actualizar un registro de ConfiguracionTraspasoAlmacen
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(ConfiguracionTraspasoAlmacenInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionTraspasoAlmacenDAL.ObtenerParametrosActualizar(info);
                Update("ConfiguracionTraspasoAlmacen_Actualizar", parameters);
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
        public ResultadoInfo<ConfiguracionTraspasoAlmacenInfo> ObtenerPorPagina(PaginacionInfo pagina, ConfiguracionTraspasoAlmacenInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxConfiguracionTraspasoAlmacenDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ConfiguracionTraspasoAlmacen_ObtenerPorPagina", parameters);
                ResultadoInfo<ConfiguracionTraspasoAlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionTraspasoAlmacenDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de ConfiguracionTraspasoAlmacen
        /// </summary>
        /// <returns></returns>
        public IList<ConfiguracionTraspasoAlmacenInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ConfiguracionTraspasoAlmacen_ObtenerTodos");
                IList<ConfiguracionTraspasoAlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionTraspasoAlmacenDAL.ObtenerTodos(ds);
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
        public IList<ConfiguracionTraspasoAlmacenInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionTraspasoAlmacenDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("ConfiguracionTraspasoAlmacen_ObtenerTodos", parameters);
                IList<ConfiguracionTraspasoAlmacenInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionTraspasoAlmacenDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de ConfiguracionTraspasoAlmacen
        /// </summary>
        /// <param name="configuracionTraspasoAlmacenID">Identificador de la ConfiguracionTraspasoAlmacen</param>
        /// <returns></returns>
        public ConfiguracionTraspasoAlmacenInfo ObtenerPorID(int configuracionTraspasoAlmacenID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionTraspasoAlmacenDAL.ObtenerParametrosPorID(configuracionTraspasoAlmacenID);
                DataSet ds = Retrieve("ConfiguracionTraspasoAlmacen_ObtenerPorID", parameters);
                ConfiguracionTraspasoAlmacenInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionTraspasoAlmacenDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de ConfiguracionTraspasoAlmacen
        /// </summary>
        /// <param name="descripcion">Descripción de la ConfiguracionTraspasoAlmacen</param>
        /// <returns></returns>
        public ConfiguracionTraspasoAlmacenInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionTraspasoAlmacenDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("ConfiguracionTraspasoAlmacen_ObtenerPorDescripcion", parameters);
                ConfiguracionTraspasoAlmacenInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionTraspasoAlmacenDAL.ObtenerPorDescripcion(ds);
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

