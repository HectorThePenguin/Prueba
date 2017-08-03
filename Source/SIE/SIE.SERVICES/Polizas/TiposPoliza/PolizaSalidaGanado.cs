using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;
using System.Threading;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaSalidaGanado : PolizaAbstract
    {
        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            throw new System.NotImplementedException();
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var interfaceSalidaTraspaso = datosPoliza as InterfaceSalidaTraspasoInfo;
            IList<PolizaInfo> polizaGenerada = GeneraDatosPoliza(interfaceSalidaTraspaso);
            return polizaGenerada;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> GeneraDatosPoliza(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso)
        {
            var polizasSalidaTraspaso = new List<PolizaInfo>();

            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(clave => clave.TipoPolizaID == TipoPoliza.SalidaGanado.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.SalidaGanado));
            }
            string textoDocumento = tipoPoliza.TextoDocumento;
            string postFijoRef3 = tipoPoliza.PostFijoRef3;
            string prefijoConcepto = tipoPoliza.ClavePoliza;

            OrganizacionInfo organizacionDestino =
                ObtenerOrganizacionIVA(interfaceSalidaTraspaso.OrganizacionDestino.OrganizacionID);

            OrganizacionInfo organizacionOrigen =
                ObtenerOrganizacionIVA(interfaceSalidaTraspaso.OrganizacionId);
            string divisionOrigen = organizacionOrigen.Division;

            long folioTraspaso = interfaceSalidaTraspaso.FolioTraspaso;

            var animalBL = new AnimalBL();
            List<LoteInfo> lotes =
                interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Select(lote => lote.Lote).ToList();
            List<AnimalInfo> animalesMovimientoSalidaTraspaso = animalBL.ObtenerMovimientosPorLoteXML(lotes);
            
            List<AnimalInfo> animalesMovimientoSacrificadosLote = animalBL.ObtenerMovimientosPorLoteSacrificadosXML(lotes);
            if (animalesMovimientoSacrificadosLote == null)
            {
                animalesMovimientoSacrificadosLote = new List<AnimalInfo>();
            }

            if (animalesMovimientoSalidaTraspaso != null && animalesMovimientoSalidaTraspaso.Any())
            {
                var costosGanadoTransferido = new List<InterfaceSalidaTraspasoCostoInfo>();
                var animalesMovimiento = new List<AnimalMovimientoInfo>();
                animalesMovimientoSalidaTraspaso.ForEach(
                    movs => animalesMovimiento.AddRange(movs.ListaAnimalesMovimiento));

                var animalCostoBL = new AnimalCostoBL();
                List<AnimalCostoInfo> costosAnimal =
                    animalCostoBL.ObtenerCostosAnimal(animalesMovimientoSalidaTraspaso);

                var costoBL = new CostoBL();
                IList<CostoInfo> costos = costoBL.ObtenerTodos(EstatusEnum.Activo);

                List<AnimalInfo> animalesPorLote;
                List<AnimalInfo> animalesSacrificadosPorLote;

                int loteID;

                const string COMPLEMENTO_REF1 = "czas.";
                const string UNIDAD_MOVIMIENTO = "Kgs.";
                const string DESCRIPCION_MOVIMIENTO = "CABEZAS";

                DateTime fechaEnvio = interfaceSalidaTraspaso.FechaEnvio;
                
                for (var indexLotes = 0; indexLotes < lotes.Count; indexLotes++)
                {
                    Thread.Sleep(999);
                    var ref3 = new StringBuilder();
                    ref3.Append("03");
                    ref3.Append(Convert.ToString(folioTraspaso).PadLeft(10, ' '));
                    ref3.Append(new Random(10).Next(10, 20));
                    ref3.Append(new Random(30).Next(30, 40));
                    ref3.Append(DateTime.Now.Millisecond);
                    ref3.Append(postFijoRef3);                    

                    var archivoFolio =
                        new StringBuilder(ObtenerArchivoFolio(fechaEnvio));
                    //var numeroDocumento =
                    //    new StringBuilder(string.Format("{0}{1}", folioTraspaso, ObtenerNumeroReferencia));
                    var numeroDocumento = ObtenerNumeroReferenciaFolio(folioTraspaso);

                    loteID = lotes[indexLotes].LoteID;
                    animalesPorLote = (from ani in animalesMovimientoSalidaTraspaso
                                       from movs in animalesMovimiento
                                       where ani.AnimalID == movs.AnimalID
                                             && movs.LoteID == loteID
                                       orderby ani.AnimalID
                                       select ani).ToList();
                    animalesSacrificadosPorLote =
                        animalesMovimientoSacrificadosLote.Where(lote => lote.LoteID == loteID).ToList();
                    var renglon = 1;
                    if (animalesPorLote.Any())
                    {
                        int cabezas = interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Where(
                            loteId => loteId.Lote.LoteID == loteID).Select(
                                cabe => cabe.Cabezas).FirstOrDefault();
                        int interfaceSalidaTraspasoDetalleID =
                            interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Where(
                                loteId => loteId.Lote.LoteID == loteID).Select(
                                    cabe => cabe.InterfaceSalidaTraspasoDetalleID).FirstOrDefault();
                        animalesPorLote = EliminarAnimalesSacrificados(animalesPorLote,
                                                                       animalesSacrificadosPorLote);
                        animalesPorLote = animalesPorLote.Take(cabezas).ToList();
                        if (animalesPorLote.Any())
                        {
                            List<AnimalCostoInfo> costosAnimalesTraspasados =
                                costosAnimal.Join(animalesPorLote, info => info.AnimalID, cos => cos.AnimalID,
                                                  (info, cos) => info).ToList();
                            List<AnimalCostoInfo> costosAnimalesTraspasadosIndividual;
                            if (costosAnimalesTraspasados.Any())
                            {
                                costosAnimalesTraspasadosIndividual = costosAnimalesTraspasados
                                    .GroupBy(costoAnimal => new { costoAnimal.CostoID, costoAnimal.AnimalID })
                                    .Select(dinero => new AnimalCostoInfo
                                    {
                                        AnimalCostoID =
                                            dinero.Select(id => id.AnimalCostoID).FirstOrDefault(),
                                        AnimalID = dinero.Key.AnimalID,
                                        CostoID = dinero.Key.CostoID,
                                        FechaCosto =
                                            dinero.Select(fecha => fecha.FechaCosto).
                                            FirstOrDefault(),
                                        FolioReferencia =
                                            dinero.Select(folio => folio.FolioReferencia).
                                            FirstOrDefault(),
                                        Importe = dinero.Sum(imp => imp.Importe),
                                        OrganizacionID =
                                            dinero.Select(org => org.OrganizacionID).
                                            FirstOrDefault(),
                                        TipoReferencia =
                                            dinero.Select(tipo => tipo.TipoReferencia).
                                            FirstOrDefault()
                                    }).ToList();

                                costosAnimalesTraspasados = costosAnimalesTraspasados
                                    .GroupBy(costoAnimal => costoAnimal.CostoID)
                                    .Select(dinero => new AnimalCostoInfo
                                                          {
                                                              AnimalCostoID =
                                                                  dinero.Select(id => id.AnimalCostoID).FirstOrDefault(),
                                                              AnimalID =
                                                                  dinero.Select(id => id.AnimalID).FirstOrDefault(),
                                                              CostoID = dinero.Select(id => id.CostoID).FirstOrDefault(),
                                                              FechaCosto =
                                                                  dinero.Select(fecha => fecha.FechaCosto).
                                                                  FirstOrDefault(),
                                                              FolioReferencia =
                                                                  dinero.Select(folio => folio.FolioReferencia).
                                                                  FirstOrDefault(),
                                                              Importe = dinero.Sum(imp => imp.Importe),
                                                              OrganizacionID =
                                                                  dinero.Select(org => org.OrganizacionID).
                                                                  FirstOrDefault(),
                                                              TipoReferencia =
                                                                  dinero.Select(tipo => tipo.TipoReferencia).
                                                                  FirstOrDefault()
                                                          }).ToList();
                                costosGanadoTransferido.AddRange(
                                    costosAnimalesTraspasadosIndividual.Select(
                                        dato => new InterfaceSalidaTraspasoCostoInfo
                                                    {
                                                        AnimalID = dato.AnimalID,
                                                        Activo = EstatusEnum.Activo,
                                                        Importe = dato.Importe,
                                                        UsuarioCreacionID =
                                                            interfaceSalidaTraspaso.
                                                            UsuarioModificacionID.Value,
                                                        Costo = new CostoInfo
                                                                    {
                                                                        CostoID =
                                                                            dato.CostoID
                                                                    },
                                                        InterfaceSalidaTraspasoDetalle =
                                                            new InterfaceSalidaTraspasoDetalleInfo
                                                                {
                                                                    InterfaceSalidaTraspasoDetalleID
                                                                        =
                                                                        interfaceSalidaTraspasoDetalleID
                                                                }
                                                    }));
                                ClaveContableInfo claveContableDestino;
                                ClaveContableInfo claveContableOrigen;
                                CostoInfo costo;

                                CuentaSAPInfo cuentaSapDestino;
                                CuentaSAPInfo cuentaSapOrigen;
                                DatosPolizaInfo datos;
                                PolizaInfo polizaSalida;

                                int cabezasRecibidas =
                                    interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Sum(cab => cab.Cabezas);
                                decimal pesoOrigen = interfaceSalidaTraspaso.PesoBruto - interfaceSalidaTraspaso.PesoTara;
                                AnimalCostoInfo animalCosto;
                                for (var indexCostos = 0; indexCostos < costosAnimalesTraspasados.Count; indexCostos++)
                                {
                                    animalCosto = costosAnimalesTraspasados[indexCostos];
                                    costo =
                                        costos.FirstOrDefault(
                                            id => id.CostoID == animalCosto.CostoID);
                                    claveContableDestino = ObtenerCuentaInventario(costo
                                                                                   , organizacionOrigen.OrganizacionID
                                                                                   ,
                                                                                   TipoOrganizacion.Ganadera.GetHashCode
                                                                                       ());
                                    claveContableOrigen = ObtenerCuentaInventario(costo
                                                                                  , organizacionDestino.OrganizacionID
                                                                                  , TipoPoliza.SalidaGanado);
                                    cuentaSapDestino =
                                        cuentasSap.FirstOrDefault(
                                            clave => clave.CuentaSAP.Equals(claveContableDestino.Valor));
                                    if (cuentaSapDestino == null)
                                    {
                                        throw new ExcepcionServicio(string.Format("{0} {1}",
                                                                                  "NO SE CUENTA CON CONFIGURACIÓN PARA CUENTA DE INVENTARIO",
                                                                                  claveContableDestino.Valor));
                                    }
                                    cuentaSapOrigen =
                                        cuentasSap.FirstOrDefault(
                                            clave => clave.CuentaSAP.Equals(claveContableOrigen.Valor));
                                    if (cuentaSapOrigen == null)
                                    {
                                        throw new ExcepcionServicio(string.Format("{0} {1}",
                                                                                  "NO SE CUENTA CON CONFIGURACIÓN PARA CUENTA DE TRANSITO",
                                                                                  claveContableOrigen.Valor));
                                    }
                                    datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = fechaEnvio,
                                        Folio = folioTraspaso.ToString(),
                                        CabezasRecibidas = cabezasRecibidas.ToString(),
                                        NumeroDocumento = folioTraspaso.ToString(),
                                        ClaseDocumento = postFijoRef3,
                                        Importe =
                                            string.Format("{0}", Cancelacion ? (animalCosto.Importe * -1).ToString("F2")
                                                                             : animalCosto.Importe.ToString("F2")),
                                        IndicadorImpuesto = String.Empty,
                                        Renglon = Convert.ToString(renglon++),
                                        Cabezas = Convert.ToString(cabezasRecibidas),
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        ArchivoFolio = archivoFolio.ToString(),
                                        DescripcionCosto = cuentaSapOrigen.Descripcion,
                                        Cuenta = cuentaSapOrigen.CuentaSAP,
                                        PesoOrigen = pesoOrigen,
                                        Division = divisionOrigen,
                                        ComplementoRef1 = COMPLEMENTO_REF1,
                                        TipoDocumento = textoDocumento,
                                        Concepto =
                                            String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                                          folioTraspaso,
                                                          cabezasRecibidas, DESCRIPCION_MOVIMIENTO,
                                                          pesoOrigen.ToString("N0"), UNIDAD_MOVIMIENTO),
                                        Sociedad = organizacionDestino.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacionDestino.Sociedad),
                                    };
                                    polizaSalida = GeneraRegistroPoliza(datos);
                                    polizasSalidaTraspaso.Add(polizaSalida);

                                    datos = new DatosPolizaInfo
                                    {
                                        NumeroReferencia = numeroDocumento,
                                        FechaEntrada = fechaEnvio,
                                        Folio = folioTraspaso.ToString(),
                                        CabezasRecibidas = cabezasRecibidas.ToString(),
                                        NumeroDocumento = folioTraspaso.ToString(),
                                        ClaseDocumento = postFijoRef3,
                                        Importe =
                                            string.Format("{0}", Cancelacion ? animalCosto.Importe.ToString("F2")
                                                                             : (animalCosto.Importe * -1).ToString("F2")),
                                        IndicadorImpuesto = String.Empty,
                                        Renglon = Convert.ToString(renglon++),
                                        Cabezas = Convert.ToString(cabezasRecibidas),
                                        ImporteIva = "0",
                                        Ref3 = ref3.ToString(),
                                        ArchivoFolio = archivoFolio.ToString(),
                                        DescripcionCosto = cuentaSapDestino.Descripcion,
                                        Cuenta = cuentaSapDestino.CuentaSAP,
                                        PesoOrigen = pesoOrigen,
                                        Division = divisionOrigen,
                                        ComplementoRef1 = COMPLEMENTO_REF1,
                                        TipoDocumento = textoDocumento,
                                        Concepto =
                                            String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                                          folioTraspaso,
                                                          cabezasRecibidas, DESCRIPCION_MOVIMIENTO,
                                                          pesoOrigen.ToString("N0"), UNIDAD_MOVIMIENTO),
                                        Sociedad = organizacionDestino.Sociedad,
                                        Segmento = string.Format("{0}{1}", PrefijoSegmento, organizacionDestino.Sociedad),
                                    };
                                    polizaSalida = GeneraRegistroPoliza(datos);
                                    polizasSalidaTraspaso.Add(polizaSalida);
                                }
                            }
                        }
                    }
                }
                if (costosGanadoTransferido.Any())
                {
                    var interfaceSalidaTraspasoCostoBL = new InterfaceSalidaTraspasoCostoBL();
                    interfaceSalidaTraspasoCostoBL.Guardar(costosGanadoTransferido);
                }
            }
            return polizasSalidaTraspaso;
        }

        /// <summary>
        /// Obtiene una lista con los animales
        /// pendientes por sacrificar del lote
        /// traspasado para su sacrificio
        /// </summary>
        /// <param name="animalesPorLote"></param>
        /// <param name="animalesMovimientoSacrificadosLote"></param>
        private List<AnimalInfo> EliminarAnimalesSacrificados(List<AnimalInfo> animalesPorLote, List<AnimalInfo> animalesMovimientoSacrificadosLote)
        {
            var animalesEnviados = new HashSet<long>(animalesPorLote.Select(id => id.AnimalID));
            var animalesSacrificados = new HashSet<long>(animalesMovimientoSacrificadosLote.Select(id => id.AnimalID));
            IEnumerable<long> animalesPendientes = animalesEnviados.Except(animalesSacrificados);

            List<AnimalInfo> animalesPendientesSacrificio =
                animalesPorLote.Join(animalesPendientes, info => info.AnimalID, id => id, (info, id) => info).ToList();

            return animalesPendientesSacrificio;
        }

        #endregion METODOS PRIVADOS
    }
}
