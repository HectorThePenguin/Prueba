using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class SolicitudPremezclaDetalleInfo
    {
        /// <summary>
        /// Identificador de la solicitud detalle
        /// </summary>
        public int SolicitudPremezclaDetalleId { get; set; }

        /// <summary>
        /// Identificador de la solicitud de premezcla
        /// </summary>
        public int SolicitudPremezclaId { get; set; }

        /// <summary>
        /// Fecha de llegada para la solicitud
        /// </summary>
        public DateTime FechaLlegada { get; set; }

        /// <summary>
        /// Premezcla de la solicitud
        /// </summary>
        public PremezclaInfo Premezcla { get; set; }

        /// <summary>
        /// Cantidad solicitada
        /// </summary>
        public int CantidadSolicitada { get; set; }

        /// <summary>
        /// Estado del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que crea el registro
        /// </summary>
        public UsuarioInfo UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifica el registro
        /// </summary>
        public UsuarioInfo UsuarioModificacion { get; set; }
    }
}
