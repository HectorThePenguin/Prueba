using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class TipoFleteInfo
    {
        /// <summary>
        /// Identificador del tipo de flete
        /// </summary>
        public int TipoFleteId { get; set; }
        /// <summary>
        /// Descripcion del tipo de flete
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Estatus del tipo de flete
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }
    }
}
