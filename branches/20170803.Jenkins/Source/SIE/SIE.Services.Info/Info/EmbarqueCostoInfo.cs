using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Info
{
    public class EmbarqueCostoInfo
    {
        /// <summary>
        /// Campo que almacena el id del embarque costo
        /// </summary>
        public int EmbarqueCostoID { get; set; }

        /// <summary>
        /// Descripcion del costo aplicado
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Importe del costo.
        /// </summary>
        public decimal? Importe { get; set; }
    }
}
