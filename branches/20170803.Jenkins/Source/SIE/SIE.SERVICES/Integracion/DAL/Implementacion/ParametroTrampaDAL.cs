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
    internal class ParametroTrampaDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de ParametroTrampa
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(ParametroTrampaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroTrampaDAL.ObtenerParametrosCrear(info);
                int result = Create("ParametroTrampa_Crear", parameters);
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
        /// Metodo para actualizar un registro de ParametroTrampa
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(ParametroTrampaInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroTrampaDAL.ObtenerParametrosActualizar(info);
                Update("ParametroTrampa_Actualizar", parameters);
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
        internal ResultadoInfo<ParametroTrampaInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroTrampaInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxParametroTrampaDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ParametroTrampa_ObtenerPorPagina", parameters);
                ResultadoInfo<ParametroTrampaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroTrampaDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de ParametroTrampa
        /// </summary>
        /// <returns></returns>
        internal IList<ParametroTrampaInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ParametroTrampa_ObtenerTodos");
                IList<ParametroTrampaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroTrampaDAL.ObtenerTodos(ds);
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
        internal IList<ParametroTrampaInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroTrampaDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("ParametroTrampa_ObtenerTodos", parameters);
                IList<ParametroTrampaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroTrampaDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de ParametroTrampa
        /// </summary>
        /// <param name="parametroTrampaID">Identificador de la ParametroTrampa</param>
        /// <returns></returns>
        internal ParametroTrampaInfo ObtenerPorID(int parametroTrampaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroTrampaDAL.ObtenerParametrosPorID(parametroTrampaID);
                DataSet ds = Retrieve("ParametroTrampa_ObtenerPorID", parameters);
                ParametroTrampaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroTrampaDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de ParametroTrampa
        /// </summary>
        /// <param name="descripcion">Descripción de la ParametroTrampa</param>
        /// <returns></returns>
        internal ParametroTrampaInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroTrampaDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("ParametroTrampa_ObtenerPorDescripcion", parameters);
                ParametroTrampaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroTrampaDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un Parametro trampa por parametro y trampa
        /// </summary>
        /// <param name="parametroID"></param>
        /// <param name="trampaID"></param>
        /// <returns></returns>
        internal ParametroTrampaInfo ObtenerPorParametroTrampa(int parametroID, int trampaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroTrampaDAL.ObtenerParametrosPorParametroTrampa(parametroID, trampaID);
                DataSet ds = Retrieve("ParametroTrampa_ObtenerPorParametroTrampa", parameters);
                ParametroTrampaInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroTrampaDAL.ObtenerPorParametroTrampa(ds);
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
        /// Metodo para Crear un registro de ParametroTrampa
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int ClonarParametroTrampa(FiltroClonarParametroTrampa info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroTrampaDAL.ObtenerParametrosClonarParametroTrampa(info);
                int result = Create("ParametroTrampa_ClonarParametroTrampa", parameters);
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
