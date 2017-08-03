using System;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Reportes
{
    public class AuxiliarDeInventarioInfo
    {        
        /// <summary>
        /// Fecha de Lledada
        /// </summary>
        public string FechaLlegada { get; set; }
        /// <summary>
        /// Fecha de Corte
        /// </summary>
        public string FechaCorte { get; set; }
        /// <summary>
        /// Tipo de Movimiento
        /// </summary>
        public string ClaveCodigo { get; set; }
        /// <summary>
        /// Corral Origen
        /// </summary>
        public string CorralOrigen { get; set; }
        /// <summary>
        /// Arete
        /// </summary>
        public string Arete { get; set; }
        /// <summary>
        /// Folio de Entrada
        /// </summary>
        public long Partida { get; set; }
        /// <summary>
        /// Dias que paso en Engorda
        /// </summary>
        public int DiasEngorda { get; set; }
        /// <summary>
        /// Indica si proviene de una entrada
        /// </summary>
        public int Entradas { get; set; }
        /// <summary>
        /// Indica si proviene de una Salida
        /// </summary>
        public int Salidas { get; set; }

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
        /// Fecha de Lledada
        /// </summary>
        public DateTime FechaMovimiento { get; set; }

    }
}
