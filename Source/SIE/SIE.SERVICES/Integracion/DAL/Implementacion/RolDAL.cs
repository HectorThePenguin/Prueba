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
    internal class RolDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Rol
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(RolInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRolDAL.ObtenerParametrosCrear(info);
                int result = Create("Rol_Crear", parameters);
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
        /// Metodo para actualizar un registro de Rol
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(RolInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRolDAL.ObtenerParametrosActualizar(info);
                Update("Rol_Actualizar", parameters);
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
        internal ResultadoInfo<RolInfo> ObtenerPorPagina(PaginacionInfo pagina, RolInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxRolDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Rol_ObtenerPorPagina", parameters);
                ResultadoInfo<RolInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRolDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Rol
        /// </summary>
        /// <returns></returns>
        internal IList<RolInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Rol_ObtenerTodos");
                IList<RolInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRolDAL.ObtenerTodos(ds);
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
        internal IList<RolInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRolDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Rol_ObtenerTodos", parameters);
                IList<RolInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRolDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Rol
        /// </summary>
        /// <param name="rolID">Identificador de la Rol</param>
        /// <returns></returns>
        internal RolInfo ObtenerPorID(int rolID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRolDAL.ObtenerParametrosPorID(rolID);
                DataSet ds = Retrieve("Rol_ObtenerPorID", parameters);
                RolInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRolDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Rol
        /// </summary>
        /// <param name="descripcion">Descripción de la Rol</param>
        /// <returns></returns>
        internal RolInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRolDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Rol_ObtenerPorDescripcion", parameters);
                RolInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRolDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una lista de Niveles de Alerta del SP NivelAlerta_ObtenerNivelAlerta
        /// </summary>
        /// <returns>Lista de los niveles de alerta</returns>
        public IList<NivelAlertaInfo> ObtenerNivelAlerta()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("NivelAlerta_ObtenerNivelAlerta");
                IList<NivelAlertaInfo> result;
                if (ValidateDataSet(ds))
                {
                    result = MapRolDAL.ObtenerNivelAlerta(ds);
                }
                else
                {
                    result = new List<NivelAlertaInfo>();
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

