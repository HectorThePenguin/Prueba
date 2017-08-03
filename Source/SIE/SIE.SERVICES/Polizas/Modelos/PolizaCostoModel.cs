using System.Security.Cryptography;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Polizas.Modelos
{
    internal class PolizaCostoModel
    {
        [AtributoCostos(TipoPoliza = TipoPoliza.PaseProceso, Orden = 1, Desplazamiento = 2, Alineacion = "right")]
        public string Clave { get; set; }
        [AtributoCostos(TipoPoliza = TipoPoliza.EntradaGanado, Orden = 1, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.EntradaGanadoDurango, Orden = 1, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVenta, Orden = 1, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerte, Orden = 1, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.PaseProceso, Orden = 2, Desplazamiento = 2, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.PolizaMuerteTransito, Orden = 1, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerteIntensiva, Orden = 1, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVentaIntensiva, Orden = 1, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerteEnTransito, Orden = 1, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVentaEnTransito, Orden = 1, Alineacion = "left")]
       
        public string Descripcion { get; set; }
        [AtributoCostos(TipoPoliza = TipoPoliza.EntradaGanado, Orden = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.EntradaGanadoDurango, Orden = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVenta, Orden = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerte, Orden = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.PaseProceso, Orden = 3, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.PolizaMuerteTransito, Orden = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerteIntensiva, Orden = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVentaIntensiva, Orden = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerteEnTransito, Orden = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVentaEnTransito, Orden = 2, Alineacion = "right")]

        public string Parcial { get; set; }
        [AtributoCostos(TipoPoliza = TipoPoliza.EntradaGanado, Orden = 3, Desplazamiento = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.EntradaGanadoDurango, Orden = 3, Desplazamiento = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVenta, Orden = 3, Desplazamiento = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerte, Orden = 3, Desplazamiento = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.PaseProceso, Orden = 6, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.PolizaMuerteTransito, Orden = 3, Desplazamiento = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerteIntensiva, Orden = 3, Desplazamiento = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVentaIntensiva, Orden = 3, Desplazamiento = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerteEnTransito, Orden = 3, Desplazamiento = 2, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVentaEnTransito, Orden = 3, Desplazamiento = 2, Alineacion = "right")]
       
        public string Total { get; set; }
        [AtributoCostos(TipoPoliza = TipoPoliza.EntradaGanado, Orden = 4, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.EntradaGanadoDurango, Orden = 4, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVenta, Orden = 4, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerte, Orden = 4, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.PaseProceso, Orden = 5, Alineacion = "right")]
        [AtributoCostos(TipoPoliza = TipoPoliza.PolizaMuerteTransito, Orden = 4, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerteIntensiva, Orden = 4, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVentaIntensiva, Orden = 4, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaMuerteEnTransito, Orden = 4, Alineacion = "left")]
        [AtributoCostos(TipoPoliza = TipoPoliza.SalidaVentaEnTransito, Orden = 4, Alineacion = "left")]
       
        public string Observaciones { get; set; }
        [AtributoCostos(TipoPoliza = TipoPoliza.PaseProceso, Orden = 4, Desplazamiento = 2, Alineacion = "right")]
        public string Iva { get; set; }
    }
}
