namespace SIE.Services.Info.Modelos
{
    public class ReporteInventarioPaseProcesoModel
    {
        public string FolioPaseProceso { get; set; }
        public int TipoMovimientoOrigenID { get; set; }
        public int TipoMovimientoDestinoID { get; set; }
        public long AlmacenMovimientoOrigenID { get; set; }
        public long AlmacenMovimientoDestinoID { get; set; }
    }
}
