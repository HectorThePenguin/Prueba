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
    public class CentroCostoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de CentroCosto
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(CentroCostoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCentroCostoDAL.ObtenerParametrosCrear(info);
                int result = Create("CentroCosto_Crear", parameters);
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
        /// Metodo para actualizar un registro de CentroCosto
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(CentroCostoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCentroCostoDAL.ObtenerParametrosActualizar(info);
                Update("CentroCosto_Actualizar", parameters);
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
        public ResultadoInfo<CentroCostoInfo> ObtenerPorPagina(PaginacionInfo pagina, CentroCostoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxCentroCostoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("CentroCosto_ObtenerPorPagina", parameters);
                ResultadoInfo<CentroCostoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCentroCostoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de CentroCosto
        /// </summary>
        /// <returns></returns>
        public IList<CentroCostoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("CentroCosto_ObtenerTodos");
                IList<CentroCostoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCentroCostoDAL.ObtenerTodos(ds);
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
        public IList<CentroCostoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCentroCostoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("CentroCosto_ObtenerTodos", parameters);
                IList<CentroCostoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCentroCostoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de CentroCosto
        /// </summary>
        /// <param name="centroCostoID">Identificador de la CentroCosto</param>
        /// <returns></returns>
        public CentroCostoInfo ObtenerPorID(int centroCostoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCentroCostoDAL.ObtenerParametrosPorID(centroCostoID);
                DataSet ds = Retrieve("CentroCosto_ObtenerPorID", parameters);
                CentroCostoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCentroCostoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de CentroCosto
        /// </summary>
        /// <param name="descripcion">Descripción de la CentroCosto</param>
        /// <returns></returns>
        public CentroCostoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxCentroCostoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("CentroCosto_ObtenerPorDescripcion", parameters);
                CentroCostoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapCentroCostoDAL.ObtenerPorDescripcion(ds);
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

