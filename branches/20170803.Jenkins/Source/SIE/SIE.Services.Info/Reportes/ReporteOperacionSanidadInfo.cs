using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteOperacionSanidadInfo
    {
        public string Concepto { get; set; }
        public int Dias13 { get; set; }
        public decimal PorcentajeDias13 { get; set; }
        public int Dias410 { get; set; }
        public decimal PorcentajeDias410 { get; set; }
        public int DiasMas10 { get; set; }
        public decimal PorcentajeDiasMas10 { get; set; }
        public int TotalPeriodo { get; set; }

        /// <summary>
        /// Encabezado
        /// </summary>
        public ReporteEncabezadoInfo Encabezado;
        /// <summary>
        /// Titulo del reporte
        /// </summary>
        public string Titulo { get; set; }
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
                return FechaInicio.ToString("dd/MM/yyyy");

            }

        }
        /// <summary>
        /// Fecha fin con formato
        /// </summary>
        public string FechaFinConFormato
        {
            get
            {
                return FechaFin.ToString("dd/MM/yyyy");

            }

        }

        /// <summary>
        /// Formato para mostrado del periodo en el informe
        /// </summary>
        public string FechaEntreCadenas
        {
            get
            {
                return string.Format("Del {0} al {1}",
                    FechaInicioConFormato,
                    FechaFinConFormato);

            }

        }
    }
}
