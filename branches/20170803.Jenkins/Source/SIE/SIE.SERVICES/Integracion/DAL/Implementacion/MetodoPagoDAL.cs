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
    public class MetodoPagoDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de MetodoPago
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(MetodoPagoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMetodoPagoDAL.ObtenerParametrosCrear(info);
                int result = Create("MetodoPago_Crear", parameters);
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
        /// Metodo para actualizar un registro de MetodoPago
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(MetodoPagoInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMetodoPagoDAL.ObtenerParametrosActualizar(info);
                Update("MetodoPago_Actualizar", parameters);
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
        public ResultadoInfo<MetodoPagoInfo> ObtenerPorPagina(PaginacionInfo pagina, MetodoPagoInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxMetodoPagoDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("MetodoPago_ObtenerPorPagina", parameters);
                ResultadoInfo<MetodoPagoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMetodoPagoDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de MetodoPago
        /// </summary>
        /// <returns></returns>
        public IList<MetodoPagoInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("MetodoPago_ObtenerTodos");
                IList<MetodoPagoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMetodoPagoDAL.ObtenerTodos(ds);
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
        public IList<MetodoPagoInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMetodoPagoDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("MetodoPago_ObtenerTodos", parameters);
                IList<MetodoPagoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMetodoPagoDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de MetodoPago
        /// </summary>
        /// <param name="metodoPagoID">Identificador de la MetodoPago</param>
        /// <returns></returns>
        public MetodoPagoInfo ObtenerPorID(int metodoPagoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMetodoPagoDAL.ObtenerParametrosPorID(metodoPagoID);
                DataSet ds = Retrieve("MetodoPago_ObtenerPorID", parameters);
                MetodoPagoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMetodoPagoDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de MetodoPago
        /// </summary>
        /// <param name="descripcion">Descripción de la MetodoPago</param>
        /// <returns></returns>
        public MetodoPagoInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxMetodoPagoDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("MetodoPago_ObtenerPorDescripcion", parameters);
                MetodoPagoInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapMetodoPagoDAL.ObtenerPorDescripcion(ds);
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

