
using System.Collections.Generic;

namespace SIE.Services.Info.Info
{
    public class ParametrosDetallePedidoInfo
    {
        /// <summary>
        /// Identificador del detalle del pedido
        /// </summary>
        public int PedidoDetalleId { get; set; }
        /// <summary>
        /// Descripcion del producto
        /// </summary>
        /// 
        public ProductoInfo Producto { get; set; }
        /// <summary>
        /// Cantidad Solicitada
        /// </summary>
        public decimal CantidadSolicitada { get; set; }
        /// <summary>
        /// Cantidad Entregada
        /// </summary>
        public decimal CantidadEntregada { get; set; }
        /// <summary>
        /// Cantidad Pendiente
        /// </summary>
        public decimal CantidadPendiente { get; set; }
        /// <summary>
        /// Cantidad programada para el pedido detalle
        /// </summary>
        public decimal CantidadProgramada { get; set; }
        /// <summary>
        /// Lote a donde va el producto
        /// </summary> 
        public AlmacenInventarioLoteInfo LoteProceso { get; set; }
        /// <summary>
        /// Programacion materia prima ingo correspondiente al detalle
        /// </summary>
        public List<ProgramacionMateriaPrimaInfo> ProgramacionMateriaPrima { get; set; }
        /// <summary>
        /// Indica si la edicion aparecera como editable en el grid
        /// </summary>
        public bool Editable { get; set; }
    }
}
