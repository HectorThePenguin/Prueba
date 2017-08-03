using System;

namespace SIE.Services.Info.Info
{
    public class EntradaEmbarqueInfo
    {
        public int EmbarqueID { get; set; }
        public int FolioEmbarque { get; set; }        
        public string TipoOrganizacion { get; set; }
        public string OrganizacionOrigen { get; set; }
        public string OrganizacionDestino { get; set; }                      
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public string TipoEmbarque { get; set; }
        public string Chofer { get; set; }
        public string PlacaCamion { get; set; }        
    }
}
