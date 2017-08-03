using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.ORM
{
    internal class SolicitudProductoReplicaDAL : DALBase
    {

        /// <summary>
        /// Obtiene una folio de solicitud de productos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public SolicitudProductoReplicaInfo ObtenerPorID(SolicitudProductoReplicaInfo filtro)
        {
            SolicitudProductoReplicaInfo solicitudProducto = null;
            try
            {
                Dictionary<string, object> parameters = AuxSolicitudProductoReplicaDAL.ObtenerParametrosPorID(filtro);
                DataSet ds = Retrieve("SolicitudProductoReplica_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    solicitudProducto = MapSolicitudProductoReplicaDAL.ObtenerPorID(ds);
                }
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
            return solicitudProducto;
        }

        /// <summary>
        /// Obtiene una folio de solicitud de productos
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public FolioSolicitudInfo ObtenerPorIDAyuda(FolioSolicitudInfo filtro)
        {
            FolioSolicitudInfo solicitudProducto = null;
            try
            {
                Dictionary<string, object> parameters = AuxSolicitudProductoReplicaDAL.ObtenerParametrosPorID(filtro);
                DataSet ds = Retrieve("SolicitudProductoReplica_ObtenerPorID", parameters);
                if (ValidateDataSet(ds))
                {
                    solicitudProducto = MapSolicitudProductoReplicaDAL.ObtenerPorIDAyuda(ds);
                }
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
            return solicitudProducto;
        }


        /// <summary>
        /// Obtiene una lista paginada de SolicitudProducto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SolicitudProductoReplicaInfo> ObtenerPorPagina(SolicitudProductoReplicaInfo filtro)
        {
            ResultadoInfo<SolicitudProductoReplicaInfo> solicitudProductoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxSolicitudProductoReplicaDAL.ObtenerParametrosPorPagina(filtro);
                DataSet ds = Retrieve("SolicitudProductoReplica_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    solicitudProductoLista = MapSolicitudProductoReplicaDAL.ObtenerPorPagina(ds);
                }
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
            return solicitudProductoLista;
        }

        /// <summary>
        /// Obtiene una lista paginada de SolicitudProducto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<FolioSolicitudInfo> ObtenerPorPaginaAyuda(FolioSolicitudInfo filtro)
        {
            ResultadoInfo<FolioSolicitudInfo> solicitudProductoLista = null;
            try
            {
                Dictionary<string, object> parameters = AuxSolicitudProductoReplicaDAL.ObtenerParametrosPorPagina(filtro);
                DataSet ds = Retrieve("SolicitudProductoReplica_ObtenerPorPagina", parameters);
                if (ValidateDataSet(ds))
                {
                    solicitudProductoLista = MapSolicitudProductoReplicaDAL.ObtenerPorPaginaAyuda(ds);
                }
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
            return solicitudProductoLista;
        }

    }
}
