using System;
using System.Collections.Generic;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class SolicitudPremezclaInfo
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int SolicitudPremezclaId { get; set; }

        /// <summary>
        /// Organizacion de la solicitud
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Fecha inicio de la solicitud
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha fin de la solicitud
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Estado de la solicitud
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha en que se creo la solicitud
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo la solicitud
        /// </summary>
        public UsuarioInfo UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha en que se modifico la solicitud
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifica la solicitud
        /// </summary>
        public UsuarioInfo UsuarioModificacion { get; set; }

        public List<SolicitudPremezclaDetalleInfo> ListaSolicitudPremezcla { get; set; } 
    }
}
