using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using System.Xml.Linq;


namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxListaPreciosCentrosDAL
    {
        internal static Dictionary<string, object> ObtenerParametrosListaPreciosCentros(ListaPreciosCentrosInfo info)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@SociedadID", info.SociedadId},
                            {"@ZonaID", info.ZonaId}
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosListaPreciosCentrosGuardar(List<ListaPreciosCentrosInfo> precios, int usuarioId)
        {
            try
            {
                Logger.Info();
                var xml = new XElement("ROOT");
                foreach(var p in precios)
                {
                    // Macho Pesado
                    var node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                                        new XElement("TipoGanadoId", p.MachoPesadoId),
                                                        new XElement("Precio", p.MachoPesado),
                                                        new XElement("Peso", p.PesoPromedioMachoPesado)
                                                 );
                    xml.Add(node);
                    
                    // Torete
                    node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                        new XElement("TipoGanadoId", p.ToreteId),
                                        new XElement("Precio", p.Torete),
                                        new XElement("Peso", p.PesoPromedioTorete)
                                        );

                    xml.Add(node);

                    // Becerro Ligero
                    node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                    new XElement("TipoGanadoId", p.BecerroLigeroId),
                                    new XElement("Precio", p.BecerroLigero),
                                    new XElement("Peso", p.PesoPromedioBecerroLigero)
                             );
                    xml.Add(node);

                    // Becerro
                    node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                    new XElement("TipoGanadoId", p.BecerroId),
                                    new XElement("Precio", p.Becerro),
                                    new XElement("Peso", p.PesoPromedioBecerro)
                             );
                    xml.Add(node);

                    // Vaquilla Tipo2
                    node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                        new XElement("TipoGanadoId", p.VaquillaTipo2Id),
                                        new XElement("Precio", p.VaquillaTipo2),
                                        new XElement("Peso", p.PesoPromedioVaquillaTipo2)
                                        );

                    xml.Add(node);

                    // Hembra Pesada
                    node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                    new XElement("TipoGanadoId", p.HembraPesadaId),
                                    new XElement("Precio", p.HembraPesada),
                                    new XElement("Peso", p.PesoPromedioHembraPesada)
                             );
                    xml.Add(node);

                    // Vaquilla
                    node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                        new XElement("TipoGanadoId", p.VaquillaId),
                                        new XElement("Precio", p.Vaquilla),
                                        new XElement("Peso", p.PesoPromedioVaquilla)
                                        );

                    xml.Add(node);

                    // Becerra
                    node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                                        new XElement("TipoGanadoId", p.BecerraId),
                                                        new XElement("Precio", p.Becerra),
                                                        new XElement("Peso", p.PesoPromedioBecerra)
                                                 );
                    xml.Add(node);

                    // Becerra Ligera
                    node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                    new XElement("TipoGanadoId", p.BecerraLigeraId),
                                    new XElement("Precio", p.BecerraLigera),
                                    new XElement("Peso", p.PesoPromedioBecerraLigera)
                             );
                    xml.Add(node);

                    // ToretePesado
                    node = new XElement("Datos", new XElement("OrganizacionId", p.OrganizacionId),
                                    new XElement("TipoGanadoId", p.ToretePesadoId),
                                    new XElement("Precio", p.ToretePesado),
                                    new XElement("Peso", p.PesoPromedioToretePesado)
                             );
                    xml.Add(node);
                }

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@Precios", xml.ToString()},
                            {"@UsuarioId", usuarioId}
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