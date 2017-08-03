namespace SIE.Services.Info.Info
{
    public class TratamientoAplicadoInfo 
    {
        /// <summary>
        /// ID del Animal Movimiento
        /// </summary>
        public long AnimalMovimientoID { get; set; }

        /// <summary>
        /// Entidad del Producto
        /// </summary>
        public ProductoInfo Producto { get; set; }

        /// <summary>
        /// Cantidad de producto
        /// </summary>
        public decimal Cantidad { get; set; }

        /// <summary>
        /// Importe del producto
        /// </summary>
        public decimal Importe { get; set; }

    }
}
