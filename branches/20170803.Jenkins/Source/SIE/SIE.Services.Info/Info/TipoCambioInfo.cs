using System;

namespace SIE.Services.Info.Info
{
    public class TipoCambioInfo : BitacoraInfo
    {
        /// <summary>
        /// Identificador del tipo de cambio
        /// </summary>
        public int TipoCambioId { set; get; }
        /// <summary>
        /// Descripcion del tipo de cambio
        /// </summary>
        public string Descripcion { set; get; }
        /// <summary>
        /// Cambio actual
        /// </summary>
        public decimal Cambio { set; get; }
        /// <summary>
        /// Fecha del tipo de cambio
        /// </summary>
        public DateTime Fecha { set; get; }
        /// <summary>
        /// Moneda del Tipo de Cambio
        /// </summary>
        public MonedaInfo Moneda { set; get; }

        /// <summary>
        /// Fecha del tipo de cambio
        /// </summary>
        public DateTime? FechaFiltro { set; get; }
        /// <summary>
        /// Descripcion de la moneda en la bd
        /// </summary>
        public string DescripcionTabla { set; get; }
    }
}
