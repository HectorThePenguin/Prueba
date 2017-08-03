using System;

namespace SIE.Services.Info.Info
{
    public class ProveedorChoferInfo
    {
        /// <summary>
        /// Identificador del proveedor chofer
        /// </summary>
        public int ProveedorChoferID { get; set; }

        /// <summary>
        /// Proveedor del chofer
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Chofer
        /// </summary>
        public ChoferInfo Chofer { get; set; }

        /// <summary>
        /// Estatus del registro
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que crea el registro
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifica el registro
        /// </summary>
        public int? UsuarioModificacionID { get; set; }
    }
}
