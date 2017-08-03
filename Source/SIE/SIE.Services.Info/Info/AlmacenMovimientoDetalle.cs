using System;

namespace SIE.Services.Info.Info
{
    public class AlmacenMovimientoDetalle
    {
        /// <summary>
        /// ID del Almacen Movimiento Detalle
        /// </summary>
        public long AlmacenMovimientoDetalleID { get; set; }
        /// <summary>
        /// ID del Almacen Movimiento
        /// </summary>
        public long AlmacenMovimientoID { get; set; }
        /// <summary>
        /// ID del Almacen Inventario Lote
        /// </summary>
        public int AlmacenInventarioLoteId { get; set; }
        /// <summary>
        /// ID del Contrato
        /// </summary>
        public int ContratoId { get; set; }
        /// <summary>
        /// Numero de piezas
        /// </summary>
        public int Piezas { get; set; }
        /// <summary>
        /// TratamientoID
        /// </summary>
        public int TratamientoID { get; set; }
        /// <summary>
        /// ProductoID
        /// </summary>
        public int ProductoID { get; set; }
        /// <summary>
        /// Precio del producto
        /// </summary>
        public decimal Precio { get; set; }
        /// <summary>
        /// Cantidad de producto
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Importe del producto
        /// </summary>
        public decimal Importe { get; set; }
        /// <summary>
        /// Fecha Creacioon del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario Creacion
        /// </summary>
        public int UsuarioCreacionID { get; set; }
        /// <summary>
        /// Usuario que modifica el registro
        /// </summary>
        public int UsuarioModificacionID { get; set; }


        public TratamientoInfo Tratamiento { get; set; }

        /// <summary>
        /// Entidad del Producto
        /// </summary>
        public ProductoInfo Producto { get; set; }       
    }
}
