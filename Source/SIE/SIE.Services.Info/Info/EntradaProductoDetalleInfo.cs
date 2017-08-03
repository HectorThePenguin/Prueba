
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class EntradaProductoDetalleInfo
    {
        /// <summary>
        /// Identificador del estadus producto detalle
        /// </summary>
        public int EntradaProductoDetalleId { get; set; }

        /// <summary>
        /// Identificador de la entrada producto a la que esta ligada
        /// </summary>
        public int EntradaProductoId { get; set; }

        /// <summary>
        /// Identificador del indicador que tiene asignado el detalle
        /// </summary>
        public IndicadorInfo Indicador { get; set; }

        /// <summary>
        /// Indica si esta activo o no
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }

        /// <summary>
        /// Obtiene las muestras que contiene el Indicador
        /// </summary>
        public List<EntradaProductoMuestraInfo> ProductoMuestras { get; set; }
    }
}
