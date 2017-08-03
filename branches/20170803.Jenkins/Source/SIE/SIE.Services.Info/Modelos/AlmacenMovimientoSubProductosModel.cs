using System;
namespace SIE.Services.Info.Modelos
{
    public class AlmacenMovimientoSubProductosModel
    {
        public int AlmacenID { get; set; }
        public long AlmacenMovimientoID { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int ProductoID { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Importe { get; set; }
    }
}
