using System.Collections.Generic;

namespace SIE.Services.Info.Reportes
{
    public class ReporteCronicosRecuperacionDatos
    {
        /// <summary>
        /// Folios
        /// </summary>
        public List<ReporteCronicosRecuperacionFolio> Folios { get; set; }
        /// <summary>
        /// Movimientos de Enfermeria
        /// </summary>
        public List<ReporteCronicosRecuperacionEnfermeria> MovimientosEnfermeria { get; set; }
        /// <summary>
        /// Movimientos de Produccion
        /// </summary>
        public List<ReporteCronicosRecuperacionProduccion> MovimientosProduccion { get; set; }
        /// <summary>
        /// Tratamientos
        /// </summary>
        public List<ReporteCronicosRecuperacionTratamiento> Tratamientos { get; set; }
    }
}
