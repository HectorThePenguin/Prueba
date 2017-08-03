using SIE.Services.Info.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class PrecioProductoInfo
    {
        /// <summary>
        /// Producto
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public decimal PrecioMaximo { get; set; }

        /// <summary>
        /// Organizacion
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Estatus
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Identificador del usuario creacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Identificador del usuario modifica
        /// </summary>
        public int UsuarioModificaID { get; set; }

        /// <summary>
        /// Fecha de modificacion
        /// </summary>
        public DateTime FechaModifica { get; set; }
    }
}
