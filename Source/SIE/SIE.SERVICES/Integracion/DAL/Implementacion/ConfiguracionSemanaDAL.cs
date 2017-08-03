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
    internal class ConfiguracionSemanaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de ConfiguracionSemana
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(ConfiguracionSemanaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionSemanaDAL.ObtenerParametrosCrear(info);
                int result = Create("ConfiguracionSemana_Crear", parameters);
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
        /// Metodo para actualizar un registro de ConfiguracionSemana
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(ConfiguracionSemanaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionSemanaDAL.ObtenerParametrosActualizar(info);
                Update("ConfiguracionSemana_Actualizar", parameters);
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
        internal ResultadoInfo<ConfiguracionSemanaInfo> ObtenerPorPagina(PaginacionInfo pagina, ConfiguracionSemanaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxConfiguracionSemanaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ConfiguracionSemana_ObtenerPorPagina", parameters);
                ResultadoInfo<ConfiguracionSemanaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionSemanaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de ConfiguracionSemana
        /// </summary>
        /// <returns></returns>
        internal IList<ConfiguracionSemanaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ConfiguracionSemana_ObtenerTodos");
                IList<ConfiguracionSemanaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionSemanaDAL.ObtenerTodos(ds);
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
        internal IList<ConfiguracionSemanaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionSemanaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("ConfiguracionSemana_ObtenerTodos", parameters);
                IList<ConfiguracionSemanaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionSemanaDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de ConfiguracionSemana
        /// </summary>
        /// <param name="configuracionSemanaID">Identificador de la ConfiguracionSemana</param>
        /// <returns></returns>
        internal ConfiguracionSemanaInfo ObtenerPorID(int configuracionSemanaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionSemanaDAL.ObtenerParametrosPorID(configuracionSemanaID);
                DataSet ds = Retrieve("ConfiguracionSemana_ObtenerPorID", parameters);
                ConfiguracionSemanaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionSemanaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de ConfiguracionSemana
        /// </summary>
        /// <param name="descripcion">Descripción de la ConfiguracionSemana</param>
        /// <returns></returns>
        internal ConfiguracionSemanaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionSemanaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("ConfiguracionSemana_ObtenerPorDescripcion", parameters);
                ConfiguracionSemanaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionSemanaDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de ConfiguracionSemana
        /// </summary>
        /// <param name="organizacionID">Organización a la que pertenece la configuración</param>
        /// <returns></returns>
        internal ConfiguracionSemanaInfo ObtenerPorOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConfiguracionSemanaDAL.ObtenerParametrosPorOrganizacion(organizacionID);
                DataSet ds = Retrieve("ConfiguracionSemana_ObtenerPorOrganizacion", parameters);
                ConfiguracionSemanaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionSemanaDAL.ObtenerPorOrganizacion(ds);
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
        internal ResultadoInfo<ConfiguracionSemanaInfo> ObtenerPorFiltroPagina(PaginacionInfo pagina, ConfiguracionSemanaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxConfiguracionSemanaDAL.ObtenerParametrosPorFiltroPagina(pagina, filtro);
                DataSet ds = Retrieve("ConfiguracionSemana_ObtenerPorFiltroPagina", parameters);
                ResultadoInfo<ConfiguracionSemanaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConfiguracionSemanaDAL.ObtenerPorFiltroPagina(ds);
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

