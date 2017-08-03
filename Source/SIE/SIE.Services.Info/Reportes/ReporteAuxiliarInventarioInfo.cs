using System.Collections.Generic;
using SIE.Services.Info.Info;
namespace SIE.Services.Info.Reportes
{
    public class ReporteAuxiliarInventarioInfo
    {
        /// <summary>
        /// Movimientos del Animal
        /// </summary>
        public List<AnimalMovimientoInfo> AnimalesMovimiento { get; set; }
        /// <summary>
        /// Entradas de Ganado
        /// </summary>
        public List<EntradaGanadoInfo> EntradasGanado { get; set; }
        /// <summary>
        /// Lotes
        /// </summary>
        public List<LoteInfo> Lotes { get; set; }
        /// <summary>
        /// Tipos de Movimientos de los Animales
        /// </summary>
        public List<TipoMovimientoInfo> TiposMovimiento { get; set; }

        /// <summary>
        /// Interface Salida Animal
        /// </summary>
        public List<InterfaceSalidaAnimalInfo> InterfaceSalidasAnimal { get; set; }

        /// <summary>
        /// Animales
        /// </summary>
        public List<AnimalInfo> Animales { get; set; }
    }
}
