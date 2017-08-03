using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AuxRecepcionProductoDAL
    {
        /// <summary>
        /// Obtiene los parametros para guardar
        /// </summary>
        /// <param name="recepcionProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardar(RecepcionProductoInfo recepcionProducto)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID",recepcionProducto.Almacen.Organizacion.OrganizacionID},
                                     {"@AlmacenID", recepcionProducto.Almacen.AlmacenID},
                                     {"@FolioOrdenCompra", recepcionProducto.FolioOrdenCompra},
                                     {"@FechaSolicitud",recepcionProducto.FechaSolicitud},
                                     {"@ProveedorID",recepcionProducto.Proveedor.ProveedorID},
                                     {"@AlmacenMovimientoID",recepcionProducto.AlmacenMovimientoId},
                                     {"@Factura",recepcionProducto.Factura},
                                     {"@UsuarioCreacionID",recepcionProducto.UsuarioCreacion.UsuarioID},
                                     {"@TipoFolio",(int)TipoFolio.RecepcionProducto},
                                     {"@Activo", EstatusEnum.Activo}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerRecepcionPorFolio(RecepcionProductoInfo recepcionProductoCompra)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@FolioOrdenCompra", recepcionProductoCompra.FolioOrdenCompra},
                                     {"@Activo", EstatusEnum.Activo}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// RecepcionProducto_ObtenerPorFolioOrganizacion
        /// </summary>
        /// <param name="recepcionProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorFolioOrganizacion(RecepcionProductoInfo recepcionProducto)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@FolioRecepcion", recepcionProducto.FolioRecepcion},
                                     {"@OrganizacionID", recepcionProducto.Almacen.Organizacion.OrganizacionID}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para 
        /// la ejecucion del procedimiento almacenado
        /// RecepcionProducto_ObtenerPorFolioOrganizacionPaginado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="recepcionProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorFolioOrganizacionPaginado(PaginacionInfo pagina, RecepcionProductoInfo recepcionProducto)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@FolioRecepcion", recepcionProducto.FolioRecepcion},
                                     {"@OrganizacionID", recepcionProducto.Almacen.Organizacion.OrganizacionID},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite},
                                     {"@Proveedor", recepcionProducto.Proveedor.Descripcion},
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado AlmacenMovimientoCosto_ObtenerPorAlmacenMovimientoXML
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>();
                var xml =
                    new XElement("ROOT",
                                 from detalle in almacenesMovimiento
                                 select new XElement("AlmacenMovimiento",
                                                     new XElement("AlmacenMovimientoID", detalle.AlmacenMovimientoID)
                                     ));
                parametros.Add("@XmlAlmacenMovimiento", xml.ToString());
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
