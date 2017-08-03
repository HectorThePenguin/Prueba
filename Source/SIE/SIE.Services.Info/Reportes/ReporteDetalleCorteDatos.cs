using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteDetalleCorteDatos
    {
        public string AreteID { get; set; }
        public int FolioEntrada { get; set; }
        public string Descripcion { get; set; }
        public string CorralOrigen { get; set; }
        public string CorralDestino { get; set; }
        public int PesoOrigen { get; set; }
        public int PesoCorte { get; set; }
        public decimal Merma { get; set; }
        public decimal Temperatura { get; set; }
        public DateTime FecInicial { get; set; }
        public DateTime FecFinal { get; set; }
    }
}
