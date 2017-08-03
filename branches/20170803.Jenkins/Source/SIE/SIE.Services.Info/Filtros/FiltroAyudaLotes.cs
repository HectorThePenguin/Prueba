namespace SIE.Services.Info.Filtros
{
    public class FiltroAyudaLotes
    {
        /// <summary>
        /// Lote
        /// </summary>
        public int Lote { get; set; }
        /// <summary>
        /// Clave de la Organizacion
        /// </summary>
        public int OrganizacionID { get; set; }
        /// <summary>
        /// Clave del Almacen
        /// </summary>
        public int AlmacenID { get; set; }
        /// <summary>
        /// Clave del Producto
        /// </summary>
        public int ProductoID { get; set; }
    }
}
