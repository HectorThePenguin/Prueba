using System;

namespace SIE.Services.Info.Filtros
{
    public class FiltrosAutorizarCierreDiaInventarioPA
    {
        public int AlmacenID { get; set; }
        public int TipoMovimientoID { get; set; }
        public int FolioMovimiento { get; set; }
        public int EstatusMovimiento { get; set; }
        public DateTime FechaMovimiento { get; set; }
    }
}
