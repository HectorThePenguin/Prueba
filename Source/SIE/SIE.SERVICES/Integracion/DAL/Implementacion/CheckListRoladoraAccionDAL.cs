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
    public class CheckListRoladoraAccionDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CheckListRoladoraAccion
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(CheckListRoladoraAccionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraAccionDAL.ObtenerParametrosCrear(info);
                int result = Create("CheckListRoladoraAccion_Crear", parameters);
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
        /// Metodo para actualizar un registro de CheckListRoladoraAccion
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(CheckListRoladoraAccionInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraAccionDAL.ObtenerParametrosActualizar(info);
                Update("CheckListRoladoraAccion_Actualizar", parameters);
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
        public ResultadoInfo<CheckListRoladoraAccionInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraAccionInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCheckListRoladoraAccionDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CheckListRoladoraAccion_ObtenerPorPagina", parameters);
                ResultadoInfo<CheckListRoladoraAccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraAccionDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CheckListRoladoraAccion
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraAccionInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CheckListRoladoraAccion_ObtenerTodos");
                IList<CheckListRoladoraAccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraAccionDAL.ObtenerTodos(ds);
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
        public IList<CheckListRoladoraAccionInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraAccionDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CheckListRoladoraAccion_ObtenerTodos", parameters);
                IList<CheckListRoladoraAccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraAccionDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CheckListRoladoraAccion
        /// </summary>
        /// <param name="checkListRoladoraAccionID">Identificador de la CheckListRoladoraAccion</param>
        /// <returns></returns>
        public CheckListRoladoraAccionInfo ObtenerPorID(int checkListRoladoraAccionID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraAccionDAL.ObtenerParametrosPorID(checkListRoladoraAccionID);
                DataSet ds = Retrieve("CheckListRoladoraAccion_ObtenerPorID", parameters);
                CheckListRoladoraAccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraAccionDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CheckListRoladoraAccion
        /// </summary>
        /// <param name="descripcion">Descripción de la CheckListRoladoraAccion</param>
        /// <returns></returns>
        public CheckListRoladoraAccionInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraAccionDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CheckListRoladoraAccion_ObtenerPorDescripcion", parameters);
                CheckListRoladoraAccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraAccionDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene una lista de CheckListRoladoraAccion
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraAccionInfo> ObtenerParametros()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CheckListRoladoraAccion_ObtenerParametros");
                IList<CheckListRoladoraAccionInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraAccionDAL.ObtenerParametros(ds);
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
