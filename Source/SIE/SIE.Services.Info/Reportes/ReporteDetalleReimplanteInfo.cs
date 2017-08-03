using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteDetalleReimplanteInfo
    {
        public string Arete{ get; set; }
        public string Descripcion { get; set; }
        public int CorralOrigen { get; set; }
        public int CorralDestino { get; set; }
        public int PesoCorte { get; set; }
        public int PesoReimplante { get; set; }
        public int GananciaDiaria { get; set; }
        public int DiasEngorda { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string Titulo { get; set; }
        public string Organizacion { get; set; }

    }
}
