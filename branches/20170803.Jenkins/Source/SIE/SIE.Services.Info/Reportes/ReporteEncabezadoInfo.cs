
using System;

namespace SIE.Services.Info.Info
{
    public class ReporteEncabezadoInfo
    {
        /// <summary>
        /// Titulo del reporte con nombre de la empresa
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Titulo del reporte con fechas
        /// </summary>
        public string TituloPeriodo { get; set; }
        /// <summary>
        /// Titulo del reporte
        /// </summary>
        public string TituloReporte { get; set; }        
        /// <summary>
        /// Organizacion del informe
        /// </summary>
        public string Organizacion { get; set; }
        /// <summary>
        /// Fecha Inicio
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha Fin
        /// </summary>
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Fecha inicio con formato
        /// </summary>
        public string FechaInicioConFormato
        {
            get
            {
                return FechaInicio.ToString("yyyy/MM/dd");

            }

        }
        /// <summary>
        /// Fecha fin con formato
        /// </summary>
        public string FechaFinConFormato
        {
            get
            {
                return FechaFin.ToString("yyyy/MM/dd");

            }

        }
    }
}
