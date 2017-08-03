using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteKardexGanadoInfo
    {
        public int CorralId { get; set; }
        public string Codigo { get; set; }
        public int LoteId { get; set; }
        public string Lote { get; set; }
        public string Tipo { get; set; }
        public int CabezasInicial { get; set; }
        public int KgsInicial { get; set; }
        public int CabezasEntradas { get; set; }
        public int KgsEntradas { get; set; }
        public int CabezasSalidas { get; set; }
        public int KgsSalidas { get; set; }
        public int CabezasFinal { get; set; }
        public int KgsFinal { get; set; }
        public decimal CostoInv { get; set; }
        public decimal Promedio { get; set; }
        public string Titulo { get; set; }
        public string Organizacion { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoCorralId { get; set; }
    }
}
