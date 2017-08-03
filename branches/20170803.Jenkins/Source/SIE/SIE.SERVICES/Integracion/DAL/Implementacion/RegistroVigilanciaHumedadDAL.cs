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
    internal class RegistroVigilanciaHumedadDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de RegistroVigilanciaHumedad
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(RegistroVigilanciaHumedadInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRegistroVigilanciaHumedadDAL.ObtenerParametrosCrear(info);
                int result = Create("RegistroVigilanciaHumedad_Crear", parameters);
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
        /// Metodo para actualizar un registro de RegistroVigilanciaHumedad
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(RegistroVigilanciaHumedadInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRegistroVigilanciaHumedadDAL.ObtenerParametrosActualizar(info);
                Update("RegistroVigilanciaHumedad_Actualizar", parameters);
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
        internal ResultadoInfo<RegistroVigilanciaHumedadInfo> ObtenerPorPagina(PaginacionInfo pagina, RegistroVigilanciaHumedadInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxRegistroVigilanciaHumedadDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("RegistroVigilanciaHumedad_ObtenerPorPagina", parameters);
                ResultadoInfo<RegistroVigilanciaHumedadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaHumedadDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de RegistroVigilanciaHumedad
        /// </summary>
        /// <returns></returns>
        internal IList<RegistroVigilanciaHumedadInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("RegistroVigilanciaHumedad_ObtenerTodos");
                IList<RegistroVigilanciaHumedadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaHumedadDAL.ObtenerTodos(ds);
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
        internal IList<RegistroVigilanciaHumedadInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRegistroVigilanciaHumedadDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("RegistroVigilanciaHumedad_ObtenerTodos", parameters);
                IList<RegistroVigilanciaHumedadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaHumedadDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de RegistroVigilanciaHumedad
        /// </summary>
        /// <param name="registroVigilanciaHumedadID">Identificador de la RegistroVigilanciaHumedad</param>
        /// <returns></returns>
        internal RegistroVigilanciaHumedadInfo ObtenerPorID(int registroVigilanciaHumedadID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRegistroVigilanciaHumedadDAL.ObtenerParametrosPorID(registroVigilanciaHumedadID);
                DataSet ds = Retrieve("RegistroVigilanciaHumedad_ObtenerPorID", parameters);
                RegistroVigilanciaHumedadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaHumedadDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de RegistroVigilanciaHumedad
        /// </summary>
        /// <param name="descripcion">Descripción de la RegistroVigilanciaHumedad</param>
        /// <returns></returns>
        internal RegistroVigilanciaHumedadInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRegistroVigilanciaHumedadDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("RegistroVigilanciaHumedad_ObtenerPorDescripcion", parameters);
                RegistroVigilanciaHumedadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaHumedadDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de RegistroVigilanciaHumedad
        /// </summary>
        /// <param name="registroVigilanciaID">Identificador del Registro de Vigilancia</param>
        /// <returns></returns>
        internal RegistroVigilanciaHumedadInfo ObtenerPorRegistroVigilanciaID(int registroVigilanciaID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRegistroVigilanciaHumedadDAL.ObtenerParametrosPorRegistroVigilanciaID(registroVigilanciaID);
                DataSet ds = Retrieve("RegistroVigilanciaHumedad_ObtenerPorRegistroVigilanciaID", parameters);
                RegistroVigilanciaHumedadInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRegistroVigilanciaHumedadDAL.ObtenerPorRegistroVigilanciaID(ds);
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

