using System;

namespace SIE.Services.Info.Info
{
    public class TipoContratoInfo
    {
        /// <summary>
        /// Identificador del tipo de contrato
        /// </summary>
        public int TipoContratoId { get; set; }

        /// <summary>
        /// Descripcion del tipo de contrato
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Estatus del contrato
        /// </summary>
        public Enums.EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionID { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario modificacion del registro
        /// </summary>
        public int UsuarioModificacionID { get; set; }
    }
}
