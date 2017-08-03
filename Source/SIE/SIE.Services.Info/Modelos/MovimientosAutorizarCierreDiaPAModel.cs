using System;

namespace SIE.Services.Info.Modelos
{
    public class MovimientosAutorizarCierreDiaPAModel
    {
        public int ProductoID { get; set; }
        public string Producto { get; set; }
        public bool ManejaLote { get; set; }
        public int FolioMovimiento { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public string Observaciones { get; set; }
        public int AlmacenInventarioLoteID { get; set; }
        public int Lote { get; set; }
        public int TamanioLote { get; set; }
        public int InventarioTeorico { get; set; }
        public int InventarioFisico { get; set; }
        public decimal PorcentajeMermaSuperavit { get; set; }
        public decimal PorcentajeLote { get; set; }
        public decimal PorcentajePermitido { get; set; }
        public decimal CostoUnitario { get; set; }
    }
}
