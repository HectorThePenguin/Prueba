using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxDiferenciasDeInventarioDAL
    {
        /// <summary>
        /// Obtiene parametros
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDiferenciasInventario(List<EstatusInfo> listaEstatusInfo, List<TipoMovimientoInfo> listaTiposMovimiento, UsuarioInfo usuarioInfo)
        {
            try
            {
                Logger.Info();
                var xmlEstatus =
                    new XElement("ROOT",
                                 from detalle in listaEstatusInfo
                                 select new XElement("Datos",
                                        new XElement("EstatusID", detalle.EstatusId)
                                     ));
                var xmlTipoMovimiento =
                    new XElement("ROOT",
                                 from detalle in listaTiposMovimiento
                                 select new XElement("Datos",
                                        new XElement("TipoMovimientoID", detalle.TipoMovimientoID)
                                     ));
                var parametros = new Dictionary<string, object>
                {
                    {"@XmlEstatus", xmlEstatus.ToString()},
                    {"@XmlTipoMovimiento", xmlTipoMovimiento.ToString()},
                    {"@UsuarioCreacionID", usuarioInfo.UsuarioCreacionID}
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
        /// Obtener parametros de autorizacion de materia prima
        /// </summary>
        /// <param name="autorizacionMateriaPrimaInfo"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ObtenerParametrosAutorizacionMateriaPrima(AutorizacionMateriaPrimaInfo autorizacionMateriaPrimaInfo)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                                {"@OrganizacionID",autorizacionMateriaPrimaInfo.OrganizacionID},
                                {"@TipoAutorizacionID",autorizacionMateriaPrimaInfo.TipoAutorizacionID},
                                {"@Folio",autorizacionMateriaPrimaInfo.Folio},
                                {"@Justificacion",autorizacionMateriaPrimaInfo.Justificacion},
                                {"@Lote",autorizacionMateriaPrimaInfo.Lote},
                                {"@Precio",autorizacionMateriaPrimaInfo.Precio},
                                {"@Cantidad",autorizacionMateriaPrimaInfo.Cantidad},
                                {"@ProductoID",autorizacionMateriaPrimaInfo.ProductoID},
                                {"@AlmacenID",autorizacionMateriaPrimaInfo.AlmacenID},
                                {"@EstatusID",autorizacionMateriaPrimaInfo.EstatusID},
                                {"@UsuarioCreacion",autorizacionMateriaPrimaInfo.UsuarioCreacion},
                                {"@Activo",autorizacionMateriaPrimaInfo.Activo}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        public static Dictionary<string, object> ObtenerParametrosGuardarProgramacionMateria(ProgramacionMateriaPrimaInfo programacionMateriaPrimaInfo, int autorizacionMateriaPrimaID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
                               {"@PedidoDetalleID", programacionMateriaPrimaInfo.PedidoDetalleId},
                               {"@OrganizacionID", programacionMateriaPrimaInfo.Organizacion.OrganizacionID},
                               {"@AlmacenID", programacionMateriaPrimaInfo.Almacen.AlmacenID},
                               {"@AlmacenInventarioLoteID", programacionMateriaPrimaInfo.InventarioLoteOrigen.AlmacenInventarioLoteId},
                               {"@CantidadProgramada", programacionMateriaPrimaInfo.CantidadProgramada},
                               {"@UsuarioCreacionID", programacionMateriaPrimaInfo.UsuarioCreacion.UsuarioID},
                               {"@Observaciones",programacionMateriaPrimaInfo.Observaciones},
                               {"@AutorizacionMateriaPrimaID",autorizacionMateriaPrimaID}
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
