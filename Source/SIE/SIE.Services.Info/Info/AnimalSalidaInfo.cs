using System;


namespace SIE.Services.Info.Info
{
    public class AnimalSalidaInfo
    {
        /// <summary>
        ///     Acceso AnimalSalida.
        /// </summary>
        public int AnimalSalidaId { get; set; }

        /// <summary>
        /// Identificador de Animal
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Identificador de lote origen
        /// </summary>
        
        public int LoteId { get; set; }
        /// <summary>
        /// Identificador de la corraleta 
        /// </summary>
        
        public int CorraletaId { get ; set; }

        /// <summary>
        /// Identificador del tipo de movimiento
        /// </summary>
        public int TipoMovimientoId { get; set; }

        /// <summary>
        /// Fecha de salida del animal
        /// </summary>
        public DateTime FechaSalida { get; set; }

        /// <summary>
        /// Estatus del animal
        /// </summary>
        public int Activo { get; set; }

        /// <summary>
        /// Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Identificador del usuario creacion
        /// </summary>
        public int UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Identificador del usuario modificacion
        /// </summary>
        public int UsuarioModificacion { get; set; }

        /// <summary>
        /// Identificador de Corral origen
        /// </summary>

        public int CorralId { get; set; }
    }
}
