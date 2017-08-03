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
    public class SolicitudProductoDetalleDAL : DALBase
    {
        /// <summary>
        /// Metodo para Crear un registro de SolicitudProductoDetalle
        /// </summary>
        /// <param name="info">Valores de la entidad que será creada</param>
        public int Crear(SolicitudProductoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDetalleDAL.ObtenerParametrosCrear(info);
                int result = Create("SolicitudProductoDetalle_Crear", parameters);
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
        /// Metodo para actualizar un registro de SolicitudProductoDetalle
        /// </summary>
        /// <param name="info">Valores de la entidad que se actualizarán</param>
        public void Actualizar(SolicitudProductoDetalleInfo info)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDetalleDAL.ObtenerParametrosActualizar(info);
                Update("SolicitudProductoDetalle_Actualizar", parameters);
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
        public ResultadoInfo<SolicitudProductoDetalleInfo> ObtenerPorPagina(PaginacionInfo pagina, SolicitudProductoDetalleInfo filtro)
        {
            try
            {
                Dictionary<string, object> parameters = AuxSolicitudProductoDetalleDAL.ObtenerParametrosPorPagina(pagina, filtro);
                DataSet ds = Retrieve("SolicitudProductoDetalle_ObtenerPorPagina", parameters);
                ResultadoInfo<SolicitudProductoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudProductoDetalleDAL.ObtenerPorPagina(ds);
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
        /// Obtiene una lista de SolicitudProductoDetalle
        /// </summary>
        /// <returns></returns>
        public IList<SolicitudProductoDetalleInfo> ObtenerTodos()
        {
            try
            {
                Logger.Info();
                DataSet ds = Retrieve("SolicitudProductoDetalle_ObtenerTodos");
                IList<SolicitudProductoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudProductoDetalleDAL.ObtenerTodos(ds);
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
        public IList<SolicitudProductoDetalleInfo> ObtenerTodos(EstatusEnum estatus)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDetalleDAL.ObtenerTodos(estatus);
                DataSet ds = Retrieve("SolicitudProductoDetalle_ObtenerTodos", parameters);
                IList<SolicitudProductoDetalleInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudProductoDetalleDAL.ObtenerTodos(ds);
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
        /// Obtiene un registro de SolicitudProductoDetalle
        /// </summary>
        /// <param name="solicitudProductoDetalleID">Identificador de la SolicitudProductoDetalle</param>
        /// <returns></returns>
        public SolicitudProductoDetalleInfo ObtenerPorID(int solicitudProductoDetalleID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDetalleDAL.ObtenerParametrosPorID(solicitudProductoDetalleID);
                DataSet ds = Retrieve("SolicitudProductoDetalle_ObtenerPorID", parameters);
                SolicitudProductoDetalleInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudProductoDetalleDAL.ObtenerPorID(ds);
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
        /// Obtiene un registro de SolicitudProductoDetalle
        /// </summary>
        /// <param name="descripcion">Descripción de la SolicitudProductoDetalle</param>
        /// <returns></returns>
        public SolicitudProductoDetalleInfo ObtenerPorDescripcion(string descripcion)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDetalleDAL.ObtenerParametrosPorDescripcion(descripcion);
                DataSet ds = Retrieve("SolicitudProductoDetalle_ObtenerPorDescripcion", parameters);
                SolicitudProductoDetalleInfo result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapSolicitudProductoDetalleDAL.ObtenerPorDescripcion(ds);
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
        /// Metodo para Crear un registro de SolicitudProductoDetalle
        /// </summary>
        /// <param name="lista">Valores de la entidad que será creada</param>
        /// <param name="solicitudProductoID">Valores de la entidad que será creada</param>
        public int GuardarSolicitudDetalle(List<SolicitudProductoDetalleInfo> lista, int solicitudProductoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxSolicitudProductoDetalleDAL.GuardarSolicitudDetalle(lista, solicitudProductoID);
                int result = Create("SolicitudProductoDetalle_GuardarDetalle", parameters);
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
        /// Metodo para marcar los productos que se recepcionan de una solicitud de producto
        /// </summary>
        /// <param name="lista">Valores de la entidad que será creada</param>
        /// <param name="solicitudProductoID">Valores de la entidad que será creada</param>
        public int MarcarProductosRecibidos(List<SolicitudProductoReplicaDetalleInfo> lista, long folioSolicitud)
        {
            try
            {
                Logger.Info();
                //Dictionary<string, object> parameters = AuxSolicitudProductoDetalleDAL.GuardarSolicitudDetalle(lista, solicitudProductoID);
                Dictionary<string, object> parameters = AuxSolicitudProductoDetalleDAL.MarcarProductosRecibidos(lista, folioSolicitud);
                int result = Create("SolicitudProductoDetalle_MarcarRecepcionProductos", parameters);
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

