using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxLoteSacrificioDAL
    {
        /// <summary>
        /// Obtiene los parametros para consultar el lote sacrificio
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerLoteSacrificio(DateTime fecha, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Fecha", fecha},
                            {"@OrganizacionID", organizacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Obtiene los parametros para actualizar el lote sacrificio
        /// </summary>
        /// <param name="loteSacrificioInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroActualizarLoteSacrificio(LoteSacrificioInfo loteSacrificioInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrdenSacrificioId", loteSacrificioInfo.OrdenSacrificioId},
                            {"@OrganizacionId", loteSacrificioInfo.OrganizacionId},
                            {"@ClienteId", loteSacrificioInfo.Cliente.ClienteID},
                            {"@Observaciones", loteSacrificioInfo.Observaciones},
                            {"@UsuarioModificacionId", loteSacrificioInfo.UsuarioModificacionId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        internal static Dictionary<string, object> ObtenerParametroActualizarLoteSacrificioLucero(LoteSacrificioInfo loteSacrificioInfo)
        {
            try
            {
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionId", loteSacrificioInfo.OrganizacionId},
                            {"@ClienteId", loteSacrificioInfo.Cliente.ClienteID},
                            {"@Observaciones", loteSacrificioInfo.Observaciones},
                            {"@UsuarioModificacionId", loteSacrificioInfo.UsuarioModificacionId},
                            {"@Fecha", loteSacrificioInfo.Fecha},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros para obtener las facturas de la orden de sacrificio a cancelar
        /// </summary>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerFacturasPorOrdenSacrificioACancelar(int ordenSacrificioId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrdenSacrificioId", ordenSacrificioId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Obtiene los parametros para obtener los datos de la factura por orden sacrificio
        /// </summary>
        /// <param name="loteSacrificioInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDatosFacturaPorOrdenSacrificio(LoteSacrificioInfo loteSacrificioInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrdenSacrificioId", loteSacrificioInfo.OrdenSacrificioId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion del procedimiento
        /// almacenado LoteSacrificio_ObtenerDatosPoliza
        /// </summary>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosPolizaSacrificio(int ordenSacrificioId)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrdenSacrificioID", ordenSacrificioId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para ejecutar
        /// el procedimiento que regresa los datos de
        /// facturacion de lucero
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <param name="foliosLoteSacrificio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosPolizaSacrificioLucero(int organizacionID, DateTime fecha, List<PolizaSacrificioModel> foliosLoteSacrificio)
        {
            try
            {
                Logger.Info();

                var xml = new XElement("ROOT",
                                       from detalle in foliosLoteSacrificio
                                       select new XElement("InterfaceDetalleID",
                                                           new XElement("Id", detalle.InterfaceSalidaTraspasoDetalleID)
                                           ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@Fecha", fecha},
                            {"@XmlInterface", xml.ToString()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para ejecutar
        /// el procedimiento que regresa los datos de
        /// facturacion de lucero
        /// </summary>
        internal static Dictionary<string, object> ObtenerParametrosDatosPolizaSacrificioLucero(int organizacionID, DateTime fecha)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                            {"@Fecha", fecha},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para ejecutar
        /// el procedimiento que regresa los datos de
        /// facturacion de lucero
        /// </summary>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDatosFacturaPorOrdenSacrificioLucero(LoteSacrificioInfo loteSacrificioInfo, List<PolizaSacrificioModel> lotesSacrificioFolios)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                                       from detalle in lotesSacrificioFolios
                                       select new XElement("InterfaceDetalleID",
                                                           new XElement("Id", detalle.InterfaceSalidaTraspasoDetalleID)
                                           ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionId", loteSacrificioInfo.OrganizacionId},
                            {"@XmlInterface", xml.ToString()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para ejecutar
        /// el procedimiento que regresa los datos de
        /// facturacion de lucero
        /// </summary>
        internal static Dictionary<string, object> ObtenerParametrosSacrificiosLucero(List<PolizaSacrificioModel> lotesSacrificioFolios)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                                       from detalle in lotesSacrificioFolios.Select(id => id.InterfaceSalidaTraspasoDetalleID).Distinct()
                                       select new XElement("InterfaceDetalleID",
                                                           new XElement("Id", detalle)
                                           ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlInterface", xml.ToString()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios
        /// para la ejecucion del procedimiento
        /// almacenado LoteSacrificio_ObtenerDetalleFacturasACancelarLucero
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroObtenerFacturasPorOrdenSacrificioACancelarLucero(int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", organizacionID},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la
        /// ejecucion del procedimiento almancenado
        /// LoteSacrificioLucero_ActualizarImportes
        /// </summary>
        /// <param name="lotesSacrificio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroActualizarImportesSacrificio(List<PolizaSacrificioModel> lotesSacrificio)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                                       from detalle in lotesSacrificio
                                       select new XElement("InterfaceDetalleID",
                                                           new XElement("Id", detalle.InterfaceSalidaTraspasoDetalleID),
                                                           new XElement("ImporteCanal", detalle.ImporteCanal),
                                                           new XElement("ImportePiel", detalle.ImportePiel),
                                                           new XElement("ImporteViscera", detalle.ImporteViscera),
                                                           new XElement("Corral", detalle.Corral)
                                           ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlInterface", xml.ToString()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la 
        /// ejecucion del procedimiento almacenado
        /// LoteSacrificioLucero_ObtenerDatosConciliacion
        /// </summary>
        /// <param name="interfaceSalidaTraspasos"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosConciliacionSacrificioTraspaso(List<InterfaceSalidaTraspasoInfo> interfaceSalidaTraspasos)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                                       from detalle in interfaceSalidaTraspasos
                                       select new XElement("InterfaceDetalleID",
                                                           new XElement("OrganizacionID", detalle.OrganizacionId),
                                                           new XElement("Id",
                                                                        detalle.ListaInterfaceSalidaTraspasoDetalle.
                                                                            Select(
                                                                                id =>
                                                                                id.InterfaceSalidaTraspasoDetalleID).
                                                                            FirstOrDefault())
                                           ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlInterface", xml.ToString()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la actualizacion
        /// de la bandera de PolizaGenerada
        /// </summary>
        /// <param name="lotesSacrificio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametroActualizarPolizaGenerada(List<PolizaSacrificioModel> lotesSacrificio)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                                       from detalle in lotesSacrificio
                                       select new XElement("DatosSacrificio",
                                                           new XElement("LoteID", detalle.LoteID),
                                                           new XElement("Fecha", detalle.Fecha),
                                                           new XElement("OrganizacionID", detalle.OrganizacionID),
                                                           new XElement("InterfaceSalidaTraspasoDetalleID",
                                                                        detalle.InterfaceSalidaTraspasoDetalleID),
                                                           new XElement("PolizaGenerada", detalle.PolizaGenerada)
                                           ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlSacrificio", xml.ToString()},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }
    }
}
