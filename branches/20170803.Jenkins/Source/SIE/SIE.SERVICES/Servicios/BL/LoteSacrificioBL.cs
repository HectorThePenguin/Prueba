using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Services.Polizas.Fabrica;
using SIE.Services.ServicioPolizasCancelacion;
using SIE.Services.ServicioPolizasLogin;

namespace SIE.Services.Servicios.BL
{
    internal class LoteSacrificioBL
    {
        /// <summary>
        /// Obtiene los datos del lote sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal LoteSacrificioInfo ObtenerLoteSacrificio(DateTime fecha, int organizacionID)
        {
            LoteSacrificioInfo result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerLoteSacrificio(fecha, organizacionID);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Obtiene los datos del lote sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="organizacionID"> </param>
        /// <returns></returns>
        internal LoteSacrificioInfo ObtenerLoteSacrificioLucero(DateTime fecha, int organizacionID)
        {
            LoteSacrificioInfo result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerLoteSacrificioLucero(fecha, organizacionID);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Actualiza el lote sacrificio seleccionado
        /// </summary>
        /// <param name="ganadoPropio"></param>
        /// <param name="loteSacrificioInfo"></param>
        internal void ActualizarLoteSacrificio(LoteSacrificioInfo loteSacrificioInfo, bool ganadoPropio)
        {
            try
            {
                Logger.Info();
                var lotesSacrificioFolios = new List<PolizaSacrificioModel>();
                //var ganaderaTraspasaGanado = false;
                using (var scope = new TransactionScope())
                {
                    //var parametroGeneralBL = new ParametroGeneralBL();
                    //ParametroGeneralInfo parametroGanaderaTraspasa =
                    //    parametroGeneralBL.ObtenerPorClaveParametro(ParametrosEnum.GANADERATRASPASAGANADO.ToString());
                    //if (parametroGanaderaTraspasa != null)
                    //{
                    //    ganaderaTraspasaGanado =
                    //        parametroGanaderaTraspasa.Valor.Split('|').ToList().Any(
                    //            dato => Convert.ToInt32(dato) == loteSacrificioInfo.OrganizacionId);
                    //}
                    if (!ganadoPropio)
                    {
                        lotesSacrificioFolios = ObtenerDatosSacrificioLucero(loteSacrificioInfo.OrganizacionId,
                                                                             loteSacrificioInfo.Fecha);
                    }
                    var loteSacrificioDal = new LoteSacrificioDAL();
                    if (!ganadoPropio)
                    {
                        loteSacrificioDal.ActualizarLoteSacrificioLucero(loteSacrificioInfo);
                    }
                    else
                    {
                        loteSacrificioDal.ActualizarLoteSacrificio(loteSacrificioInfo);
                    }

                    if (loteSacrificioInfo.Cliente.ClienteID != 0)
                    {
                        // Genera el xml y lo guarda en la ruta especificada en la configuración
                        var facturaBl = new FacturaBL();
                        // funcion no regresa valor pero entra al catch de ExcepcionServicio si alguna validacion no la cumple.
                        facturaBl.ValidarDatosDeConfiguracion(loteSacrificioInfo.OrganizacionId);
                        if (ganadoPropio)
                        {
                            facturaBl.GenerarDatosFacturaSacrificio(loteSacrificioInfo);
                        }
                    }

                    scope.Complete();
                }
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene el lote sacrificio a cancelar la factura
        /// </summary>
        /// <returns></returns>
        internal LoteSacrificioInfo ObtenerLoteSacrificioACancelar(int organizacionID)
        {
            LoteSacrificioInfo result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerLoteSacrificioACancelar(organizacionID);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }
        /// <summary>
        /// Obtiene las facturas generadas a los lotes de sacrificio
        /// </summary>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        internal List<LoteSacrificioInfo> ObtenerFacturasPorOrdenSacrificioACancelar(int ordenSacrificioId)
        {
            List<LoteSacrificioInfo> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerFacturasPorOrdenSacrificioACancelar(ordenSacrificioId);
                result = result.GroupBy(x => new { x.FolioFactura })
                    .Select(dato => new LoteSacrificioInfo
                    {
                        FolioFactura = dato.Select(org => org.FolioFactura).FirstOrDefault()
                    }).ToList();
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Obtiene los datos de facturacion para generar la polizas
        /// </summary>
        /// <param name="ordenSacrificioId"></param>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerDatosFacturaSacrificio(int ordenSacrificioId)
        {
            List<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerDatosFacturaSacrificio(ordenSacrificioId);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Obtiene los datos de facturacion para generar la polizas
        /// </summary>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerDatosFacturaSacrificioLucero(int organizacionID, DateTime fecha
                                                                               , List<PolizaSacrificioModel> foliosLoteSacrificio)
        {
            List<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerDatosFacturaSacrificioLucero(organizacionID, fecha,
                                                                               foliosLoteSacrificio);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Obtiene los datos de facturacion para generar la polizas
        /// </summary>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerDatosSacrificioLucero(int organizacionID, DateTime fecha)
        {
            List<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerDatosSacrificioLucero(organizacionID, fecha);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Obtiene una lista con
        /// los sacrificios generados
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal IEnumerable<PolizaSacrificioModel> ObtenerPolizasSacrificioConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            IEnumerable<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerPolizasSacrificioConciliacion(organizacionID, fechaInicial, fechaFinal);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Se obtiene los folios a cancelar
        /// </summary>
        /// <param name="lotesSacrificio"></param>
        /// <returns></returns>
        private List<string> ObtenerFoliosCancelar(List<PolizaSacrificioModel> lotesSacrificio)
        {
            return lotesSacrificio.Select(folio => string.Format("{0}-{1} ", folio.Serie, folio.Folio)).ToList();
        }

        /// <summary>
        /// Obtiene los documentos de SAP
        /// </summary>
        /// <param name="foliosACancelar"></param>
        /// <param name="polizasACancelar"></param>
        /// <returns></returns>
        private List<PolizaInfo> ObtenerDocumentosSAP(List<string> foliosACancelar, IList<PolizaInfo> polizasACancelar)
        {
            var polizas = new List<PolizaInfo>();
            for (var indexFolios = 0; indexFolios < foliosACancelar.Count; indexFolios++)
            {
                polizas.AddRange(
                    polizasACancelar.Where(concepto => concepto.Concepto.Contains(foliosACancelar[indexFolios])));
            }
            return polizas;
        }

        /// <summary>
        /// Complementa el concepto de poliza con los folios de traspaso
        /// </summary>
        /// <param name="polizasSacrificioSukarne"></param>
        /// <param name="lotesSacrificioFolios"></param>
        /// <param name="interfaceSalidaTraspaso"> </param>
        private void ComplementarConceptoPoliza(IList<PolizaInfo> polizasSacrificioSukarne
                                              , List<PolizaSacrificioModel> lotesSacrificioFolios
                                              , List<InterfaceSalidaTraspasoInfo> interfaceSalidaTraspaso)
        {
            PolizaSacrificioModel polizaSacrificioModel;
            List<PolizaInfo> polizas;

            var detalleTraspaso = new List<InterfaceSalidaTraspasoDetalleInfo>();

            interfaceSalidaTraspaso.ForEach(
                detalle => detalleTraspaso.AddRange(detalle.ListaInterfaceSalidaTraspasoDetalle));

            InterfaceSalidaTraspasoDetalleInfo interfaceSalidaTraspasoDetalle;
            InterfaceSalidaTraspasoInfo interfaceSalida;
            for (var indexLotes = 0; indexLotes < lotesSacrificioFolios.Count; indexLotes++)
            {
                polizaSacrificioModel = lotesSacrificioFolios[indexLotes];
                polizas =
                    polizasSacrificioSukarne.Where(
                        concepto =>
                        concepto.Concepto
                            .IndexOf(
                                string.Format("{0}-{1}", polizaSacrificioModel.Serie, polizaSacrificioModel.Folio),
                                StringComparison.Ordinal) >= 0).ToList();
                if (polizas.Any())
                {
                    interfaceSalidaTraspasoDetalle = detalleTraspaso.FirstOrDefault(
                        det =>
                        det.InterfaceSalidaTraspasoDetalleID == polizaSacrificioModel.InterfaceSalidaTraspasoDetalleID);
                    if (interfaceSalidaTraspasoDetalle == null)
                    {
                        interfaceSalidaTraspasoDetalle = new InterfaceSalidaTraspasoDetalleInfo();
                    }
                    interfaceSalida = interfaceSalidaTraspaso.FirstOrDefault(
                        id =>
                        id.InterfaceSalidaTraspasoId ==
                        interfaceSalidaTraspasoDetalle.InterfaceSalidaTraspasoID);
                    if (interfaceSalida == null)
                    {
                        interfaceSalida = new InterfaceSalidaTraspasoInfo();
                    }
                    polizas.ForEach(
                        con =>
                        con.Concepto =
                        string.Format("{0} ST-{1}", con.Concepto, interfaceSalida.FolioTraspaso));
                }
            }
        }

        /// <summary>
        /// Obtiene los datos en caso de que se sacrifico con anterioridad
        /// de ese lote de manera incompleta
        /// </summary>
        /// <param name="lotesSacrificioFolios"></param>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerSacrificiosAnteriores(List<PolizaSacrificioModel> lotesSacrificioFolios)
        {
            List<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerSacrificiosAnteriores(lotesSacrificioFolios);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Valida las cabezas ya sacrificadas
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <param name="lotesSacrificioRegistrados"></param>
        private void ValidaCabezasSacrificadas(List<InterfaceSalidaTraspasoInfo> interfaceSalidaTraspaso, List<PolizaSacrificioModel> lotesSacrificioRegistrados)
        {
            interfaceSalidaTraspaso.ForEach(datos => datos.ListaInterfaceSalidaTraspasoDetalle
                                                         .ForEach(det =>
                                                                      {
                                                                          PolizaSacrificioModel polizaSacrificioModel = lotesSacrificioRegistrados.FirstOrDefault(cab =>
                                                                                                                                                                  cab.InterfaceSalidaTraspasoDetalleID == det.InterfaceSalidaTraspasoDetalleID);
                                                                          if (polizaSacrificioModel != null)
                                                                          {
                                                                              det.CabezasSacrificadas = polizaSacrificioModel.Canales;
                                                                          }
                                                                      }));
        }

        /// <summary>
        /// Valida las cabezas por sacrificar
        /// </summary>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <param name="lotesSacrificio"></param>
        private void ValidaCabezasPorSacrificadas(List<InterfaceSalidaTraspasoInfo> interfaceSalidaTraspaso, List<PolizaSacrificioModel> lotesSacrificio)
        {
            interfaceSalidaTraspaso.ForEach(datos => datos.ListaInterfaceSalidaTraspasoDetalle
                                                         .ForEach(det =>
                                                                      {
                                                                          PolizaSacrificioModel polizaSacrificioModel =
                                                                              lotesSacrificio.FirstOrDefault(cab =>
                                                                                                             cab.InterfaceSalidaTraspasoDetalleID == det.InterfaceSalidaTraspasoDetalleID);
                                                                          if (polizaSacrificioModel != null)
                                                                          {
                                                                              det.Cabezas = polizaSacrificioModel.Canales;
                                                                          }
                                                                      }));
        }

        /// <summary>
        /// Obtiene el lote sacrificio a cancelar la factura
        /// </summary>
        /// <returns></returns>
        internal LoteSacrificioInfo ObtenerLoteSacrificioACancelarLucero(int organizacionID)
        {
            LoteSacrificioInfo result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerLoteSacrificioACancelarLucero(organizacionID);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Obtiene las facturas generadas a los lotes de sacrificio
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<LoteSacrificioInfo> ObtenerFacturasPorOrdenSacrificioACancelarLucero(int organizacionID)
        {
            List<LoteSacrificioInfo> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerFacturasPorOrdenSacrificioACancelarLucero(organizacionID);
                result = result.GroupBy(x => new { x.Fecha })
                    .Select(dato => new LoteSacrificioInfo
                {
                    FolioFactura = dato.Select(org => org.FolioFactura).FirstOrDefault()
                }).ToList();

            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Recalcula los importes de Canal, Piel y Viscera
        /// </summary>
        /// <param name="polizasSacrificioSukarne"></param>
        /// <param name="polizasSacrificioTraspaso"> </param>
        /// <param name="lotesSacrificioFolios"></param>
        /// <param name="interfaceSalidaTraspaso"></param>
        /// <param name="cancelacion"> </param>
        private void GenerarImportesSacrificioLucero(IList<PolizaInfo> polizasSacrificioSukarne
                                                   , IList<PolizaInfo> polizasSacrificioTraspaso
                                                   , List<PolizaSacrificioModel> lotesSacrificioFolios
                                                   , List<InterfaceSalidaTraspasoInfo> interfaceSalidaTraspaso
                                                   , bool cancelacion)
        {
            polizasSacrificioTraspaso =
                polizasSacrificioTraspaso.Where(
                    imp => imp.Importe.IndexOf("-", StringComparison.CurrentCultureIgnoreCase) < 0).ToList();
            if (polizasSacrificioTraspaso.Any())
            {
                int folioTraspaso =
                    polizasSacrificioTraspaso.Select(folio => Convert.ToInt32(folio.TextoAsignado)).FirstOrDefault();
                InterfaceSalidaTraspasoInfo traspaso =
                    interfaceSalidaTraspaso.FirstOrDefault(id => id.FolioTraspaso == folioTraspaso);
                if (traspaso != null)
                {
                    var precioPACBL = new PrecioPACDAL();
                    PrecioPACInfo precioPac =
                        precioPACBL.ObtenerPrecioPACActivo(traspaso.OrganizacionDestino.OrganizacionID);
                    if (precioPac == null)
                    {
                        precioPac = new PrecioPACInfo();
                    }
                    decimal importe = polizasSacrificioTraspaso.Sum(imp => Convert.ToDecimal(imp.Importe));
                    traspaso.ListaInterfaceSalidaTraspasoDetalle
                        .ForEach(datos =>
                                     {
                                         PolizaSacrificioModel polizaSacrificioModel =
                                             lotesSacrificioFolios.FirstOrDefault(
                                                 detId =>
                                                 detId.InterfaceSalidaTraspasoDetalleID ==
                                                 datos.InterfaceSalidaTraspasoDetalleID);
                                         if (polizaSacrificioModel != null)
                                         {
                                             List<PolizaInfo> polizasAModificar =
                                                 polizasSacrificioSukarne.Where(
                                                     folio =>
                                                     folio.Concepto
                                                         .IndexOf(string.Format("{0}-{1}", polizaSacrificioModel.Serie,
                                                                                polizaSacrificioModel.Folio),
                                                                  StringComparison.CurrentCultureIgnoreCase) >= 0).
                                                     OrderBy(linea => Convert.ToInt32(linea.NumeroLinea)).ToList();
                                             if (polizasAModificar.Any())
                                             {
                                                 decimal importeTotal = importe + (datos.Cabezas * 320);
                                                 decimal importeCanal = 0;
                                                 decimal importePiel = polizaSacrificioModel.PesoPiel * precioPac.Precio;
                                                 decimal importeViscera = datos.Cabezas * precioPac.PrecioViscera;

                                                 const int CANAL = 1;
                                                 const int PIEL = 2;
                                                 const int VISCERA = 3;
                                                 polizasAModificar.ForEach(poliza =>
                                                                               {
                                                                                   switch (Convert.ToInt32(poliza.NumeroLinea))
                                                                                   {
                                                                                       case CANAL:
                                                                                           importeCanal = importeTotal -
                                                                                                          (importePiel +
                                                                                                           importeViscera);
                                                                                           poliza.Importe =
                                                                                               string.Format("{0}{1:F2}", cancelacion ? string.Empty : "-",
                                                                                                             importeCanal);
                                                                                           polizaSacrificioModel.
                                                                                               ImporteCanal =
                                                                                               importeCanal;
                                                                                           break;
                                                                                       case PIEL:
                                                                                           poliza.Importe =
                                                                                               string.Format("{0}{1:F2}", cancelacion ? string.Empty : "-",
                                                                                                             importePiel);
                                                                                           polizaSacrificioModel.
                                                                                               ImportePiel = importePiel;
                                                                                           break;
                                                                                       case VISCERA:
                                                                                           poliza.Importe =
                                                                                               string.Format("{0}{1:F2}", cancelacion ? string.Empty : "-",
                                                                                                             importeViscera);
                                                                                           polizaSacrificioModel.
                                                                                               ImporteViscera =
                                                                                               importeViscera;
                                                                                           break;
                                                                                       case 4:
                                                                                           poliza.Importe =
                                                                                               string.Format("{0}{1:F2}", cancelacion ? "-" : string.Empty,
                                                                                                             importeCanal +
                                                                                                             importePiel +
                                                                                                             importeViscera);
                                                                                           break;
                                                                                   }
                                                                               });
                                             }
                                         }
                                     });
                }
            }
        }

        /// <summary>
        /// Actualiza los importes de Canal, Piel, Viscerea
        /// </summary>
        /// <param name="lotesSacrificio"></param>
        internal void ActualizarImportesSacrificioLucero(List<PolizaSacrificioModel> lotesSacrificio)
        {
            try
            {
                Logger.Info();
                var loteSacrificioDAL = new LoteSacrificioDAL();
                loteSacrificioDAL.ActualizarImportesSacrificioLucero(lotesSacrificio);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los datos para conciliacion
        /// de sacrificios de ganado por traspaso
        /// </summary>
        /// <param name="interfaceSalidaTraspasos"></param>
        /// <returns></returns>
        internal List<PolizaSacrificioModel> ObtenerDatosConciliacionSacrificioTraspasoGanado(List<InterfaceSalidaTraspasoInfo> interfaceSalidaTraspasos)
        {
            List<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerDatosConciliacionSacrificioTraspasoGanado(interfaceSalidaTraspasos);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Obtiene los datos para Generar
        /// las polizas de sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IEnumerable<PolizaSacrificioModel> ObtenerDatosGenerarPolizaSacrificio(DateTime fecha)
        {
            IEnumerable<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerDatosGenerarPolizaSacrificio(fecha);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Actualiza bandera de poliza generada
        /// </summary>
        /// <param name="lotesSacrificio"></param>
        internal void ActualizarPolizaGenerada(List<PolizaSacrificioModel> lotesSacrificio)
        {
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                loteSacrificioDal.ActualizarPolizaGenerada(lotesSacrificio);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Obtiene los datos para cancelar
        /// las polizas de sacrificio por fecha
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IEnumerable<PolizaSacrificioModel> ObtenerDatosPolizaSacrificioCanceladas(DateTime fecha)
        {
            IEnumerable<PolizaSacrificioModel> result;
            try
            {
                Logger.Info();
                var loteSacrificioDal = new LoteSacrificioDAL();
                result = loteSacrificioDal.ObtenerDatosPolizaSacrificioCanceladas(fecha);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return result;
        }
    }
}
