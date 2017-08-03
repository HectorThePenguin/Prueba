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
    public class RoladoraDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de Roladora
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(RoladoraInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRoladoraDAL.ObtenerParametrosCrear(info);
                int result = Create("Roladora_Crear", parameters);
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
        /// Metodo para actualizar un registro de Roladora
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(RoladoraInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRoladoraDAL.ObtenerParametrosActualizar(info);
                Update("Roladora_Actualizar", parameters);
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
        public ResultadoInfo<RoladoraInfo> ObtenerPorPagina(PaginacionInfo pagina, RoladoraInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxRoladoraDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("Roladora_ObtenerPorPagina", parameters);
                ResultadoInfo<RoladoraInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRoladoraDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de Roladora
        /// </summary>
        /// <returns></returns>
        public IList<RoladoraInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("Roladora_ObtenerTodos");
                IList<RoladoraInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRoladoraDAL.ObtenerTodos(ds);
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
        public IList<RoladoraInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRoladoraDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("Roladora_ObtenerTodos", parameters);
                IList<RoladoraInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRoladoraDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de Roladora
        /// </summary>
        /// <param name="roladoraID">Identificador de la Roladora</param>
        /// <returns></returns>
        public RoladoraInfo ObtenerPorID(int roladoraID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRoladoraDAL.ObtenerParametrosPorID(roladoraID);
                DataSet ds = Retrieve("Roladora_ObtenerPorID", parameters);
                RoladoraInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRoladoraDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de Roladora
        /// </summary>
        /// <param name="descripcion">Descripción de la Roladora</param>
        /// <returns></returns>
        public RoladoraInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRoladoraDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("Roladora_ObtenerPorDescripcion", parameters);
                RoladoraInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRoladoraDAL.ObtenerPorDescripcion(ds);
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

