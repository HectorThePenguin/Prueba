using SIE.Services.Info.Reportes;
using System;

namespace SIE.Services.Info.Info
{
    public class DisponibilidadLoteInfo
    {
        /// <summary>
        /// Clave de Lote
        /// </summary>
        public int LoteId { get; set; }
        /// <summary>
        /// Codigo de Lote
        /// </summary>
        public string Lote { get; set; }
        /// <summary>
        /// Cantida de Cabezas
        /// </summary>
        public int Cabezas { get; set; }
        /// <summary>
        /// Fecha de Disponibilidad
        /// </summary>
        public DateTime FechaDisponibilidad { get; set; }
        /// <summary>
        /// Fecha Salida
        /// </summary>
        public DateTime FechaAsignada { get; set; }
        /// <summary>
        /// Codigo de Corral
        /// </summary>
        public string CodigoCorral { get; set; }
        /// <summary>
        /// Peso Sacrificio
        /// </summary>
        public int PesoProyectado { get; set; }
        /// <summary>
        /// Peso Origen
        /// </summary>
        public int PesoOrigen { get; set; }
        /// <summary>
        /// Descripción del Tipo de Ganado del Corral
        /// </summary>
        public string Tipo { get; set; }
        /// <summary>
        /// Dias de Engorda
        /// </summary>
        public int DiasEngorda { get; set; }
        /// <summary>
        /// Dias de Engorda
        /// </summary>
        public bool SumarDias { get; set; }

        /// <summary>
        /// Fecha en el que inició el Lote
        /// </summary>
        public DateTime FechaInicioLote { get; set; }

        /// <summary>
        /// Ganancia Diaria del Lote
        /// </summary>
        public decimal GananciaDiaria { get; set; }

        /// <summary>
        /// Indica si ya se ha realizado la disponibilidad manual del Lote
        /// </summary>
        public bool DisponibilidadManual { get; set; }

        /// <summary>
        /// Campos del reporte proyector
        /// </summary>
        public ReporteProyectorInfo DatosProyector { get; set; }

        /// <summary>
        /// Indica si ya se ha realizado la disponibilidad manual del Lote
        /// </summary>
        public bool Revision { get; set; }

        /// <summary>
        /// Fecha de Disponibilidad
        /// </summary>
        public DateTime FechaDisponibilidadOriginal { get; set; }

    }
}
