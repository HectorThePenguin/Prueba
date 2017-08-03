using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Filtros
{
    public class FiltroImpresionCalidadMezclado
    {
        /// <summary>
        /// Organizacion donde esta logueado el Usuario
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// Fecha del mezclado de calidad
        /// </summary>
        public DateTime Fecha { get; set; }
    }
}
