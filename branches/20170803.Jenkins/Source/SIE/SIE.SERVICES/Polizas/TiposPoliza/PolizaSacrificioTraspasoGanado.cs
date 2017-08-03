using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Polizas.TiposPoliza
{
    public class PolizaSacrificioTraspasoGanado : PolizaAbstract
    {
        #region METODOS SOBREESCRITOS

        public override MemoryStream ImprimePoliza(object datosPoliza, IList<PolizaInfo> polizas)
        {
            throw new System.NotImplementedException();
        }

        public override IList<PolizaInfo> GeneraPoliza(object datosPoliza)
        {
            var interfaceSalidaTraspaso = datosPoliza as InterfaceSalidaTraspasoInfo;
            IList<PolizaInfo> polizaGenerada = null;

            var interfaceSalidaTraspasoCostoBL = new InterfaceSalidaTraspasoCostoBL();
            List<InterfaceSalidaTraspasoCostoInfo> interfaceSalidaTraspasoCosto =
                interfaceSalidaTraspasoCostoBL.ObtenerCostosInterfacePorDetalle(
                    interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle);
            if (interfaceSalidaTraspasoCosto != null && interfaceSalidaTraspasoCosto.Any())
            {
                polizaGenerada = ObtenerPoliza(interfaceSalidaTraspaso, interfaceSalidaTraspasoCosto);
            }
            return polizaGenerada;
        }

        #endregion METODOS SOBREESCRITOS

        #region METODOS PRIVADOS

        private IList<PolizaInfo> ObtenerPoliza(InterfaceSalidaTraspasoInfo interfaceSalidaTraspaso, List<InterfaceSalidaTraspasoCostoInfo> interfaceSalidaTraspasoCosto)
        {
            var polizasSalidaTraspaso = new List<PolizaInfo>();

            IList<CuentaSAPInfo> cuentasSap = ObtenerCuentasSAP();

            TipoPolizaInfo tipoPoliza =
                TiposPoliza.FirstOrDefault(
                    clave => clave.TipoPolizaID == TipoPoliza.PolizaSacrificioTraspasoGanado.GetHashCode());
            if (tipoPoliza == null)
            {
                throw new ExcepcionServicio(string.Format("{0} {1}", "EL TIPO DE POLIZA",
                                                          TipoPoliza.PolizaSacrificioTraspasoGanado));
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

            List<LoteInfo> lotes =
                interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Select(lote => lote.Lote).ToList();

            var costoBL = new CostoBL();
            IList<CostoInfo> costos = costoBL.ObtenerTodos(EstatusEnum.Activo);

            List<InterfaceSalidaTraspasoCostoInfo> animalesPorLote;
            int loteID;

            const string COMPLEMENTO_REF1 = "czas.";
            const string UNIDAD_MOVIMIENTO = "Kgs.";
            const string DESCRIPCION_MOVIMIENTO = "CABEZAS";

            DateTime fechaEnvio = interfaceSalidaTraspaso.FechaEnvio;

            ParametroOrganizacionInfo parametroOrganizacion =
                ObtenerParametroOrganizacionPorClave(organizacionOrigen.OrganizacionID,
                                                     ParametrosEnum.CTACENTROCOSTOENG.ToString());
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
                animalesPorLote = (from ani in interfaceSalidaTraspasoCosto
                                   from movs in interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle
                                   where movs.Lote.LoteID == loteID
                                         &&
                                         ani.InterfaceSalidaTraspasoDetalle.InterfaceSalidaTraspasoDetalleID ==
                                         movs.InterfaceSalidaTraspasoDetalleID
                                   orderby ani.InterfaceSalidaTraspasoCostoID, ani.AnimalID
                                   select ani).ToList();
                var renglon = 1;
                if (animalesPorLote.Any())
                {
                    int cabezas = interfaceSalidaTraspaso.ListaInterfaceSalidaTraspasoDetalle.Where(
                        loteId => loteId.Lote.LoteID == loteID).Select(
                            cabe => cabe.Cabezas).FirstOrDefault();
                    List<long> animalesAFacturar = ObtenerAnimalesPorFacturar(animalesPorLote, cabezas);
                    animalesPorLote =
                        animalesPorLote.Join(animalesAFacturar, lote => lote.AnimalID, id => id, (lote, id) => lote).
                            ToList();
                    var interfaceSalidaTraspasoCostoBL = new InterfaceSalidaTraspasoCostoBL();
                    interfaceSalidaTraspasoCostoBL.ActualizarFacturado(animalesAFacturar);
                    if (animalesPorLote.Any())
                    {
                        if (animalesPorLote.Any())
                        {
                            animalesPorLote = animalesPorLote
                                .GroupBy(costoAnimal => costoAnimal.Costo.CostoID)
                                .Select(dinero => new InterfaceSalidaTraspasoCostoInfo
                                {
                                    AnimalID =
                                        dinero.Select(id => id.AnimalID).FirstOrDefault(),
                                    Costo = new CostoInfo
                                    {
                                        CostoID = dinero.Key
                                    },
                                    Importe = dinero.Sum(imp => imp.Importe),
                                }).ToList();
                            ClaveContableInfo claveContableDestino;
                            ClaveContableInfo claveContableOrigen;
                            CostoInfo costo;

                            CuentaSAPInfo cuentaSapDestino;
                            CuentaSAPInfo cuentaSapOrigen;
                            DatosPolizaInfo datos;
                            PolizaInfo polizaSalida;

                            decimal pesoOrigen = interfaceSalidaTraspaso.PesoBruto -
                                                 interfaceSalidaTraspaso.PesoTara;
                            InterfaceSalidaTraspasoCostoInfo animalCosto;
                            for (var indexCostos = 0; indexCostos < animalesPorLote.Count; indexCostos++)
                            {
                                animalCosto = animalesPorLote[indexCostos];
                                costo =
                                    costos.FirstOrDefault(
                                        id => id.CostoID == animalCosto.Costo.CostoID);
                                claveContableDestino = ObtenerCuentaInventario(costo
                                                                               , organizacionDestino.OrganizacionID
                                                                               ,
                                                                               TipoPoliza.SalidaGanado);
                                claveContableOrigen = ObtenerCuentaInventario(costo
                                                                              , organizacionOrigen.OrganizacionID
                                                                              ,
                                                                              TipoPoliza.
                                                                                  PolizaSacrificioTraspasoGanado);
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
                                    CabezasRecibidas = cabezas.ToString(),
                                    NumeroDocumento = folioTraspaso.ToString(),
                                    ClaseDocumento = postFijoRef3,
                                    Importe =
                                        string.Format("{0}", Cancelacion ? (animalCosto.Importe * -1).ToString("F2")
                                                                         : animalCosto.Importe.ToString("F2")),
                                    IndicadorImpuesto = String.Empty,
                                    Renglon = Convert.ToString(renglon++),
                                    Cabezas = Convert.ToString(cabezas),
                                    ImporteIva = "0",
                                    Ref3 = ref3.ToString(),
                                    ArchivoFolio = archivoFolio.ToString(),
                                    DescripcionCosto = cuentaSapOrigen.Descripcion,
                                    Cuenta = cuentaSapOrigen.CuentaSAP,
                                    CentroCosto =
                                        cuentaSapOrigen.CuentaSAP.StartsWith(PrefijoCuentaCentroCosto)
                                            ? parametroOrganizacion.Valor
                                            : string.Empty,
                                    PesoOrigen = pesoOrigen,
                                    Division = divisionOrigen,
                                    ComplementoRef1 = COMPLEMENTO_REF1,
                                    TipoDocumento = textoDocumento,
                                    Concepto =
                                        String.Format("{0}-{1} ,{2} {3}, {4} {5}", prefijoConcepto,
                                                      folioTraspaso,
                                                      cabezas, DESCRIPCION_MOVIMIENTO,
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
                                    CabezasRecibidas = cabezas.ToString(),
                                    NumeroDocumento = folioTraspaso.ToString(),
                                    ClaseDocumento = postFijoRef3,
                                    Importe =
                                        string.Format("{0}", Cancelacion ? animalCosto.Importe.ToString("F2")
                                                                         : (animalCosto.Importe * -1).ToString("F2")),
                                    IndicadorImpuesto = String.Empty,
                                    Renglon = Convert.ToString(renglon++),
                                    Cabezas = Convert.ToString(cabezas),
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
                                                      cabezas, DESCRIPCION_MOVIMIENTO,
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
            return polizasSalidaTraspaso;
        }

        /// <summary>
        /// Obtiene los indentificadores de los animales que
        /// seran facturados en esta fecha
        /// </summary>
        /// <param name="animalesPorLote"></param>
        /// <param name="cabezas"></param>
        /// <returns></returns>
        private List<long> ObtenerAnimalesPorFacturar(List<InterfaceSalidaTraspasoCostoInfo> animalesPorLote, int cabezas)
        {
            var identificadores = new HashSet<long>(animalesPorLote.Select(id => id.AnimalID));
            return identificadores.Take(cabezas).ToList();
        }

        #endregion METODOS PRIVADOS
    }
}
