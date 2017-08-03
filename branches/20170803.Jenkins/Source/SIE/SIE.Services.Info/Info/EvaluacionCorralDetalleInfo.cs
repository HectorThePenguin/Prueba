using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class EvaluacionCorralDetalleInfo
    {
        /// <summary>
        /// Acceso OrganizacionID
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Acceso EvaluacionID
        /// </summary>
        public int EvaluacionID { get; set; }


        /// <summary>
        /// Acceso PreguntaID
        /// </summary>
        public int PreguntaID { get; set; }


        /// <summary>
        /// Acceso Respuesta
        /// </summary>
        public string Respuesta { get; set; }


        /// <summary>
        /// Acceso Activo
        /// </summary>
        public Boolean Activo { get; set; }


        /// <summary>
        /// Acceso UsuarioModificacion
        /// </summary>
        public int UsuarioModificacion { get; set; }

        /// <summary>
        /// Acceso FechaModificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Acceso UsuarioCreacion
        /// </summary>
        public int UsuarioCreacion { get; set; }
        
    }
}
