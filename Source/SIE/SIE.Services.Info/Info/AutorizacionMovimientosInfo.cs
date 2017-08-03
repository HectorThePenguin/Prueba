using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class AutorizacionMovimientosInfo
    {
        /// <summary>
        /// Folio movimiento
        /// </summary>
        public long AutorizacionID { get; set; }
        /// <summary>
        /// Identificador del estatus de la AutorizacionMateriaPrima
        /// </summary>
        public int EstatusID { get; set; }
        /// <summary>
        /// Identificador del estatus de la tabla AlmacenMovimiento
        /// </summary>
        public long AlmacenMovimientoID { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// Identificador del estatus de la tabla AlmacenMovimiento
        /// </summary>
        public int EstatusInventarioID { get; set; }
    }
}
