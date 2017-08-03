using System;
namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaMuerteMovimientosProduccion
    {
        /// <summary>
        /// Identificador del animal
        /// </summary>
        public int AnimalID { get; set; }
        /// <summary>
        /// Identificador del movimiento del animal
        /// </summary>
        public long AnimalMovimientoID { get; set; }
        /// <summary>
        /// Corral de Produccion
        /// </summary>
        public string CorralProduccion { get; set; }
        /// <summary>
        /// Fecha ultima que paso en produccion
        /// </summary>
        public DateTime Fecha { get; set; }
    }
}
