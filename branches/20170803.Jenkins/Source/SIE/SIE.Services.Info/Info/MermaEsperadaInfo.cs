using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class MermaEsperadaInfo
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int MermaEsperadaID { get; set; }
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

        /// <summary>
        /// Estado del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Estado de la merma
        /// 1 Nuevo, 2 Edicion, 3 Eliminar
        /// </summary>
        public int Nuevo { get; set; }
    }
}
