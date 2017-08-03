using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteEstadoComederoCorralesEstadoInfo
    {
        /// <summary>
        /// Id del estado comedero que se mostrara en el RPT
        /// </summary>
        public int IdEstadoComedero { get; set; }
        /// <summary>
        /// Descripcion del estado comedero que se mostrara en el RPT
        /// </summary>
        public string DescripcionComedero { get; set; }
        /// <summary>
        /// Corral que se mostrara en el RPT
        /// </summary>
        public int Corrales { get; set; }
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
