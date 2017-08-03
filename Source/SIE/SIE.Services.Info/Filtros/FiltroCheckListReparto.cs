using System;

namespace SIE.Services.Info.Filtros
{
    public class FiltroCheckListReparto
    {
        public int OperadorID { get; set; }
        public int TipoServicioID { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroEconomico { get; set; }
        public int CamionRepartoID { get; set; }
        public int OrganizacionID { get; set; }
        public string NumeroTolva { get; set; }
    }
}
