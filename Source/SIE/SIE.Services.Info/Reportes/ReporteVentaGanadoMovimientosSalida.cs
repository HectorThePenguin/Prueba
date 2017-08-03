using System;
namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaGanadoMovimientosSalida
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
        /// Fecha del movimiento del animal
        /// </summary>
        public DateTime FechaMovimiento { get; set; }
        /// <summary>
        /// Corral al que pertenece el animal
        /// </summary>
        public string Corral { get; set; }
        /// <summary>
        /// Enfermeria a la que pertenece el corral
        /// </summary>
        public string Enfermeria { get; set; }
    }
}
