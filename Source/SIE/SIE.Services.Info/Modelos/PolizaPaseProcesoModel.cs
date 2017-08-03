using System.Collections.Generic;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class PolizaPaseProcesoModel
    {
        /// <summary>
        /// Producto del que se genera Poliza
        /// </summary>
        public ProductoInfo Producto { get; set; }
        /// <summary>
        /// Pesaje de materia prima a generar factura
        /// </summary>
        public PesajeMateriaPrimaInfo PesajeMateriaPrima { get; set; }
        /// <summary>
        /// Programacion de la cual se genera la poliza
        /// </summary>
        public ProgramacionMateriaPrimaInfo ProgramacionMateriaPrima { get; set; }
        /// <summary>
        /// Flete Interno
        /// </summary>
        public FleteInternoInfo FleteInterno { get; set; }
        /// <summary>
        /// Almacen Inventario Lote
        /// </summary>
        public AlmacenInventarioLoteInfo AlmacenInventarioLote { get; set; }
        /// <summary>
        /// Almacen Movimiento Detalle
        /// </summary>
        public AlmacenMovimientoDetalle AlmacenMovimientoDetalle { get; set; }
        /// <summary>
        /// Proveedor Chofer
        /// </summary>
        public ProveedorChoferInfo ProveedorChofer { get; set; }
        /// <summary>
        /// Proveedor
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }
        /// <summary>
        /// Flete Interno Costo
        /// </summary>
        public FleteInternoCostoInfo FleteInternoCosto { get; set; }
        /// <summary>
        /// Almacen Movimiento
        /// </summary>
        public AlmacenMovimientoInfo AlmacenMovimiento { get; set; }
        /// <summary>
        /// Organizacion
        /// </summary>
        public OrganizacionInfo Organizacion { get; set; }
        /// <summary>
        /// Pedido
        /// </summary>
        public PedidoInfo Pedido { get; set; }
        /// <summary>
        /// Almacen
        /// </summary>
        public AlmacenInfo Almacen { get; set; }
        /// <summary>
        /// Almacen Movimiento Costo
        /// </summary>
        public List<AlmacenMovimientoCostoInfo> ListaAlmacenMovimientoCosto { get; set; }
    }
}
