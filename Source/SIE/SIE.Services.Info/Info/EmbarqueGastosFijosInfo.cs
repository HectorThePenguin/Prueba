using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class EmbarqueGastosFijosInfo
    {
        /// <summary>
        /// Id del gasto fijo de acuerdo al embarque.
        /// </summary>
        public int EmbarqueGastosFijosID { get; set; }

        /// <summary>
        /// Importe perteneciente al gasto fijo.
        /// </summary>
        public decimal? Importe { get; set; }
    }
}
