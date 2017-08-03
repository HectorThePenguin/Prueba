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
    public class CheckListRoladoraRangoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CheckListRoladoraRango
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(CheckListRoladoraRangoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraRangoDAL.ObtenerParametrosCrear(info);
                int result = Create("CheckListRoladoraRango_Crear", parameters);
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
        /// Metodo para actualizar un registro de CheckListRoladoraRango
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(CheckListRoladoraRangoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraRangoDAL.ObtenerParametrosActualizar(info);
                Update("CheckListRoladoraRango_Actualizar", parameters);
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
        public ResultadoInfo<CheckListRoladoraRangoInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraRangoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCheckListRoladoraRangoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CheckListRoladoraRango_ObtenerPorPagina", parameters);
                ResultadoInfo<CheckListRoladoraRangoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraRangoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CheckListRoladoraRango
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraRangoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CheckListRoladoraRango_ObtenerTodos");
                IList<CheckListRoladoraRangoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraRangoDAL.ObtenerTodos(ds);
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
        public IList<CheckListRoladoraRangoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraRangoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CheckListRoladoraRango_ObtenerTodos", parameters);
                IList<CheckListRoladoraRangoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraRangoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CheckListRoladoraRango
        /// </summary>
        /// <param name="checkListRoladoraRangoID">Identificador de la CheckListRoladoraRango</param>
        /// <returns></returns>
        public CheckListRoladoraRangoInfo ObtenerPorID(int checkListRoladoraRangoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraRangoDAL.ObtenerParametrosPorID(checkListRoladoraRangoID);
                DataSet ds = Retrieve("CheckListRoladoraRango_ObtenerPorID", parameters);
                CheckListRoladoraRangoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraRangoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CheckListRoladoraRango
        /// </summary>
        /// <param name="descripcion">Descripción de la CheckListRoladoraRango</param>
        /// <returns></returns>
        public CheckListRoladoraRangoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraRangoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CheckListRoladoraRango_ObtenerPorDescripcion", parameters);
                CheckListRoladoraRangoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraRangoDAL.ObtenerPorDescripcion(ds);
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

