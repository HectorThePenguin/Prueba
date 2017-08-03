using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteTabularDisponibilidadInfo
    {
        /// <summary>
        /// Codigo del corral
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Codigo del corral
        /// </summary>
        public int LoteID { get; set; }

        /// <summary>
        /// Indica el número de cabezas
        /// </summary>
        public int Cabezas { get; set; }

        /// <summary>
        /// Tipo de Ganado
        /// </summary>
        public String Descripcion { get; set; }

        /// <summary>
        /// Fecha Cierre
        /// </summary>
        public DateTime FechaCierre{ get; set; }

        /// <summary>
        /// Fecha Disponibilidad Proyectada
        /// </summary>
        public DateTime FechaDisponibilidadProyectada { get; set; }

        /// <summary>
        /// Fecha Disponibilidad
        /// </summary>
        public DateTime FechaDisponibilidad { get; set; }

        /// <summary>
        /// Disponibilidad Manual
        /// </summary>
        public int DisponibilidadManual { get; set; }

        /// <summary>
        /// Peso Total Lote
        /// </summary>
        public int PesoTotalLote { get; set; }

        /// <summary>
        /// Peso Promedio
        /// </summary>
        public int PesoPromedio { get; set; }

        /// <summary>
        /// Sexo
        /// </summary>
        public string Sexo { get; set; }
        /// <summary>
        /// Formula Servida
        /// </summary>
        public string FormulaIDServida { get; set; }

        /// <summary>
        /// Titulo usado para el informe
        /// </summary>
        public string Titulo { get; set; }
        /// <summary>
        /// Organizacion del informe
        /// </summary>
        public string Organizacion { get; set; }

        public int Semana { get; set; }

        public DateTime FechaInicioSemana { get; set;  }
    }
}
