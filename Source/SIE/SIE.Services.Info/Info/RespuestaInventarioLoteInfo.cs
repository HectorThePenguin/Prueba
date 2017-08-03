
namespace SIE.Services.Info.Info
{
    public class RespuestaInventarioLoteInfo
    {
        /// <summary>
        /// Identificador del almacen inventeio lote
        /// </summary>
        public int AlmacenInventarioLoteId { get; set; }
        /// <summary>
        /// Lote asignado al almacen inventario lote
        /// </summary>
        public int Lote { set; get; }
        /// <summary>
        /// Cantidad que tiene el almacen
        /// </summary>
        public decimal Cantidad { set; get; }
        /// <summary>
        /// Identificador del almacen
        /// </summary>
        public int AlmacenId { set; get; }
        /// <summary>
        /// Codigo del almacen
        /// </summary>
        public string CodigoAlmacen { get; set; }
    }
}
