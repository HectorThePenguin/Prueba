using System;
namespace SIE.Services.Info.Reportes
{
    public class ReporteEstadoComederoCorralesFormulaInfo
    {
        /// <summary>
        /// Fromula que se mostrara en el reporte
        /// </summary>
        public string Formula { get; set; }
        /// <summary>
        /// Corral que se mostrara en el reporte
        /// </summary>
        public string corral { get; set; }
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
