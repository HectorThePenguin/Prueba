using System;

namespace SIE.Services.Info.Filtros
{
    public class FiltroCierreCorral
    {
        public int OrganizacionID { get; set; }
        public int LoteID { get; set; }
        public DateTime FechaCierre { get; set; }
        public DateTime FechaSacrificio { get; set; }
        public int UsuarioModificacionID { get; set; }
        public DateTime FechaEjecucion { get; set; }
    }
}
