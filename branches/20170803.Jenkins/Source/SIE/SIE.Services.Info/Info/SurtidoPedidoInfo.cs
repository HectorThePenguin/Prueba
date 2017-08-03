
using System;

namespace SIE.Services.Info.Info
{
    public class SurtidoPedidoInfo
    {
        public int IdentificadorRegistro { get;set; }
        /// <summary>
        /// Identificador del pesaje
        /// </summary>
        public PesajeMateriaPrimaInfo PesajeMateriaPrima { get; set; }
        /// <summary>
        /// Fecha de surtido
        /// </summary>
        public string FechaSurtidoString
        {
            get
            {
                string regreso = String.Empty;

                if (PesajeMateriaPrima.FechaSurtido.Year != 1900)
                    regreso = PesajeMateriaPrima.FechaSurtido.ToShortDateString();
                return regreso;
            }
        }

        
        /// <summary>
        /// Producto del surtido
        /// </summary>
        public ProductoInfo Producto { get; set; }
        /// <summary>
        /// Chofer del pedido
        /// </summary>
        public ChoferInfo Chofer { get; set; }
        /// <summary>
        /// Proveedor del pedido
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }
        /// <summary>
        /// Almacen inventario lote destino
        /// </summary>
        public AlmacenInventarioLoteInfo AlmacenInventarioLote { get; set; }
        /// <summary>
        /// Cantidad solicitada del producto
        /// </summary>
        public decimal CantidadSolicitada { get; set; }
        /// <summary>
        /// Cantidad entregada del producto
        /// </summary>
        public decimal CantidadEntregada { get; set; }
        /// <summary>
        /// Cantidad recibida del producto
        /// </summary>
        public decimal CantidadRecibida { get; set; }
        /// <summary>
        /// Cantidad pendiente del producto
        /// </summary>
        public decimal CantidadPendiente { get; set; }
        /// <summary>
        /// Bandera de seleccion
        /// </summary>
        public bool Seleccionado { get; set; }
        /// <summary>
        /// Pedido asociado
        /// </summary>
        public PedidoInfo Pedido { get; set; }
        /// <summary>
        /// Pedido detalle
        /// </summary>
        public PedidoDetalleInfo PedidoDetalle { get; set; }
        /// <summary>
        /// Programacion de materia prima
        /// </summary>
        public ProgramacionMateriaPrimaInfo ProgramacionMateriaPrima { get; set; }

    }
}
