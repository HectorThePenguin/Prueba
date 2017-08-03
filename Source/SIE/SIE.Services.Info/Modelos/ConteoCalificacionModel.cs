using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ConteoCalificacionModel
    {
        /// <summary>
        /// Propiedad para obtener las calidades de Machos para el conteo de calificacion
        /// </summary>
        public List<CalidadGanadoInfo> CalidadMachos { get; set; }
        /// <summary>
        /// Propiedad para obtener las calidades de Hembras para el conteo de calificacion
        /// </summary>
        public List<CalidadGanadoInfo> CalidadHembras { get; set; }
    }
}
