using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class CorteTransferenciaTotalCabezasInfo
    {
        /// <summary>
        /// Total de cabezas 
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Total cabezas por cortar
        /// </summary>
        public int TotalPorCortar { get; set; }

        /// <summary>
        /// Total cabezas cortadas
        /// </summary>
        public int TotalCortadas { get; set; }
    }
}
