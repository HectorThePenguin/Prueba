using System.Collections.Generic;

namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaMuerteDatos
    {
        /// <summary>
        /// Folios
        /// </summary>
        public List<ReporteVentaMuerteFolio> Folios { get; set; }
        /// <summary>
        /// Movimientos de Enfermeria
        /// </summary>
        public List<ReporteVentaMuerteMovimientosEnfermeria> MovimientosEnfermeria { get; set; }
        /// <summary>
        /// Movimientos de Produccion
        /// </summary>
        public List<ReporteVentaMuerteMovimientosProduccion> MovimientosProduccion { get; set; }
        /// <summary>
        /// Tratamientos
        /// </summary>
        public List<ReporteVentaMuerteTratamiento> Tratamientos { get; set; }
    }
}
