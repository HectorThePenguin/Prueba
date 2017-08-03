using System;
using System.Collections;
using System.Collections.Generic;
using SIE.Services.Info.Enums;
namespace SIE.Services.Info.Info
{
    public class SupervisionDetectoresInfo : BitacoraInfo
    {
        /// <summary>
        /// Idenetificador de la supervision
        /// </summary>
        public int SupervisionDetectoresId { get; set; }
        /// <summary>
        /// Organizacion de la supervision
        /// </summary>
	    public int OrganizacionId { get; set; }
        /// <summary>
        /// Identificador del operador supervisado
        /// </summary>
	    public int OperadorId { get; set; }
        /// <summary>
        /// Fecha de realizacion de la supervision
        /// </summary>
	    public DateTime FechaSupervision { get; set; }
        /// <summary>
        /// Fecha de supervision en formato cadena
        /// </summary>
        public string FechaSupervisionString
        {
            get { return FechaSupervision.ToShortDateString(); }
        }

        /// <summary>
        /// Criterio establecido resultado de la supervision
        /// </summary>
	    public int CriterioSupervisionId { get; set; }
        /// <summary>
        /// Observaciones de la supervision
        /// </summary>
	    public string Observaciones { get; set; }
        
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
	    public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
	    public int UsuarioCreacionId { get; set; }
        /// <summary>
        /// Respuestas a la supervision
        /// </summary>
        public IList<SupervisionDetectoresRespuestaInfo> Respuestas { get; set; }
        /// <summary>
        /// Preguntas de la supervision
        /// </summary>
        public IList<PreguntaInfo> Preguntas { get; set; }
    }
}
