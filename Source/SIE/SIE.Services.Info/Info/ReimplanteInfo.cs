using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class ReimplanteInfo
    {
        /// <summary>
        ///     Identificador AnimalID .
        /// </summary>
        public AnimalInfo Animal { get; set; }

        /// <summary>
        ///     Identificador Lote .
        /// </summary>
        public LoteInfo Lote { get; set; }

        /// <summary>
        ///     Identificador AnimalMovimiento .
        /// </summary>
        public AnimalMovimientoInfo AnimalMovimiento { get; set; }

        /// <summary>
        ///     Identificador NumeroReimplante .
        /// </summary>
        public int NumeroReimplante { get; set; }

        /// <summary>
        ///     Identificador Corral .
        /// </summary>
        public CorralInfo Corral { get; set; }

        /// <summary>
        ///     Identificador PesoCorte .
        /// </summary>
        public int PesoCorte { get; set; }

        /// <summary>
        ///     Identificador FolioProgramacionReimplanteID .
        /// </summary>
        public int FolioProgramacionReimplanteID { get; set; }

        /// <summary>
        ///     Identificador LoteReimplanteID .
        /// </summary>
        public int LoteReimplanteID { get; set; }

 		/// <summary>
        ///     Identificador TipoOrigen .
        /// </summary>
        public int TipoOrigen { get; set; }

        /// <summary>
        /// Fecha de reimplante
        /// </summary>
        public DateTime FechaReimplante { get; set; }
    }
}
