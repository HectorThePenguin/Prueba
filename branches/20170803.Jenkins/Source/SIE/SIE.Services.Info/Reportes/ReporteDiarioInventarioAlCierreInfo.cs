using System;

namespace SIE.Services.Info.Reportes
{
    public class ReporteDiarioInventarioAlCierreInfo
    {
        public int ProductoId { get; set; }
        public string Ingrediente { get; set; }
        public decimal TMExisAlmacenPA { get; set; }
        public decimal TMExisAlmacenMP { get; set; }
        public decimal TMInvTotalPAyMA { get; set; }
        public decimal TMConsumoDia { get; set; }
        public decimal DiasCobertura { get; set; }
        public decimal CapacidadAlamacenajeDias { get; set; }
        public decimal DiasCoberturaFaltante { get; set; }
        public decimal MinimoDiasReorden { get; set; }
        public decimal TMCapacidadAlmacenaje { get; set; }
        public string EstatusReorden { get; set; }
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
        public DateTime Fecha { get; set; }

    }
}
