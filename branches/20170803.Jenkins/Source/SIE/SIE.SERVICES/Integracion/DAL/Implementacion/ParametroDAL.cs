using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.InteropServices;
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
    internal class ParametroDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Parametro
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(ParametroInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroDAL.ObtenerParametrosCrear(info);
                int result = Create("Parametro_Crear", parameters);
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
        /// Metodo para actualizar un registro de Parametro
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(ParametroInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroDAL.ObtenerParametrosActualizar(info);
                Update("Parametro_Actualizar", parameters);
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
        internal ResultadoInfo<ParametroInfo> ObtenerPorPagina(PaginacionInfo pagina, ParametroInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxParametroDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Parametro_ObtenerPorPagina", parameters);
                ResultadoInfo<ParametroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Parametro
        /// </summary>
        /// <returns></returns>
        internal IList<ParametroInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Parametro_ObtenerTodos");
                IList<ParametroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroDAL.ObtenerTodos(ds);
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
        internal IList<ParametroInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Parametro_ObtenerTodos", parameters);
                IList<ParametroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Parametro
        /// </summary>
        /// <param name="parametroID">Identificador de la Parametro</param>
        /// <returns></returns>
        internal ParametroInfo ObtenerPorID(int parametroID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroDAL.ObtenerParametrosPorID(parametroID);
                DataSet ds = Retrieve("Parametro_ObtenerPorID", parameters);
                ParametroInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Parametro
        /// </summary>
        /// <param name="descripcion">Descripción de la Parametro</param>
        /// <returns></returns>
        internal ParametroInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Parametro_ObtenerPorDescripcion", parameters);
                ParametroInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una entidad por su Clave
        /// </summary>
        /// <param name="parametroInfo"></param>
        /// <returns></returns>
        internal ParametroInfo ObtenerPorParametroTipoParametro(ParametroInfo parametroInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxParametroDAL.ObtenerParametrosPorParametroTipoParametro(parametroInfo);
                DataSet ds = Retrieve("Parametro_ObtenerPorParametroTipoParametro", parameters);
                ParametroInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapParametroDAL.ObtenerPorParametroTipoParametro(ds);
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
