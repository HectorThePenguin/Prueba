using System;
using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ContenedorReporteProyectorInfo
    {
        public int CorralID { get; set; }
        public string CodigoCorral { get; set; }
        public int LoteID { get; set; }
        public string CodigoLote { get; set; }
        public int Cabezas { get; set; }
        public string TipoGanado { get; set; }
        public DateTime FechaInicioLote { get; set; }
        public DateTime FechaDisponibilidad { get; set; }
        public bool DisponibilidadManual { get; set; }
        public int DiasF4 { get; set; }
        public int DiasZilmax { get; set; }
        public int DiasSacrificio { get; set; }

        public List<AnimalInfo> ListaAnimales { get; set; }
        public LoteProyeccionInfo LoteProyeccion { get; set; }
    }
}
