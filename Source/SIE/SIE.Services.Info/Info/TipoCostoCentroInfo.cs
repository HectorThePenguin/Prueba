using SIE.Services.Info.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class TipoCostoCentroInfo
    {
        /// <summary>
        /// Identificador de Tipo costo de centro
        /// </summary>
        public int? TipoCostoCentroID { get; set; }
        /// <summary>
        /// Descripcion del Tipo costo centro
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Estatus de el tipo costo de centro
        /// </summary>
        public EstatusEnum Activo { get; set; }
        /// <summary>
        /// Fecha Creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario modifica
        /// </summary>
        public int UsuarioModificacionID { get; set; }
    }
}
