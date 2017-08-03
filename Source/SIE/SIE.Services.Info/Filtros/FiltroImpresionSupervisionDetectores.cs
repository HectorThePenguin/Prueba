using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroImpresionSupervisionDetectores
    {
        /// <summary>
        /// id de la supervision
        /// </summary>
        public int SupervisionDetectoresID { get; set; }
        /// <summary>
        /// Organizacion del filtro de la pantalla
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// Fecha de Supervision
        /// </summary>
        public DateTime FechaSupervision { get; set; }
    }
}
