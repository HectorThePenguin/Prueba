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
    internal class CheckListRoladoraHorometroDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CheckListRoladoraHorometro
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(CheckListRoladoraHorometroInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraHorometroDAL.ObtenerParametrosCrear(info);
                int result = Create("CheckListRoladoraHorometro_Crear", parameters);
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
        /// Metodo para actualizar un registro de CheckListRoladoraHorometro
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CheckListRoladoraHorometroInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraHorometroDAL.ObtenerParametrosActualizar(info);
                Update("CheckListRoladoraHorometro_Actualizar", parameters);
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
        internal ResultadoInfo<CheckListRoladoraHorometroInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraHorometroInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCheckListRoladoraHorometroDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CheckListRoladoraHorometro_ObtenerPorPagina", parameters);
                ResultadoInfo<CheckListRoladoraHorometroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraHorometroDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CheckListRoladoraHorometro
        /// </summary>
        /// <returns></returns>
        internal IList<CheckListRoladoraHorometroInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CheckListRoladoraHorometro_ObtenerTodos");
                IList<CheckListRoladoraHorometroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraHorometroDAL.ObtenerTodos(ds);
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
        internal IList<CheckListRoladoraHorometroInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraHorometroDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CheckListRoladoraHorometro_ObtenerTodos", parameters);
                IList<CheckListRoladoraHorometroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraHorometroDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CheckListRoladoraHorometro
        /// </summary>
        /// <param name="checkListRoladoraHorometroID">Identificador de la CheckListRoladoraHorometro</param>
        /// <returns></returns>
        internal CheckListRoladoraHorometroInfo ObtenerPorID(int checkListRoladoraHorometroID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraHorometroDAL.ObtenerParametrosPorID(checkListRoladoraHorometroID);
                DataSet ds = Retrieve("CheckListRoladoraHorometro_ObtenerPorID", parameters);
                CheckListRoladoraHorometroInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraHorometroDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CheckListRoladoraHorometro
        /// </summary>
        /// <param name="descripcion">Descripción de la CheckListRoladoraHorometro</param>
        /// <returns></returns>
        internal CheckListRoladoraHorometroInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraHorometroDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CheckListRoladoraHorometro_ObtenerPorDescripcion", parameters);
                CheckListRoladoraHorometroInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraHorometroDAL.ObtenerPorDescripcion(ds);
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
        /// Metodo para Crear un registro de CheckListRoladoraHorometro
        /// </summary>
        /// <param name="checkListRoladoraHorometro">Valores de la entidad que será creada</param>
        internal int Crear(List<CheckListRoladoraHorometroInfo> checkListRoladoraHorometro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraHorometroDAL.ObtenerParametrosCrear(checkListRoladoraHorometro);
                int result = Create("CheckListRoladoraHorometro_Guardar", parameters);
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
        internal List<CheckListRoladoraHorometroInfo> ObtenerPorCheckListRoladoraGeneralID(int checkListRoladoraGeneralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraHorometroDAL.ObtenerParametrosObtenerPorCheckListRoladoraGeneralID(checkListRoladoraGeneralID);
                DataSet ds = Retrieve("CheckListRoladoraHorometro_ObtenerPorCheckListGeneralID", parameters);
                List<CheckListRoladoraHorometroInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraHorometroDAL.ObtenerPorCheckListRoladoraGeneralID(ds);
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

