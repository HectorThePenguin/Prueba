using System;

namespace SIE.Services.Info.Info
{
    public class ProgramacionCorteInfo : BitacoraInfo
    {
        /// <summary>
        ///     Acceso a FolioProgramacionID 
        /// </summary>
        public int FolioProgramacionID { get; set; }

        /// <summary>
        ///     Acceso a OrganizacionID
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Acceso al nombre de la organizacion
        /// </summary>
        public string OrganizacionNombre { get; set; }

        /// <summary>
        ///     Acceso a FolioEntradaID
        /// </summary>
        public int FolioEntradaID { get; set; }

        /// <summary>
        ///     Acceso FechaProgramacion
        /// </summary>
        public DateTime FechaProgramacion { get; set; }

        /// <summary>
        ///     Acceso FechaInicioCorte
        /// </summary>
        public DateTime FechaInicioCorte { get; set; }

        /// <summary>
        ///     Acceso FechaFinCorte
        /// </summary>
        public DateTime FechaFinCorte { get; set; }

        /// <summary>
        ///    Acceso FechaCreacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        ///    Acceso UsuarioModificacion
        /// </summary>
        public int UsuarioModificacion { get; set; }

        /// <summary>
        ///    Acceso FechaModificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
		
        /// <summary>
        /// Calcula Merma  
        /// </summary>
        public float Merma { get; set; }

        /// <summary>
        /// Acceso Hembras  
        /// </summary>
        public int Hembras { get; set; }

        /// <summary>
        /// Acceso Machos  
        /// </summary>
        public int Machos { get; set; }

        /// <summary>
        /// Calcula Dias Cortes
        /// </summary>
        public int Dias { get; set; }

        /// <summary>
        /// Cabezas recibidas
        /// </summary>
        public int CabezasRecibidas { get; set; }

        /// <summary>
        /// Acceso a CodigoCorral
        /// </summary>
        public string CodigoCorral { get; set; }

        /// <summary>
        /// Acceso a FechaEntrada
        /// </summary>
        public string FechaEntrada { get; set; }

        /// <summary>
        ///     Acceso a Rechazos 
        /// </summary>
        public int Rechazos { get; set; }

        /// <summary>
        /// Muestra el mensaje del Nivel de Garrapata
        /// </summary>
        public string LeyendaNivelGarrapata { get; set; }
        /// <summary>
        /// Evaluacion de la programacion
        /// </summary>
        public string Evaluacion { get; set; }

    }
}
