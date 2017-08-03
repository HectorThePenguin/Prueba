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
    internal class TrampaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Trampa
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(TrampaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTrampaDAL.ObtenerParametrosCrear(info);
                int result = Create("Trampa_Crear", parameters);
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
        /// Metodo para actualizar un registro de Trampa
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(TrampaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTrampaDAL.ObtenerParametrosActualizar(info);
                Update("Trampa_Actualizar", parameters);
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
        internal ResultadoInfo<TrampaInfo> ObtenerPorPagina(PaginacionInfo pagina, TrampaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxTrampaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Trampa_ObtenerPorPagina", parameters);
                ResultadoInfo<TrampaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTrampaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Trampa
        /// </summary>
        /// <returns></returns>
        internal IList<TrampaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Trampa_ObtenerTodos");
                IList<TrampaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTrampaDAL.ObtenerTodos(ds);
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
        internal IList<TrampaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTrampaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Trampa_ObtenerTodos", parameters);
                IList<TrampaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTrampaDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Trampa
        /// </summary>
        /// <param name="trampaID">Identificador de la Trampa</param>
        /// <returns></returns>
        internal TrampaInfo ObtenerPorID(int trampaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTrampaDAL.ObtenerParametrosPorID(trampaID);
                DataSet ds = Retrieve("Trampa_ObtenerPorID", parameters);
                TrampaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTrampaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Trampa
        /// </summary>
        /// <param name="descripcion">Descripción de la Trampa</param>
        /// <returns></returns>
        internal TrampaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTrampaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Trampa_ObtenerPorDescripcion", parameters);
                TrampaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTrampaDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una entidad por su Organizacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<TrampaInfo> ObtenerPorOrganizacion(int organizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxTrampaDAL.ObtenerParametrosPorOrganizacion(organizacionID);
                DataSet ds = Retrieve("Trampa_ObtenerPorOrganizacion", parameters);
                List<TrampaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTrampaDAL.ObtenerPorOrganizacion(ds);
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
        /// Obtiene un registro de Trampa
        /// </summary>
        /// <param name="hostName">hostName de la Trampa</param>
        /// <returns></returns>
        internal TrampaInfo ObtenerPorHostName(string hostName)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object> { { "@HostName", hostName } };
                DataSet ds = Retrieve("Trampa_ObtenerPorHostName", parametros);
                TrampaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTrampaDAL.ObtenerPorDescripcion(ds);
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
        /// Metrodo Para obtener la trampa
        /// </summary>
        internal TrampaInfo ObtenerTrampa(TrampaInfo trampaInfo)
        {
            try
            {
                Logger.Info();
                var parameters = AuxTrampaDAL.ObtenerParametrosObtenerTrampa(trampaInfo);
                var ds = Retrieve("Trampa_ObtenerOrganizacionTipoHostName", parameters);
                TrampaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapTrampaDAL.ObtenerObtenerTrampa(ds);
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

