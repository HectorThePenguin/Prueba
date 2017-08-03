using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using System.Xml.Linq;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxSolicitudAutorizacionDAL
    {
        /// Se verifica si ya se cuenta con una solicitud
        internal static Dictionary<string, object> ObtenerDatosSolicitudAutorizacion(SolicitudAutorizacionInfo solicitudInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioSalida", solicitudInfo.FolioSalida},
                            {"@OrganizacionID", solicitudInfo.OrganizacionID},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// Se valida si ya se genero una solicitud para el mismo precio venta
        internal static Dictionary<string, object> ConsultarPrecioRechazadoSolicitud(int folioSalida, decimal precioVenta, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioSalida", folioSalida},
                            {"@PrecioVenta", precioVenta},
                            {"@OrganizacionID", organizacionID},
                            {"@EstatusRechazadoID", Estatus.AMPRechaza},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }


        /// Se genera la solicitud de auorizacion para el precio venta menos capturado
        internal static Dictionary<string, object> GenerarSolicitudAutorizacion(SolicitudAutorizacionInfo solicitudInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", solicitudInfo.OrganizacionID},
                            {"@FolioSalida", solicitudInfo.FolioSalida},
                            {"@Justificacion", solicitudInfo.Justificacion},
                            {"@Precio", solicitudInfo.Precio},
                            {"@ProductoID", solicitudInfo.ProductoID},
                            {"@AlmacenID", solicitudInfo.AlmacenID},
                            {"@UsuarioCreacionID", solicitudInfo.UsuarioCreacionID},
                            {"@TipoAutorizacionID", (int)TipoAutorizacionEnum.PrecioVenta},
                            {"@EstatusID", (int)Estatus.AMPPendien},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtener parametros para solicitud autorizacion de programacion de MP
        /// </summary>
        /// <param name="autorizacionInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDatosSolicitudAutorizacionProgramacionMP(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", autorizacionInfo.OrganizacionID},
                            {"@TipoAutorizacionID", autorizacionInfo.TipoAutorizacionID},
                            {"@Folio", autorizacionInfo.Folio},
                            {"@Lote",autorizacionInfo.Lote},
                            {"@Activo",EstatusEnum.Activo}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtener parametros para obtener solicitud autorizada
        /// </summary>
        /// <param name="autorizacionInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDatosSolicitudAutorizada(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", autorizacionInfo.OrganizacionID},
                            {"@TipoAutorizacionID", autorizacionInfo.TipoAutorizacionID},
                            {"@Folio", autorizacionInfo.Folio},
                            {"@EstatusID",autorizacionInfo.EstatusID},
                            {"@Activo",EstatusEnum.Activo}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// Se obtiene la lista de solicitudes con estatus pendiente
        internal static Dictionary<string, object> ObtenerSolicitudesPendientes(int organizacionID, int tipoAutorizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@TipoAutorizacionID", tipoAutorizacionID},
                            {"@EstatusID", (int)Estatus.AMPPendien},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// Se obtiene la lista de solicitudes con estatus pendiente
        internal static Dictionary<string, object> ObtenerSolicitudesAjusteInventarioPendientes(int organizacionID, int tipoAutorizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@TipoAutorizacionID", tipoAutorizacionID},
                            {"@DifInvPendiente", (int)Estatus.DifInvPendiente},
                            {"@EntradaID", (int)TipoMovimiento.EntradaPorAjuste},
                            {"@SalidaID", (int)TipoMovimiento.SalidaPorAjuste},
                            {"@EstatusID", (int)Estatus.AMPPendien},
                            {"@Activo", EstatusEnum.Activo.GetHashCode()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// Se obtiene la lista de solicitudes con estatus pendiente
        internal static Dictionary<string, object> GuardarRespuestasSolicitudes(List<AutorizacionMovimientosInfo> respuestaSolicitudes, int organizacionID, int tipoAutorizacionID, int usuarioID)
        {
            try
            {
                Logger.Info();
                var xml =
                new XElement("ROOT",
               from datos in respuestaSolicitudes
               select
                   new XElement("AutorizacionMovimientos",
                                new XElement("AutorizacionID", datos.AutorizacionID),
                                new XElement("EstatusID", datos.EstatusID),
                                new XElement("Observaciones", datos.Observaciones)
                                )
                   );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlSolicitudes", xml.ToString()},
                            {"@OrganizacionID", organizacionID},
                            {"@TipoAutorizacionID", tipoAutorizacionID},
                            {"@UsuarioModificaID", usuarioID},
                            {"@EstatusPendienteID", (int)Estatus.AMPPendien}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// Se obtiene la lista de solicitudes con estatus pendiente
        internal static Dictionary<string, object> GuardarAjusteInventario(List<AutorizacionMovimientosInfo> respuestaSolicitudes, int organizacionID)
        {
            try
            {
                Logger.Info();
                var xml =
                new XElement("ROOT",
               from datos in respuestaSolicitudes
               select
                   new XElement("AutorizacionMovimientos",
                                new XElement("AlmacenMovimientoID", datos.AlmacenMovimientoID),
                                new XElement("EstatusInventarioID", datos.EstatusInventarioID)
                                )
                   );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlSolicitudes", xml.ToString()},
                            {"@DifInvAutorizadoID", (int)Estatus.DifInvAutorizado}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// Se obtiene la lista de solicitudes con estatus pendiente
        internal static Dictionary<string, object> ObtenerParametrosMovimientosAutorizacion(List<AutorizacionMovimientosInfo> respuestaSolicitudes)
        {
            try
            {
                Logger.Info();
                var xml =
                new XElement("ROOT",
               from datos in respuestaSolicitudes
               select
                   new XElement("AutorizacionMovimientos",
                                new XElement("AlmacenMovimientoID", datos.AlmacenMovimientoID),
                                new XElement("EstatusInventarioID", datos.EstatusInventarioID)
                                )
                   );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlSolicitudes", xml.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtener parametros para solicitud autorizacion de programacion de MP
        /// </summary>
        /// <param name="autorizacionInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerSolicitudAutorizacion(AutorizacionMateriaPrimaInfo autorizacionInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", autorizacionInfo.OrganizacionID},
                            {"@TipoAutorizacionID", autorizacionInfo.TipoAutorizacionID},
                            {"@Folio", autorizacionInfo.Folio},
                            {"@Activo", EstatusEnum.Activo}
                        };
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
