using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class AplicacionConsumoDetalleModel
    {
        /// <summary>
        /// Formula de Reparto
        /// </summary>
        public ProductoInfo Producto { get; set; }
        /// <summary>
        /// Cantidad de registros con la misma formula
        /// </summary>
        public int CantidadRegistros { get; set; }
        /// <summary>
        /// Cantidad Inventario Actual
        /// </summary>
        public decimal CantidadInventario { get; set; }
        /// <summary>
        /// Cantidad de Reparto
        /// </summary>
        public int CantidadReparto { get; set; }
        /// <summary>
        /// Cantidad de Diferencia
        /// </summary>
        public decimal CantidadDiferencia { get; set; }
    }
}
