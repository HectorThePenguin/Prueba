
using System;

namespace SIE.Services.Info.Info
{
    public class EntradaProductoMuestraInfo 
    {
        /// <summary>
        /// Identificador de la muestra
        /// </summary>
        public int EntradaProductoMuestraId { get; set; }

        /// <summary>
        /// Identificador del detalle al que esta ligado la muestra
        /// </summary>
        public int EntradaProductoDetalleId { get; set; }

        /// <summary>
        /// Porcentaje de la muestra
        /// </summary>
        public decimal Porcentaje { get; set; }

        /// <summary>
        /// Descuento que se hizo a la muestra
        /// </summary>
        public decimal Descuento { get; set; }

        /// <summary>
        /// Indica si la muestra es origen o destino.
        /// </summary>
        public Enums.EsOrigenEnum EsOrigen { get; set; }

        /// <summary>
        /// Indica si se rechazo la muestra
        /// </summary>
        public Enums.EstatusEnum Rechazo { get; set; }

        /// <summary>
        /// Indica si esta activa la muestra
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }

        /// <summary>
        /// Usuario que crea el registro
        /// </summary>
        public UsuarioGrupoInfo UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha en que se crea el registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que modifica el registro
        /// </summary>
        public UsuarioInfo UsuarioModificacion { get; set; }

        /// <summary>
        /// Fecha en que se modifica el registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
    }
}
