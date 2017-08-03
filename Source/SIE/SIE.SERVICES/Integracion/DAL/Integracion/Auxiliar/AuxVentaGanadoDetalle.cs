using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxVentaGanadoDetalle
    {
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorVentaGanadoID(int ventaGanadoID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@VentaGanadoID", ventaGanadoID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosGrabarDetallePropio(GrupoVentaGanadoInfo venta)
        {
            try
            {
                Logger.Info();

                var xml = new XElement("ROOT",
                        from animal in venta.VentaGandadoDetalle
                        select new XElement("GrabarVenta",
                                            new XElement("OrganizacionID", venta.OrganizacionId),
                                            new XElement("FolioTicket", venta.VentaGanado.FolioTicket),
                                            new XElement("CodigoCorral", venta.CodigoCorral),
                                            new XElement("UsuarioID", venta.VentaGanado.UsuarioModificacionID),
                                            new XElement("CausaPrecioID", animal.CausaPrecioID),
                                            new XElement("Arete", animal.Arete),
                                            new XElement("FotoVenta", animal.FotoVenta),
                                            new XElement("TipoVenta", venta.TipoVenta.GetHashCode()),
                                            new XElement("TotalCabezas", venta.TotalCabezas)
                            ));
    
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlVenta", xml.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosGrabarDetalleExterno(GrupoVentaGanadoInfo venta)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT",
                        new XElement("GrabarVenta",
                                            new XElement("OrganizacionID", venta.OrganizacionId),
                                            new XElement("FolioTicket", venta.VentaGanado.FolioTicket),
                                            new XElement("CodigoCorral", venta.CodigoCorral),
                                            new XElement("UsuarioID", venta.UsuarioID),
                                            new XElement("CausaPrecioID", venta.CausaPrecioID),
                                            new XElement("Arete", ""),
                                            new XElement("FotoVenta", ""),
                                            new XElement("TipoVenta", venta.TipoVenta.GetHashCode()),
                                            new XElement("TotalCabezas", venta.TotalCabezas)
                            ));
                
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XmlVenta", xml.ToString()}
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
        /// metodo para mapear como paametro el animalID
        /// </summary>
        /// <param name="animal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAnimalID(AnimalInfo animal)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@AnimalID", animal.AnimalID}
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
