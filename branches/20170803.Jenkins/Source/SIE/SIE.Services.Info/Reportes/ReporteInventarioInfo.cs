using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteInventarioInfo
    {
        private string tipoProceso;
        /// <summary>
        /// Descripcion del Tipo de Ganado
        /// </summary>
        public string TipoGanado { get; set; }
        /// <summary>
        /// Inventario Inicial
        /// </summary>
        public int InventarioInicial { get; set; }
        /// <summary>
        /// Entradas
        /// </summary>
        public int Entradas { get; set; }
        /// <summary>
        /// Sacrificio
        /// </summary>
        public int Sacrificio { get; set; }
        /// <summary>
        /// Ventas
        /// </summary>
        public int Ventas { get; set; }
        /// <summary>
        /// Muertes
        /// </summary>
        public int Muertes { get; set; }
        /// <summary>
        /// Inventario Final
        /// </summary>
        public int InventarioFinal { get; set; }
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
        /// Cadena con formato para mostrado en el reporte de entre fechas
        /// </summary>
        public string CadenaEntreFechas
        {
            get { return "Del " + FechaInicioConFormato + " al " + FechaFinConFormato; }
        }

        /// <summary>
        /// Tipo de proceso del reporte
        /// </summary>
        public string TipoProceso {
            get { return String.Format("\"{0}\"", tipoProceso); }
            set { tipoProceso = value; }
        }
    }
}
