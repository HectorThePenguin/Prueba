﻿using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteEstadoComederosDetalleInfo
    {
        public string Codigo { get; set; }
        public string Lote { get; set; }
        public string TipoGanado { get; set; }
        public int Cabezas { get; set; }
        public int DiasEngorda { get; set; }
        public double PesoProyectado { get; set; }
        public int DiasUltimaFormula { get; set; }
        public decimal Prom5Dias { get; set; }
        public int EstadoComederoID { get; set; }
        public int AMProgramadoHoy { get; set; }
        public string FormulaDescAMProgramadoHoy { get; set; }
        public int PMProgramadoHoy { get; set; }
        public string FormulaDescPMProgramadoHoy { get; set; }
        public int TotalProgramadoHoy { get; set; }
        public int ECRealServidorAyer { get; set; }
        public int AMRealServidorAyer { get; set; }
        public string FormulaDescAMServidaAyer { get; set; }
        public int PMRealServidorAyer { get; set; }
        public string FormulaDescPMServidaAyer { get; set; }
        public int TotalServidoAyer { get; set; }
        public int Kilogramos3 { get; set; }
        public decimal CxC3 { get; set; }
        public int EC3 { get; set; }
        public int Kilogramos4 { get; set; }
        public decimal CxC4 { get; set; }
        public int EC4 { get; set; }
        public int Kilogramos5 { get; set; }
        public decimal CxC5 { get; set; }
        public int EC5 { get; set; }
        public int Kilogramos6 { get; set; }
        public decimal CxC6 { get; set; }
        public int EC6 { get; set; }
        public int Kilogramos7 { get; set; }
        public decimal CxC7 { get; set; }
        public int EC7 { get; set; }
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
