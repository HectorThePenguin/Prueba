using System;
using System.Collections.Generic;

namespace SIE.Clases
{
    public class ReporteDetalleCorteInfo
    {
        /// <summary>
        /// Total de hembras de la programacion
        /// </summary>
        public int TipoOrganizacion { get; set; }
        /// <summary>
        /// Fecha de la programacion
        /// </summary>
        public DateTime FechaInicialCorte { get; set; }
        /// <summary>
        /// Fecha de la programacion
        /// </summary>
        public DateTime FechaFinalCorte { get; set; }
        /// <summary>
        /// Fecha de la programacion
        /// </summary>
        public int IDUsuario { get; set; }
        /// <summary>
        /// Identificador del informe
        /// </summary>
        public string FormatoId { get; set; }

    }
}
