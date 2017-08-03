using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class ProduccionFormulaDetalleInfo
    {
        /// <summary>
        /// Identificador del registro de formula detalle
        /// </summary>
        public int ProduccionFormulaDetalleId { get; set; }

        /// <summary>
        /// Identificador del registro de la formula
        /// </summary>
        public int ProduccionFormulaId { get; set; }

        /// <summary>
        /// Producto que integra la formula
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Cantidad del producto
        /// </summary>
        public decimal CantidadProducto { get; set; }

        /// <summary>
        /// Ingrediente de la formula
        /// </summary>
        public IngredienteInfo Ingrediente { get; set; }

        /// <summary>
        /// Estado del registro
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Fecha en que se creo el registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Fecha en que se modifico el registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que modifico el registro
        /// </summary>
        public int UsuarioModificacionId { get; set; }
        /// <summary>
        /// Division
        /// </summary>
        public string Division { get; set; }

        /// <summary>
        /// Clave de la Organizacion
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Precio Promedio que se le aplico al producto para la poliza de Produccion de Alimento
        /// </summary>
        public decimal PrecioPromedio { get; set; }

        /// <summary>
        /// Lote de donde saldra el Producto
        /// </summary>
        public int AlmacenInventarioLoteID { get; set; }

        /// <summary>
        /// Clave del almacen
        /// </summary>
        public int AlmacenID { get; set; }
    }
}
