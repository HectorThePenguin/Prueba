namespace SIE.Services.Info.Info
{
    public class EntradaGanadoTransitoDetalleInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador de EntradaGanadoTransitoDetalle
        /// </summary>
        public int EntradaGanadoTransitoDetalleID { get; set; }
        /// <summary>
        /// Identificador de EntradaGanadoTransito
        /// </summary>
        public int EntradaGanadoTransitoID { get; set; }
        /// <summary>
        /// Costo que sera aplicado al transito
        /// </summary>
        public CostoInfo Costo { get; set; }
        /// <summary>
        /// Importe que sera aplicado al transito
        /// </summary>
        public decimal Importe { get; set; }
    }
}
