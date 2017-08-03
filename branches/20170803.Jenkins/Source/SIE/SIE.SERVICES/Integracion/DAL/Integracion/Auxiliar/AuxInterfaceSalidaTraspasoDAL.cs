using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxInterfaceSalidaTraspasoDAL
    {
        /// <summary>
        /// Obtiene los parametros para insertar un nuevo registro en la tabla InterfaceSalidaTraspaso
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionID", interfaceSalidaTraspaso.OrganizacionId},
                    {"@OrganizacionIDDestino", interfaceSalidaTraspaso.OrganizacionDestino.OrganizacionID},
                    {"@FolioTraspaso", interfaceSalidaTraspaso.FolioTraspaso},
                    {"@CabezasEnvio", interfaceSalidaTraspaso.CabezasEnvio},
                    {"@TraspasoGanado", interfaceSalidaTraspaso.TraspasoGanado},
                    {"@SacrifioGanado", interfaceSalidaTraspaso.SacrificioGanado},
                    {"@PesoTara", interfaceSalidaTraspaso.PesoTara},
                    {"@PesoBruto", interfaceSalidaTraspaso.PesoBruto},
                    {"@Activo", interfaceSalidaTraspaso.Activo.GetHashCode()},
                    {"@UsuarioCreacionID", interfaceSalidaTraspaso.UsuarioCreacionID}
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
        /// Obtiene los parametros para insertar un nuevo registro en la tabla InterfaceSalidaTraspaso
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizar(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@InterfaceSalidaTraspasoID", interfaceSalidaTraspaso.InterfaceSalidaTraspasoId},
                    {"@CabezasEnvio", interfaceSalidaTraspaso.CabezasEnvio},
                    {"@PesoTara", interfaceSalidaTraspaso.PesoTara},
                    {"@PesoBruto", interfaceSalidaTraspaso.PesoBruto},
                    {"@UsuarioModificacionID", interfaceSalidaTraspaso.UsuarioModificacionID}
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
        ///  Obtiene los parametros para insertar una lista en la tabla InterfaceSalidaTraspaso
        /// </summary>
        /// <param name="listaInterfaceSalidaTraspasoDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrearListado(List<InterfaceSalidaTraspasoDetalleInfo> listaInterfaceSalidaTraspasoDetalle)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros;
                var xml =
                    new XElement("ROOT",
                                 from detalle in listaInterfaceSalidaTraspasoDetalle
                                 select new XElement("InterfaceSalidaTraspasoDetalle",
                                        new XElement("InterfaceSalidaTraspasoID", detalle.InterfaceSalidaTraspasoID),
                                        new XElement("LoteID", detalle.Lote.LoteID),
                                        new XElement("TipoGanadoID", detalle.TipoGanado.TipoGanadoID),
                                        new XElement("PesoProyectado", detalle.PesoProyectado),
                                        new XElement("GananciaDiaria", detalle.GananciaDiaria),
                                        new XElement("TipoGanadoID", detalle.TipoGanado.TipoGanadoID),
                                        new XElement("DiasEngorda", detalle.DiasEngorda),
                                        new XElement("FormulaID", detalle.Formula.FormulaId),
                                        new XElement("DiasFormula", detalle.DiasFormula),
                                        new XElement("Cabezas", detalle.Cabezas),
                                        new XElement("Activo", detalle.Activo.GetHashCode()),
                                        new XElement("UsuarioCreacionID", detalle.UsuarioCreacionID)
                                     ));
                parametros = new Dictionary<string, object>
                    {
                        {"@InterfaceSalidaDetalleXML", xml.ToString()}
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
        /// Obtiene los parametros para insertar un nuevo registro en la tabla InterfaceSalidaTraspaso
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosInterfaceSalidaTraspasoPorFolioOrganizacion(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionID", interfaceSalidaTraspaso.OrganizacionId},
                    {"@FolioTraspaso", interfaceSalidaTraspaso.FolioTraspaso}
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
        /// Obtiene los parametros necesarios para la ejecucion del proceso
        /// almacenado InterfaceSalidaTraspaso_ObtenerPorFolioTraspaso
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="folioOrigen"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosInterfaceSalidaTraspaso(int organizacionID, int folioOrigen)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                                     {
                                         {"@OrganizacionID", organizacionID},
                                         {"@FolioTraspaso", folioOrigen}
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
        /// </summary>
        /// <param name="loteInfo"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosInterfaceSalidaTraspasoPorLote(LoteInfo loteInfo)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionID", loteInfo.OrganizacionID},
                    {"@LoteID", loteInfo.LoteID}
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
        /// Obtiene los parametros necesarios para la
        /// ejecucion del procedimiento almacenado
        /// InterfaceSalidaTraspaso_ObtenerPorLoteXML
        /// </summary>
        /// <param name="lotes"></param>
        /// <param name="fecha"> </param>
        /// <param name="foliosTraspaso"> </param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosInterfaceSalidaTraspasoPorLotes(List<LoteInfo> lotes, DateTime fecha, List<PolizaSacrificioModel> foliosTraspaso)
        {
            try
            {
                Logger.Info();
                var xml =
                    new XElement("ROOT",
                                 from lote in lotes
                                 select new XElement("Lotes",
                                                     new XElement("LoteID", lote.LoteID)
                                     ));
                var xmlFolios =
                    new XElement("ROOT",
                                 from folio in foliosTraspaso
                                 select new XElement("InterfaceDetalleID",
                                                     new XElement("Id", folio.InterfaceSalidaTraspasoDetalleID)
                                     ));
                var parametros = new Dictionary<string, object>
                                     {
                                         {"@LotesXML", xml.ToString()},
                                         {"@Fecha", fecha},
                                         {"@XmlInterface", xmlFolios.ToString()}
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
        /// Obtiene los parametros necesarios
        /// para la ejecucion del procedimiento
        /// almacenado InterfaceSalidaTraspaso_ObtenerPorLote
        /// </summary>
        /// <param name="folioTraspaso"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosInterfaceSalidaTraspaso(PolizaSacrificioModel folioTraspaso)
        {
            try
            {
                Logger.Info();
                var parametros = new Dictionary<string, object>
                {
                    {"@InterfaceDetalleID", folioTraspaso.InterfaceSalidaTraspasoDetalleID},
                    {"@LoteID", folioTraspaso.LoteID}
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
