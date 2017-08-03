using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class OrdenSacrificioInfo
    {
        /// <summary>
        /// Identificador de la orden de Sacrificio
        /// </summary>
        public int OrdenSacrificioID { get; set; }
        /// <summary>
        /// Folio de la orden de sacrificio
        /// </summary>
        public int FolioOrdenSacrificio { get; set; }
        /// <summary>
        /// Identificador del estatus
        /// </summary>
        public int EstatusID { get; set; }
        /// <summary>
        /// Usuario que creo la orden
        /// </summary>
        public int UsuarioCreacion { get; set; }
        /// <summary>
        /// Organizacion 
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Observacion de la orden de sacrificio
        /// </summary>
        public string Observacion { get; set; }
        /// <summary>
        /// Fecha Orden
        /// </summary>
        public DateTime FechaOrden { get; set; }
        /// <summary>
        /// Lista con los detalle de la orden
        /// </summary>
        public IList<OrdenSacrificioDetalleInfo> DetalleOrden { get; set; }
        /// <summary>
        /// Verifica si la orden esta activa
        /// </summary>
        public bool Activo { get; set; }
        /// <summary>
        /// Fecha en que se creó el registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha que en sufrió la última modificación el registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }
        /// <summary>
        /// Último usuario que modificó el registro
        /// </summary>
        public int? UsuarioModificacionID { get; set; }

    }
}
