
namespace SIE.Services.Info.Info
{
    public class ParametroRecepcionMateriaPrimaLote
    {
        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int ProductoId { get; set; }
        /// <summary>
        /// Tipo Almacen que esta seleccionado
        /// </summary>
        public string TipoAlmacen { get; set; }
        /// <summary>
        /// Lote tecleado
        /// </summary>
        public int Lote { get; set; }
    }
}
