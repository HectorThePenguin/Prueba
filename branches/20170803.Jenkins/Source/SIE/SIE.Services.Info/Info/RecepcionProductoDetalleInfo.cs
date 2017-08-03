using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class RecepcionProductoDetalleInfo : IEquatable<RecepcionProductoDetalleInfo>, IComparable<RecepcionProductoDetalleInfo>
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public int RecepcionProductoDetalleId { get; set; }

        /// <summary>
        /// identificador del registro de la recepcion
        /// </summary>
        public int RecepcionProductoId { get; set; }

        /// <summary>
        /// Producto del registro
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Cantidad que se recibe del producto
        /// </summary>
        public decimal Cantidad { get; set; }

        /// <summary>
        /// Precio promedio del producto
        /// </summary>
        public decimal PrecioPromedio { get; set; }

        /// <summary>
        /// Importe del producto
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Estado del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha creacion  del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public UsuarioInfo UsuarioCreacion { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifica el registro
        /// </summary>
        public UsuarioInfo UsuarioModificacion { get; set; }

        public bool Equals(RecepcionProductoDetalleInfo other)
        {
            return this.Producto.ProductoId == other.Producto.ProductoId;
        }

        public int CompareTo(RecepcionProductoDetalleInfo other)
        {
            return this.Producto.ProductoId - other.Producto.ProductoId;
        }
    }
}
