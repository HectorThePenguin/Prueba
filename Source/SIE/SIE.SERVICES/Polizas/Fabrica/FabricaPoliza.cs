using SIE.Services.Info.Enums;
using SIE.Services.Polizas.TiposPoliza;

namespace SIE.Services.Polizas.Fabrica
{
    /// <summary>
    /// Clase para Generar un Tipo de Poliza
    /// </summary>
    public class FabricaPoliza
    {
        private static FabricaPoliza _fabrica;

        private FabricaPoliza()
        {
        }

        /// <summary>
        /// Obtiene la Instancia de la Fabrica
        /// </summary>
        /// <returns></returns>
        public static FabricaPoliza ObtenerInstancia()
        {
            return _fabrica ?? (_fabrica = new FabricaPoliza());
        }

        /// <summary>
        /// Obtiene el Tipo de Poliza que se
        /// estara generando
        /// </summary>
        /// <param name="tipoPoliza">Enumerador con el Tipo de Poliza por Generar</param>
        /// <returns></returns>
        public PolizaAbstract ObtenerTipoPoliza(TipoPoliza tipoPoliza)
        {
            PolizaAbstract polizaAbstract = null;
            switch (tipoPoliza)
            {
                case TipoPoliza.EntradaGanado:
                    polizaAbstract = new PolizaEntradaGanado();
                    break;
                case TipoPoliza.ConsumoProducto:
                    polizaAbstract = new PolizaConsumoProducto();
                    break;
                case TipoPoliza.SalidaMuerte:
                    polizaAbstract = new PolizaSalidaMuerte();
                    break;
                case TipoPoliza.SalidaVenta:
                    polizaAbstract = new PolizaSalidaVenta();
                    break;
                case TipoPoliza.SalidaVentaIntensiva:
                    polizaAbstract = new PolizaSalidaVentaIntensiva();
                    break;
                case TipoPoliza.ConsumoAlimento:
                    polizaAbstract = new PolizaConsumoAlimento();
                    break;
                case TipoPoliza.EntradaCompra:
                    polizaAbstract = new PolizaEntradaCompra();
                    break;
                case TipoPoliza.SalidaAjuste:
                    polizaAbstract = new PolizaSalidaPorAjuste();
                    break;
                case TipoPoliza.SalidaTraspaso:
                    polizaAbstract = new PolizaSalidaTraspaso();
                    break;
                case TipoPoliza.EntradaAjuste:
                    polizaAbstract = new PolizaEntradaPorAjuste();
                    break;
                case TipoPoliza.SalidaVentaProducto:
                    polizaAbstract = new PolizaSalidaVentaProducto();
                    break;
                case TipoPoliza.SalidaConsumo:
                    polizaAbstract = new PolizaSalidaConsumo();
                    break;
                case TipoPoliza.PaseProceso:
                    polizaAbstract = new PolizaPaseProceso();
                    break;
                case TipoPoliza.ProduccionAlimento:
                    polizaAbstract = new PolizaProduccionAlimento();
                    break;
                case TipoPoliza.EntradaCompraMateriaPrima:
                    polizaAbstract = new PolizaEntradaCompraMateriaPrima();
                    break;
                case TipoPoliza.GastosInventario:
                    polizaAbstract = new PolizaGastosInventario();
                    break;
                case TipoPoliza.GastosMateriaPrima:
                    polizaAbstract = new PolizaGastosMateriaPrima();
                    break;
                case TipoPoliza.PolizaContratoTerceros:
                case TipoPoliza.PolizaContratoTransito:
                    polizaAbstract = new PolizaContrato();
                    break;
                case TipoPoliza.PolizaContratoOtrosCostos:
                    polizaAbstract = new PolizaContratoOtrosCostos();
                    break;
                case TipoPoliza.PolizaContratoParcialidades:
                    polizaAbstract = new PolizaContratoParcialidades();
                    break;
                case TipoPoliza.PolizaSubProducto:
                    polizaAbstract = new PolizaSubProducto();
                    break;
                case TipoPoliza.PolizaSacrificio:
                    polizaAbstract = new PolizaSacrificio();
                    break;
                case TipoPoliza.PolizaPremezcla:
                    polizaAbstract = new PolizaPremezcla();
                    break;
                case TipoPoliza.EntradaTraspaso:
                    polizaAbstract = new PolizaEntradaTraspaso();
                    break;
                case TipoPoliza.SalidaGanado:
                    polizaAbstract = new PolizaSalidaGanado();
                    break;
                case TipoPoliza.PolizaSacrificioTraspasoGanado:
                    polizaAbstract = new PolizaSacrificioTraspasoGanado();
                    break;
                case TipoPoliza.PolizaMuerteTransito:
                    polizaAbstract = new PolizaMuerteTransito();
                    break;
                case TipoPoliza.EntradaGanadoDurango:
                    polizaAbstract = new PolizaEntradaGanadoIntensivo();
                    break;
                case TipoPoliza.EntradaTraspasoSAP:
                    polizaAbstract = new PolizaEntradaMaterialSAP();
                    break;
                case TipoPoliza.SalidaMuerteIntensiva:
                    polizaAbstract = new PolizaSalidaMuerteIntensiva();
                    break;
				case TipoPoliza.SalidaMuerteEnTransito:
                    polizaAbstract = new PolizaSalidaPorMuerteEnTransito();
                    break;
                case TipoPoliza.SalidaVentaEnTransito:
                    polizaAbstract = new PolizaSalidaGanadoEnTransitoPorVenta();

                    break;
            }
            return polizaAbstract;
        }
    }
}
