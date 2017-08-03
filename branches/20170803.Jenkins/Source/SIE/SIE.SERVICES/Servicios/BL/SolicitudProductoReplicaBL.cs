using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.ORM;

namespace SIE.Services.Servicios.BL
{
    public class SolicitudProductoReplicaBL 
    {

        /// <summary>
        /// Obtiene un folio de solicitud por ID 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public SolicitudProductoReplicaInfo ObtenerPorID(SolicitudProductoReplicaInfo filtro)
        {
            try
            {
                Logger.Info();
                SolicitudProductoReplicaDAL solicitudProductoDAL = new SolicitudProductoReplicaDAL(); 
                return solicitudProductoDAL.ObtenerPorID(filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista paginada de SolicitudProducto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<SolicitudProductoReplicaInfo> ObtenerPorPagina(SolicitudProductoReplicaInfo filtro)
        {
            try
            {
                Logger.Info();
                SolicitudProductoReplicaDAL solicitudProductoDAL = new SolicitudProductoReplicaDAL();
                return solicitudProductoDAL.ObtenerPorPagina(filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene una lista paginada de SolicitudProducto
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public ResultadoInfo<FolioSolicitudInfo> ObtenerPorPaginaAyuda(PaginacionInfo pagina, FolioSolicitudInfo filtro)
        {
            try
            {
                Logger.Info();
                SolicitudProductoReplicaDAL solicitudProductoDAL = new SolicitudProductoReplicaDAL();
                return solicitudProductoDAL.ObtenerPorPaginaAyuda(filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un folio de solicitud por ID 
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public FolioSolicitudInfo ObtenerPorIDAyuda(FolioSolicitudInfo filtro)
        {
            try
            {
                Logger.Info();
                SolicitudProductoReplicaDAL solicitudProductoDAL = new SolicitudProductoReplicaDAL(); ;
                return solicitudProductoDAL.ObtenerPorIDAyuda(filtro);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
