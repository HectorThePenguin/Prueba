using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class SupervisionGanadoInfo
    {
        public int SupervisionGanadoID { get; set; }

        public string CodigoCorral { get; set; }

        public int LoteID  { get; set; }

	    public string Arete { get; set; }

	    public string AreteMetalico { get; set; }

	    public DateTime FechaDeteccion  { get; set; }

	    public int ConceptoDeteccionID  { get; set; }

	    public string Acuerdo { get; set; }

	    public int Notificacion  { get; set; }

	    public int Activo  { get; set; }

        public string FotoSupervision { get; set; }
    }
}
