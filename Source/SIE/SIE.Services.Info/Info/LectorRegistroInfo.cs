using System;
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class LectorRegistroInfo
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int LectorRegistroID { get; set; }
        /// <summary>
        /// Identificar de la organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Seccion
        /// </summary>
        public int Seccion { get; set; }
        /// <summary>
        /// Identificador del lote
        /// </summary>
        public int LoteID { get; set; }
        /// <summary>
        /// Fecha del lector
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Bandera que indica cambio de formula
        /// </summary>
        public bool CambioFormula { get; set; }
        /// <summary>
        /// Cabezas
        /// </summary>
        public int Cabezas { get; set; }
        /// <summary>
        /// Identificador del estado del comedor
        /// </summary>
        public int EstadoComederoID { get; set; }
        /// <summary>
        /// Cantidad original
        /// </summary>
        public decimal CantidadOriginal { get; set; }
        /// <summary>
        /// Cantidad de pedido
        /// </summary>
        public decimal CantidadPedido { get; set; }
        /// <summary>
        /// Detalle del lector del registro
        /// </summary>
        public List<LectorRegistroDetalleInfo> DetalleLector { get; set; }
    }
}
