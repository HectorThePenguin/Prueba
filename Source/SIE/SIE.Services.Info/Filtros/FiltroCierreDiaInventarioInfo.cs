using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIE.Services.Info.Filtros
{
    public class FiltroCierreDiaInventarioInfo
    {
        /// <summary>
        /// Id del Almacen
        /// </summary>
        public int AlmacenID { set; get; }

        /// <summary>
        /// Id del Tipo del Movimiento
        /// </summary>
        public int TipoMovimientoID { set; get; }

        /// <summary>
        /// Id del Tipo del Movimiento
        /// </summary>
        public int Folio { set; get; }
    }
}
