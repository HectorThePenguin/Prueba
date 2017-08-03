using System;
using System.Collections.Generic;
namespace SIE.Services.Info.Info
{
    public class ReporteSalidaConCostosInfo
    {
		
        public String Leyenda { get; set; }
        public String NombreReporte { get; set; }
        public String RangoFechas { get; set; }
        public int TipoMovimientoID {get; set;}
        public String TipoSalida {get; set;}
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
         
        public DateTime FechaMovimiento { get; set; }
        public String Corral { get; set; }
        public String TipoGanado { get; set; }
        public String Arete { get; set; }
        public Decimal PesoCompra { get; set; }
        public Decimal PesoNoqueo { get; set; }
        public Decimal PesoCanal { get; set; }
        public Decimal ImporteCanal { get; set; }
        public Decimal PesoPiel { get; set; }
        public Decimal ImportePiel { get; set; }
        public Decimal PesoVisceras { get; set; }
        public Decimal ImporteVisceras { get; set; }
        public Decimal PrecioCanal { get; set; }
        public Decimal PrecioPiel { get; set; }
        public Decimal PrecioVisceras { get; set; }

        public String Costo { get; set; }
        public Decimal ImporteCosto { get; set; }
        public Boolean esSalidaVentaOMuerte { get; set; }
        public Boolean esDetallado { get; set; }
        //public IList<SalidaDatosInfo> Salidas { get; set; }
    }
}
