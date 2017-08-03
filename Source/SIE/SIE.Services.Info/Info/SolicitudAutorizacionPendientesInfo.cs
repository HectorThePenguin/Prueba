using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class SolicitudAutorizacionPendientesInfo
    {
        public long Folio { set; get; }
        public int AutorizacionID { get; set; }
        public string Producto { set; get; }
        public long AlmacenMovimientoID { set; get; }
        public string Almacen { set; get; }
        public int Lote { set; get; }
        public decimal Costo { set; get; }
        public int LoteNuevo { set; get; }
        public decimal Precio { set; get; }
        public decimal CantidadAjuste { set; get; }
        public decimal PorcentajeAjuste { set; get; }
        public string Justificacion { set; get; }
    }
}