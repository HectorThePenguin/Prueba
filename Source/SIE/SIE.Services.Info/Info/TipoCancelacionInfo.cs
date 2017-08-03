using SIE.Services.Info.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class TipoCancelacionInfo: BitacoraInfo
    {
        /// <summary>
        /// Identificador del tipo de cancelacion
        /// </summary>
        public int TipoCancelacionId { get; set; }

        /// <summary>
        /// Descripcion del tipo de cancelacion
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Dias permitidos para realizar la cancelacion de un movimiento
        /// </summary>
        public int DiasPermitidos { get; set; }
    }
}
