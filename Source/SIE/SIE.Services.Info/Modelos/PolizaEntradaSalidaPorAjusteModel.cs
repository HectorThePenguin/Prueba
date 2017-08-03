using SIE.Services.Info.Enums;
using System;

namespace SIE.Services.Info.Modelos
{
    public class PolizaEntradaSalidaPorAjusteModel
    {
        /// <summary>
        /// Tipo de Ajuste
        /// </summary>
        public TipoAjusteEnum TipoAjuste { get; set; }
        /// <summary>
        /// Clave del Producto
        /// </summary>
        public int ProductoID { get; set; }
        /// <summary>
        /// Cantidad
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Precio
        /// </summary>
        public decimal Precio { get; set; }
        /// <summary>
        /// Importe
        /// </summary>
        public decimal Importe { get; set; }
        /// <summary>
        /// Cantidad en Inventario Fisico
        /// </summary>
        public decimal CantidadInventarioFisico { get; set; }
        /// <summary>
        /// Precio de Inventario fisico
        /// </summary>
        public decimal PrecioInventarioFisico { get; set; }
        /// <summary>
        /// Cantidad de Inventario Teorico
        /// </summary>
        public decimal CantidadInventarioTeorico { get; set; }
        /// <summary>
        /// Precio de Inventario Teorico
        /// </summary>
        public decimal PrecioInventarioTeorico { get; set; }
        /// <summary>
        /// Clave de Almacen de Inventario
        /// </summary>
        public int AlmacenInventarioID { get; set; }
        /// <summary>
        /// Clave del Almacen Movimiento Detalle
        /// </summary>
        public long AlmacenMovimientoDetalleID { get; set; }

        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }

        /// <summary>
        /// Folio del movimiento
        /// </summary>
        public long FolioMovimiento { get; set; }

        /// <summary>
        /// Fecha en que se registro el movimiento
        /// </summary>
        public DateTime FechaMovimiento { get; set; }

        /// <summary>
        /// Lote del Producto
        /// </summary>
        public int Lote { get; set; }
    }
}
