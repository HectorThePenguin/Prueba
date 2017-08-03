using System;
namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaMuerteTratamiento
    {
        /// <summary>
        /// Codigo del tratamiento
        /// </summary>
        public int CodigoTratamiento { get; set; }
        /// <summary>
        /// Identificador del animal
        /// </summary>
        public long AnimalID { get; set; }
        /// <summary>
        /// Identificador del movimiento del animal
        /// </summary>
        public long AnimalMovimientoID { get; set; }
        /// <summary>
        /// Fecha del movimiento
        /// </summary>
        public DateTime FechaMovimiento { get; set; }
        /// <summary>
        /// Descripcion del producto
        /// </summary>
        public string Producto { get; set; }
    }
}
