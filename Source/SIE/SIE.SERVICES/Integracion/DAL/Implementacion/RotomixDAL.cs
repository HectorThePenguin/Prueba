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
    internal class RotomixDAL:DALBase
    {
        ///<summary>
        /// Obtiene una lista de la tabla RotoMix para cargar el commobox del mismo nombre "RotoMix"
        /// </summary>
        /// <returns></returns>
        internal RotoMixInfo ObtenerRotoMixXOrganizacionYDescripcion(int organizacionId, string Descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRotomixDAL.ObtenerRotoMixXOrganizacionYDescripcion(organizacionId, Descripcion);
                DataSet ds = Retrieve("ProduccionFormula_ObtenerRotoMixXDescripcion", parameters);
                RotoMixInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRotomixDAL.ObtenerRotoMixXOrganizacionYDescripcion(ds);
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
        /// Metodo para Crear un registro de Rotomix
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(RotoMixInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRotomixDAL.ObtenerParametrosCrear(info);
                int result = Create("Rotomix_Crear", parameters);
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
        /// Metodo para actualizar un registro de Rotomix
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(RotoMixInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRotomixDAL.ObtenerParametrosActualizar(info);
                Update("Rotomix_Actualizar", parameters);
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
        internal ResultadoInfo<RotoMixInfo> ObtenerPorPagina(PaginacionInfo pagina, RotoMixInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxRotomixDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Rotomix_ObtenerPorPagina", parameters);
                ResultadoInfo<RotoMixInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRotomixDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Rotomix
        /// </summary>
        /// <returns></returns>
        internal IList<RotoMixInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Rotomix_ObtenerTodos");
                IList<RotoMixInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRotomixDAL.ObtenerTodos(ds);
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
        internal IList<RotoMixInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRotomixDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Rotomix_ObtenerTodos", parameters);
                IList<RotoMixInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRotomixDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Rotomix
        /// </summary>
        /// <param name="rotomixID">Identificador de la Rotomix</param>
        /// <returns></returns>
        internal RotoMixInfo ObtenerPorID(int rotomixID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRotomixDAL.ObtenerParametrosPorID(rotomixID);
                DataSet ds = Retrieve("Rotomix_ObtenerPorID", parameters);
                RotoMixInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRotomixDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Rotomix
        /// </summary>
        /// <param name="descripcion">Descripción de la Rotomix</param>
        /// <returns></returns>
        internal RotoMixInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRotomixDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Rotomix_ObtenerPorDescripcion", parameters);
                RotoMixInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRotomixDAL.ObtenerPorDescripcion(ds);
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
