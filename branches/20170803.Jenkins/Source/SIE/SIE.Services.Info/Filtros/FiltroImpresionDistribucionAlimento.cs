using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroImpresionDistribucionAlimento
    {
        /// <summary>
        /// Organizacion del usuario Logueado
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// Fecha de la distribucion de alimento
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Tipo de Servicio de la distribucion de alimento
        /// </summary>
        public TipoServicioInfo TipoServicio { get; set; }
    }
}
