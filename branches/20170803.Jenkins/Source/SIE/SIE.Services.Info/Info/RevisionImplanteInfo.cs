
using System;
namespace SIE.Services.Info.Info
{
    /// <summary>
    /// The revision implante info.
    /// </summary>
    public class RevisionImplanteInfo
    {
        /// <summary>
        /// Get or set identificador de la revision implante
        /// </summary>
        public int RevisionImplanteId { get; set; }

        /// <summary>
        /// Gets or sets the corral.
        /// </summary>
        public CorralInfo Corral { get; set; }

        /// <summary>
        /// Gets or sets the animal.
        /// </summary>
        public AnimalInfo Animal { get; set; }

        /// <summary>
        /// Gets or sets the causa.
        /// </summary>
        public CausaRevisionImplanteInfo Causa { get; set; }

        /// <summary>
        /// Gets or sets the lugar validacion.
        /// </summary>
        public AreaRevisionInfo LugarValidacion { get; set; }

        /// <summary>
        /// Gets or sets the lote.
        /// </summary>
        public LoteInfo Lote { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether correcto.
        /// </summary>
        public bool Correcto { get; set; }

        /// <summary>
        /// Gets or sets the fecha.
        /// </summary>
        public DateTime Fecha { get; set; }
    }
}
