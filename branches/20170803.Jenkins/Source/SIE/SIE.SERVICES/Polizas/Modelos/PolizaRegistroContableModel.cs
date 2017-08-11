﻿using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Polizas.Modelos
{
    internal class PolizaRegistroContableModel
    {
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanado, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanadoDurango, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVenta, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerte, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompra, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompraMateriaPrima, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaAjuste, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaAjuste, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PaseProceso, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.ProduccionAlimento, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaTraspaso, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaConsumo, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaProducto, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspaso, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspasoSAP, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PolizaMuerteTransito, Orden = 1, Alineacion = "left")]
		[AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteIntensiva, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaIntensiva, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteEnTransito, Orden = 1, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaEnTransito, Orden = 1, Alineacion = "left")]
        public string Cuenta { get; set; }
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanado, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanadoDurango, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVenta, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerte, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompra, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompraMateriaPrima, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaAjuste, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaAjuste, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PaseProceso, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.ProduccionAlimento, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaTraspaso, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaConsumo, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaProducto, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspaso, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspasoSAP, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PolizaMuerteTransito, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteIntensiva, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaIntensiva, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteEnTransito, Orden = 2, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaEnTransito, Orden = 2, Alineacion = "left")]
        public string Descripcion { get; set; }
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanado, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanadoDurango, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVenta, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerte, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompra, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompraMateriaPrima, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaAjuste, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaAjuste, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PaseProceso, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.ProduccionAlimento, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaTraspaso, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaConsumo, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaProducto, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspaso, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspasoSAP, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PolizaMuerteTransito, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteIntensiva, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaIntensiva, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteEnTransito, Orden = 3, Alineacion = "left")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaEnTransito, Orden = 3, Alineacion = "left")]
        public string Concepto { get; set; }
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanado, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanadoDurango, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVenta, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerte, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompra, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompraMateriaPrima, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaAjuste, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaAjuste, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PaseProceso, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.ProduccionAlimento, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaTraspaso, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaConsumo, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaProducto, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspaso, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspasoSAP, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PolizaMuerteTransito, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteIntensiva, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaIntensiva, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteEnTransito, Orden = 4, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaEnTransito, Orden = 4, Alineacion = "right")]
        public string Cargo { get; set; }
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanado, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaGanadoDurango, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVenta, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerte, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompra, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaCompraMateriaPrima, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaAjuste, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaAjuste, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PaseProceso, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.ProduccionAlimento, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaTraspaso, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaConsumo, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaProducto, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspaso, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.EntradaTraspasoSAP, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.PolizaMuerteTransito, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteIntensiva, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaIntensiva, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaMuerteEnTransito, Orden = 5, Alineacion = "right")]
        [AtributoRegistroContable(TipoPoliza = TipoPoliza.SalidaVentaEnTransito, Orden = 5, Alineacion = "right")]
        public string Abono { get; set; }
    }
}