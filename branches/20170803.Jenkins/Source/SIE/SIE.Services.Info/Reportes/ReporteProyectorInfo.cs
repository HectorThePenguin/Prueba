using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteProyectorInfo
    {
        public string Corral { get; set; }
        public string Lote { get; set; }
        public int Cabezas { get; set; }
        public string TipoGanado { get; set; }
        public string Clasificacion { get; set; }
        public int PesoOrigen { get; set; }
        public decimal Merma { get; set; }
        public int PesoProyectado { get; set; }
        public decimal GananciaDiaria { get; set; }
        public int DiasEngorda { get; set; }
        public string Fecha1Reimplante { get; set; }
        public int Peso1Reimplante { get; set; }
        public decimal Ganancia1Diaria { get; set; }
        public string Fecha2Reimplante { get; set; }
        public int Peso2Reimplante { get; set; }
        public decimal Ganancia2Diaria { get; set; }
        public string Fecha3Reimplante { get; set; }
        public int Peso3Reimplante { get; set; }
        public decimal Ganancia3Diaria { get; set; }
        public int DiasF4 { get; set; }
        public int DiasZilmax { get; set; }
        public string FechaSacrificio { get; set; }
        public string Titulo { get; set; }
        public string Organizacion { get; set; }
        public DateTime FechaReporte { get; set; }
    }
}
