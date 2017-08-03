using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaGanadoTratamiento
    {
        /// <summary>
        /// Codigos de los tratamientos
        /// </summary>
        public int CodigoTratamiento { get; set; }
        /// <summary>
        /// Identificador del Animal
        /// </summary>
        public long AnimalID { get; set; }
        /// <summary>
        /// Identificador del animal movimiento
        /// </summary>
        public long AnimalMovimientoID { get; set; }
        /// <summary>
        /// Fecha del movimiento
        /// </summary>
        public DateTime FechaMovimiento { get; set; }
        /// <summary>
        /// Descripción del Producto
        /// </summary>
        public string Producto { get; set; }
    }
}
