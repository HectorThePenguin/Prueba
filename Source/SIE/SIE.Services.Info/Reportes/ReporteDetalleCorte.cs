
using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class ReporteDetalleCorteModel
    {
        public string AreteId { get; set; }
        public string Descripcion { get; set; }
        public string CorralOrigen { get; set; }
        public string CorralDestino { get; set; }
        public int PesoOrigen { get; set; }
        public int PesoCorte { get; set; }
        public decimal Merma { get; set; }
        public decimal Temperatura { get; set; }
        public string TituloReporte { get; set; }
        public string Fecha { get; set; }
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
        /// FolioEntrada Partida
        /// </summary>
        public int FolioEntrada { get; set; }
    }

}