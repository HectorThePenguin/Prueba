namespace SIE.Services.Info.Info
{
    public class ContenedorVentaGanado
    {
        public VentaGanadoInfo VentaGanado { get; set; }
        public VentaGanadoDetalleInfo VentaGanadoDetalle { get; set; }
        public CausaPrecioInfo CausaPrecio { get; set; }
        public ClienteInfo Cliente { get; set; }
        public int TotalCabezas { get; set; }
        public decimal Importe { get; set; }
        public int CostoId { get; set; }
        public int OrganizacionId { get; set; }
        //public int LoteId { get; set; }
        public int EntradaGandoId { get; set; }
        public LoteInfo Lote { get; set; }
    }
}
