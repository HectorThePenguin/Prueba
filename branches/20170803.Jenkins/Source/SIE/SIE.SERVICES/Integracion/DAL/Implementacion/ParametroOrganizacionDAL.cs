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
    internal class ParametroOrganizacionDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de ParametroOrganizacion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(ParametroOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroOrganizacionDAL.ObtenerParametrosCrear(info);
                int result = Create("ParametroOrganizacion_Crear", parameters);
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
        /// Metodo para actualizar un registro de ParametroOrganizacion
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(ParametroOrganizacionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroOrganizacionDAL.ObtenerParametrosActualizar(info);
                Update("ParametroOrganizacion_Actualizar", parameters);
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
        internal ResultadoInfo<ParametroOrganizacionInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroOrganizacionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxParametroOrganizacionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("ParametroOrganizacion_ObtenerPorPagina", parameters);
                ResultadoInfo<ParametroOrganizacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroOrganizacionDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de ParametroOrganizacion
        /// </summary>
        /// <returns></returns>
        internal IList<ParametroOrganizacionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("ParametroOrganizacion_ObtenerTodos");
                IList<ParametroOrganizacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroOrganizacionDAL.ObtenerTodos(ds);
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
        internal IList<ParametroOrganizacionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroOrganizacionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("ParametroOrganizacion_ObtenerTodos", parameters);
                IList<ParametroOrganizacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroOrganizacionDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de ParametroOrganizacion
        /// </summary>
        /// <param name="parametroOrganizacionID">Identificador de la ParametroOrganizacion</param>
        /// <returns></returns>
        internal ParametroOrganizacionInfo ObtenerPorID(int parametroOrganizacionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroOrganizacionDAL.ObtenerParametrosPorID(parametroOrganizacionID);
                DataSet ds = Retrieve("ParametroOrganizacion_ObtenerPorID", parameters);
                ParametroOrganizacionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroOrganizacionDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de ParametroOrganizacion
        /// </summary>
        /// <param name="descripcion">Descripción de la ParametroOrganizacion</param>
        /// <returns></returns>
        internal ParametroOrganizacionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroOrganizacionDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("ParametroOrganizacion_ObtenerPorDescripcion", parameters);
                ParametroOrganizacionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroOrganizacionDAL.ObtenerPorDescripcion(ds);
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
        internal ResultadoInfo<ParametroOrganizacionInfo> ObtenerPorFiltroPagina(PaginacionInfo pagina, ParametroOrganizacionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxParametroOrganizacionDAL.ObtenerParametrosPorFiltroPagina(pagina, filtro);
                DataSet ds = Retrieve("ParametroOrganizacion_ObtenerPorFiltroPagina", parameters);
                ResultadoInfo<ParametroOrganizacionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroOrganizacionDAL.ObtenerPorFiltroPagina(ds);
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
        /// Obtiene un registro de ParametroOrganizacion
        /// </summary>
        /// <param name="filtro">contenedor de los parámetros</param>
        /// <returns></returns>
        internal ParametroOrganizacionInfo ObtenerPorParametroOrganizacionID(ParametroOrganizacionInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroOrganizacionDAL.ObtenerParametrosPorParametroOrganizacionID(filtro);
                DataSet ds = Retrieve("ParametroOrganizacion_ObtenerPorParametroOrganizacionID", parameters);
                ParametroOrganizacionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroOrganizacionDAL.ObtenerPorParametroOrganizacionID(ds);
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
        /// Obtiene una entidad por organizacion y clave parametro
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="claveParametro"></param>
        /// <returns></returns>
        internal ParametroOrganizacionInfo ObtenerPorOrganizacionIDClaveParametro(int organizacionID, string claveParametro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroOrganizacionDAL.ObtenerParametrosPorOrganizacionIDClaveParametro(organizacionID, claveParametro);
                using (IDataReader reader = RetrieveReader("ParametroOrganizacion_ObtenerPorOrganizacionClave", parameters))
                {
                    ParametroOrganizacionInfo result = null;
                    if (ValidateDataReader(reader))
                    {
                        result = MapParametroOrganizacionDAL.ObtenerPorOrganizacionIDClaveParametro(reader);
                    }
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                    return result;
                }
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

