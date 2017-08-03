using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ImpresionProgramacionCorteInfo
    {
        /// <summary>
        /// Total de hembras de la programacion
        /// </summary>
        public int TotalHembras { get; set; }
        /// <summary>
        /// Total de machos de la programacion
        /// </summary>
        public int TotalMachos { get; set; }
        /// <summary>
        /// Total de cabezas recibidas de la programacion
        /// </summary>
        public int TotalRecibidas { get; set; }
        /// <summary>
        /// Lista de programacion generada
        /// </summary>
        public IList<ProgramacionCorteInfo> ProgramacionCorte { get; set; }
        /// <summary>
        /// Fecha de la programacion
        /// </summary>
        public DateTime FechaProgramacion { get; set; }
        /// <summary>
        /// Identificador del informe
        /// </summary>
        public string FormatoId { get; set; }

    }
}
