using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class GanadoIntensivoCostoInfo : BitacoraInfo
    {
        /// <summary>
        /// Costos
        /// </summary>
        public CostoInfo Costos { get; set; }

        /// <summary>
        /// Importe
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Fecha Creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
    }
}
