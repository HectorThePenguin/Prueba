

using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class EntradaGanadoEnfermeriaInfo
    {
        /// <summary>
        /// Animal que entrara a enfermeria
        /// </summary>
        public AnimalInfo Animal { get; set; }
        /// <summary>
        /// Movimiento que se guardara
        /// </summary>
        public AnimalMovimientoInfo Movimiento { get; set; }
        /// <summary>
        /// Deteccion actual
        /// </summary>
        public AnimalDeteccionInfo Deteccion { get; set; }
        /// <summary>
        /// Identificador del usuario
        /// </summary>
        public int UsuarioId { get; set; }
        /// <summary>
        /// Almacen movimiento
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimiento { get; set; }
        /// <summary>
        /// Lista de tratamientos
        /// </summary>
        public List<TratamientoInfo> Tratamientos { get; set; }
        /// <summary>
        /// Lista de problemas
        /// </summary>
        public List<ProblemaInfo> ListaProblemas { get; set; }
        /// <summary>
        /// Lote destino
        /// </summary>
        public LoteInfo LoteDestino { get; set; }
        /// <summary>
        /// Lote origen
        /// </summary>
        public LoteInfo LoteOrigen { get; set; }
        /// <summary>
        /// Resultado del proceso
        /// </summary>
        public bool Resultado { get; set; }
        /// <summary>
        /// Flag para el cambio de sexo
        /// </summary>
        public bool CambiarTipoGanado { get; set; }
        /// <summary>
        /// Flag para el AnimalRecaido
        /// </summary>
        public bool AnimalRecaido { get; set; }
    }
}
