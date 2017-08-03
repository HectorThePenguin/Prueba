namespace SIE.Services.Info.Info
{
    public class EntradaGanadoMuerteDetalleInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador de Entrada Ganado Muerte
        /// </summary>
        public EntradaGanadoMuerteInfo EntradaGanadoMuerte { get; set; }
        /// <summary>
        /// Representa el costo que se aplicara
        /// </summary>
        public CostoInfo Costo { get; set; }
        /// <summary>
        /// Indica el importe que se vera afectado
        /// </summary>
        public decimal Importe { get; set; }
    }
}
