

namespace SIE.Services.Info.Modelos
{
    public class PedidoPendienteLoteModel
    {
        /// <summary>
        /// Id del Pedido
        /// </summary>
        public int PedidoID { get; set; }
        /// <summary>
        /// Folio del Pedido
        /// </summary>
        public int FolioPedido { get; set; }

        /// <summary>
        /// Id del Detalle del pedido
        /// </summary>
        public int PedidoDetalleID { get; set; }
        /// <summary>
        /// Id del Producto
        /// </summary>
        public int ProductoID { get; set; }
        /// <summary>
        /// ID del lote de destino
        /// </summary>
        public int InventarioLoteIDDestino { get; set; }
        /// <summary>
        /// id de la Programacion de Materia Prima
        /// </summary>
        public int ProgramacionMateriaPrimaID { get; set; }
        /// <summary>
        /// id del lote de Origen
        /// </summary>
        public int InventarioLoteIDOrigen { get; set; }
        /// <summary>
        /// id del Pesaje de Materia Prima
        /// </summary>
        public int PesajeMateriaPrimaID { get; set; }
        /// <summary>
        /// Estatus del Pesaje
        /// </summary>
        public int EstatusID { get; set; }
        /// <summary>
        /// Estatus del Pesaje
        /// </summary>
        public decimal CantidadProgramada { get; set; }
        /// <summary>
        /// Estatus del Pesaje
        /// </summary>
        public long AlmacenMovimientoOrigenID { get; set; }
        /// <summary>
        /// Cantidad que se ha entregado del Pedido
        /// </summary>
        public int CantidadEntregada { get; set; }
    }
}
