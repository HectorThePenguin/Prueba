using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class SupervisionDetectoresRespuestaInfo
    {
        /// <summary>
        /// Id de la supervision
        /// </summary>
        public int SupervisionDetectoresId { get; set; }
        /// <summary>
        /// Identificador del detalle de la supervision
        /// </summary>
        public int SupervisionDetectoresDetalleId { get; set; }
        /// <summary>
        /// Identificador de la pregunta
        /// </summary>
        public int PreguntaId { get; set; }
        /// <summary>
        /// Respuesta a la pregunta
        /// </summary>
        public int Respuesta { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }
        /// <summary>
        /// Pregunta asociada a la Respuesta
        /// </summary>
        public PreguntaInfo Pregunta { get; set; }
    }
}
