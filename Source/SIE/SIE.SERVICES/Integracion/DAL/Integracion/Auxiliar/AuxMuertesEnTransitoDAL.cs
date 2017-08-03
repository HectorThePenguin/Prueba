using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Base.Infos;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxMuertesEnTransitoDAL
    {

        internal static Dictionary<string, object> ObtenerParametrosGuardar(List<EntradaGanadoMuerteInfo> entradaGanadoMuerteLista, ClienteInfo cliente)
        {
            try
            {
                Logger.Info();
                var xmlEntradas =
                 new XElement("ROOT",
                              from entrada in entradaGanadoMuerteLista
                              select new XElement("EntradaGanadoMuerte",
                                     new XElement("EntradaGanadoID", entrada.EntradaGanado.EntradaGanadoID),
                                     new XElement("Arete", entrada.Animal.Arete),
                                     new XElement("FolioMuerte", entrada.FolioMuerte),
                                     new XElement("Activo",  entrada.Activo.GetHashCode()),
                                     new XElement("Peso", entrada.Peso),
                                     new XElement("UsuarioCreacionID", entrada.UsuarioCreacionID),
                                     new XElement("ClienteID", cliente.ClienteID),
                                     new XElement("AnimalID", entrada.Animal.AnimalID)
                              ));
                var detalles = new List<EntradaGanadoMuerteDetalleInfo>();
                entradaGanadoMuerteLista.ForEach(det => detalles.AddRange(det.EntradaGanadMuerteDetalle));
                var xmlDetalle =
                 new XElement("ROOT",
                              from det in detalles
                              select new XElement("EntradaGanadoMuerteDetalle",
                                     new XElement("EntradaGanadoID", det.EntradaGanadoMuerte.EntradaGanado.EntradaGanadoID),
                                     new XElement("CostoID", det.Costo.CostoID),
                                     new XElement("Importe", det.Importe),
                                     new XElement("UsuarioCreacionID", det.UsuarioCreacionID),
                                     new XElement("Arete", det.EntradaGanadoMuerte.Animal.Arete)
                              ));
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@EntradaGanadoMuerteXML", xmlEntradas.ToString()},
                            {"@EntradaGanadoMuerteDetalleXML", xmlDetalle.ToString()}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosPorPagina(PaginacionInfo pagina, MuertesEnTransitoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioEntrada", filtro.Origen}, //Se utiliza para mostrar el campo en el grid y filtrar con el.
                            {"@OrganizacionID", filtro.OrganizacionID},
                            {"@Inicio", pagina.Inicio},
                            {"@Limite", pagina.Limite}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosPorFolioEntrada(MuertesEnTransitoInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioEntrada", filtro.FolioEntrada},
                            {"@OrganizacionID", filtro.OrganizacionID}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object>ObtenerParametrosObtenerDatosFacturaMuertesEnTransito(long folioMuerte, int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioMuerte", folioMuerte},
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


        internal static Dictionary<string, object> ObtenerParametrosObtenerTotalFoliosValidos(int organizacionID)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
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

        internal static Dictionary<string, object> ObtenerParametrosValidarFolioEntrada(int folioEntrada, int organizacionID, List<string> aretes)
        {
            try
            {
                Logger.Info();


                var xmlAretes =
                 new XElement("ROOT",
                              from arete in aretes
                              select new XElement("Aretes", new XElement("Arete", new XElement("ID", arete))
                              ));


                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@FolioEntrada", folioEntrada},
                            {"@OrganizacionID", organizacionID},
                            {"@AretesXML", xmlAretes.ToString()}
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
