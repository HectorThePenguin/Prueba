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
    public class CheckListRoladoraGeneralDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CheckListRoladoraGeneral
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(CheckListRoladoraGeneralInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraGeneralDAL.ObtenerParametrosCrear(info);
                int result = Create("CheckListRoladoraGeneral_Crear", parameters);
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
        /// Metodo para actualizar un registro de CheckListRoladoraGeneral
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(CheckListRoladoraGeneralInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraGeneralDAL.ObtenerParametrosActualizar(info);
                Update("CheckListRoladoraGeneral_Actualizar", parameters);
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
        public ResultadoInfo<CheckListRoladoraGeneralInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraGeneralInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCheckListRoladoraGeneralDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CheckListRoladoraGeneral_ObtenerPorPagina", parameters);
                ResultadoInfo<CheckListRoladoraGeneralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraGeneralDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CheckListRoladoraGeneral
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraGeneralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CheckListRoladoraGeneral_ObtenerTodos");
                IList<CheckListRoladoraGeneralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraGeneralDAL.ObtenerTodos(ds);
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
        public IList<CheckListRoladoraGeneralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraGeneralDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CheckListRoladoraGeneral_ObtenerTodos", parameters);
                IList<CheckListRoladoraGeneralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraGeneralDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CheckListRoladoraGeneral
        /// </summary>
        /// <param name="checkListRoladoraGeneralID">Identificador de la CheckListRoladoraGeneral</param>
        /// <returns></returns>
        public CheckListRoladoraGeneralInfo ObtenerPorID(int checkListRoladoraGeneralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraGeneralDAL.ObtenerParametrosPorID(checkListRoladoraGeneralID);
                DataSet ds = Retrieve("CheckListRoladoraGeneral_ObtenerPorID", parameters);
                CheckListRoladoraGeneralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraGeneralDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CheckListRoladoraGeneral
        /// </summary>
        /// <param name="descripcion">Descripción de la CheckListRoladoraGeneral</param>
        /// <returns></returns>
        public CheckListRoladoraGeneralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraGeneralDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CheckListRoladoraGeneral_ObtenerPorDescripcion", parameters);
                CheckListRoladoraGeneralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraGeneralDAL.ObtenerPorDescripcion(ds);
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

