using System;
namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaGanadoMovimientosProduccion
    {
        /// <summary>
        /// Identificador del movimiento del animal
        /// </summary>
        public long AnimalMovimientoID { get; set; }
        /// <summary>
        /// Identificador del animal
        /// </summary>
        public long AnimalID { get; set; }
        /// <summary>
        /// Corral de produccion en que estuvo el animal
        /// </summary>
        public string Corral { get; set; }
        /// <summary>
        /// Fecha del movimiento
        /// </summary>
        public DateTime FechaMovimiento { get; set; }
    }
}
