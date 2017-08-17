﻿using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteEjecutivoResultadoInfo
    {
        /// <summary>
        /// Descripcion del Tipo de Ganado
        /// </summary>
        public string TipoGanado { get; set; }
        /// <summary>
        /// Numero de Cabezas por Tipo de Ganado
        /// </summary>
        public int NumeroCabezas { get; set; }
        /// <summary>
        /// Peso Promedio del Tipo de Ganado
        /// </summary>
        public int PesoPromedio { get; set; }
        /// <summary>
        /// Porcentaje de Merca del Tipo de Ganado
        /// </summary>
        public decimal PorcentajeMerma { get; set; }
        /// <summary>
        /// Costo de Compra por Tipo de Ganado
        /// </summary>
        public decimal CostoCompra { get; set; }
        /// <summary>
        /// Costo Integrado por Tipo de Ganado
        /// </summary>
        public decimal CostoIntegrado { get; set; }
        /// <summary>
        /// Titulo usado para el informe
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

        public string FechaCompuesta
        {
            get { return String.Format("Del {0} al {1}", FechaInicioConFormato, FechaFinConFormato); }
        }

    }
}