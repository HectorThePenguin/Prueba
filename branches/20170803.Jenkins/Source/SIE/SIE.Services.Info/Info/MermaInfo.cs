using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class MermaInfo
    {
        /// <summary>
        /// Merma del registro
        /// </summary>
        public Decimal Merma { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Id de la Organización Origen
        /// </summary>
        public OrganizacionInfo OrganizacionOrigen{ get; set; }

        /// <summary>
        /// Id de la Organización Destino
        /// </summary>
        public OrganizacionInfo OrganizacionDestino{ get; set; }
    }
}
