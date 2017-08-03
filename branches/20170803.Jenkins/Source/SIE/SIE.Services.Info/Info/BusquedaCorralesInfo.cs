using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class BusquedaCorralesInfo
    {
        public int Folio { get; set; }
        public string Corral { get; set; }
        public int Lote { get; set; }
        public int Cabezas { get; set; }
        public double Kgsllegada { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string Origen { get; set; }
        public int Salida { get; set; }
        public int CorralId { get; set; }
        public double PesoOrigen { get; set; }
        public DateTime FechaSalida { get; set; }
    }
}
