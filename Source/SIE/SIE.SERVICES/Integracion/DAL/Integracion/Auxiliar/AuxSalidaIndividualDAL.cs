using SIE.Base.Exepciones;
using SIE.Base.Log;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxSalidaIndividualDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosGrabar(string arete, int organizacion, string codigoCorral, int corraletaID, int usuarioCreacion, int tipoMovimiento)
        {
            try
            {
                Logger.Info();
                var xml =
                   new XElement("ROOT",
                                    new XElement("SalidaRecuperacionGrabar",
                                                 new XElement("Arete", arete),
                                                 new XElement("Organizacion", organizacion),
                                                 new XElement("CodigoCorral", codigoCorral),
                                                 new XElement("CodigoCorraletaID", corraletaID),
                                                 new XElement("UsuarioCreacionID", usuarioCreacion),
                                                 new XElement("TipoMovimiento", tipoMovimiento)
                                    )
                );
        
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlSalidaRecuperacion", xml.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerTicket(TicketInfo ticket)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", ticket.Organizacion},
                            {"@PesoTara", ticket.PesoTara},
                            {"@CodigoSAP", ticket.Cliente},
                            {"@TipoFolio", ticket.TipoFolio},
                            {"@Usuario", ticket.Usuario},
                            {"@TipoVenta", ticket.TipoVenta.GetHashCode().ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosGuardarSalidaIndividualGanado(SalidaIndividualInfo salidaIndividual)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioTicket", salidaIndividual.FolioTicket},
                            {"@PesoBruto", salidaIndividual.PesoBruto},
                            {"@Peso", salidaIndividual.Peso},
                            {"@NumeroDeCabezas", salidaIndividual.NumeroDeCabezas},
                            {"@OrganizacionID", salidaIndividual.Organizacion},
                            {"@UsuarioCreacionID", salidaIndividual.Usuario},
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
        /// Obtiene los parametros para obtener los datos de la factura para generarla
        /// </summary>
        /// <param name="folioTicket"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDatosFacturaVentaDeGanado(int folioTicket, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioTicket", folioTicket},
                            {"@OrganizacionID", organizacionID}
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
        /// Obtiene los parametros necesarios para la ejecucion
        /// del procedimiento almacenado SalidaIndividualVenta_GuardarHistoricos
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosGuardarHistoricos(SalidaIndividualInfo salidaIndividual)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioTicket", salidaIndividual.FolioTicket},
                            {"@OrganizacionID", salidaIndividual.Organizacion},
                            {"@UsuarioCreacionID", salidaIndividual.Usuario},
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
