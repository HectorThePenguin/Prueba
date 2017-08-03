using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteEstadoComederoInfo
    {
        /// <summary>
        /// Organizacion que se mostrara en el rpt
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Titulo que tendra el RPT
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// rango de fechas en que se genero el RPT
        /// </summary>
        public string RangoFechas { get; set; }


    }
}
