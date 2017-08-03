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
    internal class CheckListCorralDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CheckListCorral
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        internal int Crear(CheckListCorralInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListCorralDAL.ObtenerParametrosCrear(info);
                int result = Create("CheckListCorral_Crear", parameters);
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
        /// Metodo para actualizar un registro de CheckListCorral
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        internal void Actualizar(CheckListCorralInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListCorralDAL.ObtenerParametrosActualizar(info);
                Update("CheckListCorral_Actualizar", parameters);
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
        internal ResultadoInfo<CheckListCorralInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListCorralInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCheckListCorralDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CheckListCorral_ObtenerPorPagina", parameters);
                ResultadoInfo<CheckListCorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListCorralDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CheckListCorral
        /// </summary>
        /// <returns></returns>
        internal IList<CheckListCorralInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CheckListCorral_ObtenerTodos");
                IList<CheckListCorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListCorralDAL.ObtenerTodos(ds);
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
        internal IList<CheckListCorralInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListCorralDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CheckListCorral_ObtenerTodos", parameters);
                IList<CheckListCorralInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListCorralDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CheckListCorral
        /// </summary>
        /// <param name="checkListCorralID">Identificador de la CheckListCorral</param>
        /// <returns></returns>
        internal CheckListCorralInfo ObtenerPorID(int checkListCorralID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListCorralDAL.ObtenerParametrosPorID(checkListCorralID);
                DataSet ds = Retrieve("CheckListCorral_ObtenerPorID", parameters);
                CheckListCorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListCorralDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CheckListCorral
        /// </summary>
        /// <param name="descripcion">Descripción de la CheckListCorral</param>
        /// <returns></returns>
        internal CheckListCorralInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListCorralDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CheckListCorral_ObtenerPorDescripcion", parameters);
                CheckListCorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListCorralDAL.ObtenerPorDescripcion(ds);
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
        /// Obtiene un registro de CheckListCorral
        /// </summary>
        /// <param name="loteID">identificador del Lote</param>
        /// <param name="organizacionID">identificador de la Organización</param>
        /// <returns></returns>
        internal CheckListCorralInfo ObtenerPorLote(int organizacionID, int loteID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListCorralDAL.ObtenerParametrosPorLote(organizacionID, loteID);
                DataSet ds = Retrieve("CheckListCorral_ObtenerCheckListPorLote", parameters);
                CheckListCorralInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListCorralDAL.ObtenerPorLote(ds);
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
        /// Obtiene un registro de CheckListCorral
        /// </summary>
        /// <param name="filtro">identificador del Lote</param>
        /// <returns></returns>
        internal LoteProyeccionInfo GenerarProyeccion(CheckListCorralInfo filtro)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListCorralDAL.ObtenerParametrosGenerarProyeccion(filtro);
                DataSet ds = Retrieve("GenerarProyeccion", parameters);
                LoteProyeccionInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListCorralDAL.GenerarProyeccion(ds);
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

