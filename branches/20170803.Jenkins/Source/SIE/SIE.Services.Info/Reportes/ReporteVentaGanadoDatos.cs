using System.Collections.Generic;

namespace SIE.Services.Info.Reportes
{
    public class ReporteVentaGanadoDatos
    {
        /// <summary>
        /// Lista de tratamientos aplicados
        /// al animal
        /// </summary>
        public List<ReporteVentaGanadoTratamiento> Tratamientos { get; set; }
        /// <summary>
        /// Lista con los movimientos de salida
        /// </summary>
        public List<ReporteVentaGanadoMovimientosSalida> MovimientosSalida { get; set; }
        /// <summary>
        /// Lista con los movimientos de produccion
        /// </summary>
        public List<ReporteVentaGanadoMovimientosProduccion> MovimientosProduccion { get; set; }
        /// <summary>
        /// Lista con los folios del animal
        /// </summary>
        public List<ReporteVentaGanadoFolio> Folios { get; set; }
        /// <summary>
        /// Lista con los animales
        /// </summary>
        public List<ReporteVentaGanadoAnimal> Animales { get; set; }
    }
}
