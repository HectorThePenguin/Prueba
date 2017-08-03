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
    public class CheckListRoladoraDetalleDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CheckListRoladoraDetalle
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(CheckListRoladoraDetalleInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDetalleDAL.ObtenerParametrosCrear(info);
                int result = Create("CheckListRoladoraDetalle_Crear", parameters);
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
        /// Metodo para actualizar un registro de CheckListRoladoraDetalle
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(CheckListRoladoraDetalleInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDetalleDAL.ObtenerParametrosActualizar(info);
                Update("CheckListRoladoraDetalle_Actualizar", parameters);
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
        public ResultadoInfo<CheckListRoladoraDetalleInfo> ObtenerPorPagina(PaginacionInfo pagina, CheckListRoladoraDetalleInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCheckListRoladoraDetalleDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CheckListRoladoraDetalle_ObtenerPorPagina", parameters);
                ResultadoInfo<CheckListRoladoraDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDetalleDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CheckListRoladoraDetalle
        /// </summary>
        /// <returns></returns>
        public IList<CheckListRoladoraDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CheckListRoladoraDetalle_ObtenerTodos");
                IList<CheckListRoladoraDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDetalleDAL.ObtenerTodos(ds);
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
        public IList<CheckListRoladoraDetalleInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDetalleDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CheckListRoladoraDetalle_ObtenerTodos", parameters);
                IList<CheckListRoladoraDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDetalleDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CheckListRoladoraDetalle
        /// </summary>
        /// <param name="checkListRoladoraDetalleID">Identificador de la CheckListRoladoraDetalle</param>
        /// <returns></returns>
        public CheckListRoladoraDetalleInfo ObtenerPorID(int checkListRoladoraDetalleID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDetalleDAL.ObtenerParametrosPorID(checkListRoladoraDetalleID);
                DataSet ds = Retrieve("CheckListRoladoraDetalle_ObtenerPorID", parameters);
                CheckListRoladoraDetalleInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDetalleDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CheckListRoladoraDetalle
        /// </summary>
        /// <param name="descripcion">Descripción de la CheckListRoladoraDetalle</param>
        /// <returns></returns>
        public CheckListRoladoraDetalleInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDetalleDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CheckListRoladoraDetalle_ObtenerPorDescripcion", parameters);
                CheckListRoladoraDetalleInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDetalleDAL.ObtenerPorDescripcion(ds);
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

        internal void Crear(List<CheckListRoladoraDetalleInfo> checkListRoladoraDetalle)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDetalleDAL.ObtenerParametrosGuardar(checkListRoladoraDetalle);
                Create("CheckListRoladoraDetalle_Guardar", parameters);
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

        internal List<CheckListRoladoraDetalleInfo> ObtenerCheckListCompleto(int organizacionID, int turno, int roladoraId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCheckListRoladoraDetalleDAL.ObtenerParametrosCheckListCompleto(organizacionID, turno, roladoraId);
                DataSet ds = Retrieve("CheckListRoladoraDetalle_ObtenerCheckListCompletado", parameters);
                List<CheckListRoladoraDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCheckListRoladoraDetalleDAL.ObtenerCheckListCompleto(ds);
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
