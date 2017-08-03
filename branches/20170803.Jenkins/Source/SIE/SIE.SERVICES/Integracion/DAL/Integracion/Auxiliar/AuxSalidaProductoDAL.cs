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

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxSalidaProductoDAL
    {
        /// <summary>
        /// obtiene los parametros para consultar la salida de productos por pagina
        /// </summary>
        /// <param name="pagina">paginacion</param>
        /// <param name="filtro">filtro</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaSalidaProducto(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                
                parametros = new Dictionary<string, object>
                                 {
                                     {"@FolioSalida", filtro.FolioSalida},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@DescripcionMovimiento",filtro.TipoMovimiento.Descripcion},
                                     {"@Activo", (int)EstatusEnum.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
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
        /// Obtiene los parametros necesarios para obtener la salida de producto por folio salida
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorFolioSalida(SalidaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@FolioSalida", filtro.FolioSalida},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Activo", (int)filtro.Activo}
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
        /// Obtiene los parametros necesarios para obtener la salida de producto por id
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorId(SalidaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@SalidaProductoID", filtro.FolioSalida},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Activo", (int)filtro.Activo}
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
        /// Obtiene los parametros necesarion para crear el primeri pesaje de la salida de producto
        /// </summary>
        /// <param name="salidaProducto">Datos de la salida de producto</param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarPrimerPesajeSalida(SalidaProductoInfo salidaProducto)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@TipoFolio", (int)TipoFolio.SalidaVentaTraspaso},
                    {"@OrganizacionID", salidaProducto.Organizacion.OrganizacionID},
                    {"@TipoMovimientoID", salidaProducto.TipoMovimiento.TipoMovimientoID},
                    {"@Cliente", salidaProducto.Cliente.ClienteID},
                    {"@Division", salidaProducto.OrganizacionDestino.OrganizacionID},
                    {"@PesoTara", salidaProducto.PesoTara},
                    {"@FechaSalida", salidaProducto.FechaSalida },
                    {"@ChoferID", salidaProducto.Chofer.ChoferID },
                    {"@CamionID", salidaProducto.Camion.CamionID },
                    {"@Activo", (int)EstatusEnum.Activo},
                    {"@UsuarioCreacionID", salidaProducto.UsuarioCreacionId}
                                     
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
        /// Obtiene los parametros necesarios para guardar el segundo pesaje de la salida de producto
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarSegundoPesajeSalida(SalidaProductoInfo salidaProducto)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@SalidaProductoID", salidaProducto.SalidaProductoId},
                    {"@CuentaSAPID", salidaProducto.CuentaSAP ==null ? 0 :salidaProducto.CuentaSAP.CuentaSAPID},
                    {"@PesoBruto", salidaProducto.PesoBruto},
                    {"@Precio", salidaProducto.Precio},
                    {"@UsuarioModificacionID", salidaProducto.UsuarioModificacionId}
                                     
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
        /// Obtiene los parametros necesarios para terminar la salida
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <param name="movimiento"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosTerminarSalida(SalidaProductoInfo salidaProducto,AlmacenMovimientoInfo movimiento)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                int facturar = salidaProducto.GeneraFactura ? 1 : 0;

                    parametros = new Dictionary<string, object>
                    {
                        {"@SalidaProductoID", salidaProducto.SalidaProductoId},
                        {"@AlmacenMovimientoID", movimiento.AlmacenMovimientoID},
                        {"@Activo", (int)EstatusEnum.Inactivo},
                        {"@UsuarioModificacionID", salidaProducto.UsuarioModificacionId},
                        {"@OrganizacionID", salidaProducto.Organizacion.OrganizacionID},
                        {"@RequiereFactura", facturar}
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
        /// Obtiene los parametros necesarios para actualizar el almacen, almacen inventario y piezas.
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizaAlmacenInventarioLote(SalidaProductoInfo salidaProducto)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@SalidaProductoID", salidaProducto.SalidaProductoId},
                    {"@AlmacenID",salidaProducto.Almacen.AlmacenID},
                    {"@AlmacenInventarioLoteID",salidaProducto.AlmacenInventarioLote.AlmacenInventarioLoteId},
                    {"@Piezas",salidaProducto.Piezas},
                    {"@Observaciones",salidaProducto.Observaciones}           
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
        /// Obtiene los parametros necesarios para buscar los folios activos con el peso tara capturado.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosTraspasoFoliosActivos(SalidaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@FolioSalida", filtro.FolioSalida},
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Activo", (int)filtro.Activo}
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
        /// Obtiene los parametros para ejecutar el sp para obtener los datos de la factura
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDatosFacturaPorFolioSalida(SalidaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                {
                    {"@FolioSalida", filtro.FolioSalida},
                    {"@OrganizacionID", filtro.Organizacion.OrganizacionID}
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
        /// del procedimiento almacenado SalidaProducto_ObtenerFolioPorPaginaReimpresion
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorPaginaReimpresion(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@DescripcionMovimiento", filtro.Descripcion},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite},
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
        /// del procedimiento almacenado SalidaProducto_ObtenerFolioReimpresion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorReimpresion(SalidaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@FolioSalida", filtro.FolioSalida},
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
        /// Obtiene los parametros necesarios
        /// para la ejecucion del procedimiento
        /// almacenado SalidaProducto_ObtenerConciliacionMovimientosSIAP
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPorAlmacenMovimientoXML(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in almacenesMovimiento
                                 select new XElement("AlmacenMovimiento",
                                                     new XElement("AlmacenMovimientoID", detalle.AlmacenMovimientoID)
                                     ));
                var parametros = new Dictionary<string, object> {{"@XmlAlmacenMovimiento", xml.ToString()}};
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros
        /// </summary>
        /// <param name="salidaProducto"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCancelar(SalidaProductoInfo salidaProducto)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();

                parametros = new Dictionary<string, object>
                                 {
                                     {"@SalidaProductoID", salidaProducto.SalidaProductoId},
                                     {"@UsuarioModificacionID", salidaProducto.UsuarioModificacionId},
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerFoliosPorPaginaParaCancelacion(PaginacionInfo pagina, SalidaProductoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from detalle in filtro.listaTipoMovimiento
                                 select new XElement("TipoMovimiento",
                                                     new XElement("TipoMovimientoID", detalle.TipoMovimientoID)
                                     ));
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                                     {"@Fecha",filtro.FechaSalida},
                                     {"@Descripcion",filtro.TipoMovimiento.Descripcion},
                                     {"@XMLTipoMovimiento",xml.ToString()},
                                     {"@Activo", (int)EstatusEnum.Activo},
                                     {"@Inicio", pagina.Inicio},
                                     {"@Limite", pagina.Limite}
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
