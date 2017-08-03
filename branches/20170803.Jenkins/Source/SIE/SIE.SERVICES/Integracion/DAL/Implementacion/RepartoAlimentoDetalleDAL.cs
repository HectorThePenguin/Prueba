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
    public class RepartoAlimentoDetalleDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de RepartoAlimentoDetalle
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(RepartoAlimentoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDetalleDAL.ObtenerParametrosCrear(info);
                int result = Create("RepartoAlimentoDetalle_Crear", parameters);
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
        /// Metodo para actualizar un registro de RepartoAlimentoDetalle
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(RepartoAlimentoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDetalleDAL.ObtenerParametrosActualizar(info);
                Update("RepartoAlimentoDetalle_Actualizar", parameters);
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
        public ResultadoInfo<RepartoAlimentoDetalleInfo> ObtenerPorPagina(PaginacionInfo pagina, RepartoAlimentoDetalleInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxRepartoAlimentoDetalleDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("RepartoAlimentoDetalle_ObtenerPorPagina", parameters);
                ResultadoInfo<RepartoAlimentoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDetalleDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de RepartoAlimentoDetalle
        /// </summary>
        /// <returns></returns>
        public IList<RepartoAlimentoDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("RepartoAlimentoDetalle_ObtenerTodos");
                IList<RepartoAlimentoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDetalleDAL.ObtenerTodos(ds);
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
        public IList<RepartoAlimentoDetalleInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDetalleDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("RepartoAlimentoDetalle_ObtenerTodos", parameters);
                IList<RepartoAlimentoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDetalleDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de RepartoAlimentoDetalle
        /// </summary>
        /// <param name="repartoAlimentoDetalleID">Identificador de la RepartoAlimentoDetalle</param>
        /// <returns></returns>
        public RepartoAlimentoDetalleInfo ObtenerPorID(int repartoAlimentoDetalleID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDetalleDAL.ObtenerParametrosPorID(repartoAlimentoDetalleID);
                DataSet ds = Retrieve("RepartoAlimentoDetalle_ObtenerPorID", parameters);
                RepartoAlimentoDetalleInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDetalleDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de RepartoAlimentoDetalle
        /// </summary>
        /// <param name="descripcion">Descripción de la RepartoAlimentoDetalle</param>
        /// <returns></returns>
        public RepartoAlimentoDetalleInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDetalleDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("RepartoAlimentoDetalle_ObtenerPorDescripcion", parameters);
                RepartoAlimentoDetalleInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDetalleDAL.ObtenerPorDescripcion(ds);
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
        /// Metodo para Crear un registro de RepartoAlimentoDetalle
        /// </summary>
        /// <param name="listaDetalle">Valores de la entidad que será creada</param>
        public int Guardar(List<RepartoAlimentoDetalleInfo> listaDetalle)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDetalleDAL.ObtenerParametrosGuardar(listaDetalle);
                int result = Create("RepartoAlimentoDetalle_GuardarXml", parameters);
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
        public IList<RepartoAlimentoDetalleInfo> ObtenerPorRepartoAlimentoID(int repartoAlimentoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxRepartoAlimentoDetalleDAL.ObtenerParametrosPorRepartoAlimentoID(repartoAlimentoID);
                DataSet ds = Retrieve("RepartoAlimentoDetalle_ObtenerPorRepartoAlimento", parameters);
                IList<RepartoAlimentoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapRepartoAlimentoDetalleDAL.ObtenerPorRepartoAlimentoID(ds);
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

