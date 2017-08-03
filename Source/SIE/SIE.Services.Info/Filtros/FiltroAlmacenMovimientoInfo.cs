namespace SIE.Services.Info.Filtros
{
    public class FiltroAlmacenMovimientoInfo
    {
        /// <summary>
        /// Identificador del Almacen
        /// </summary>
        public int AlmacenID { get; set; }

        /// <summary>
        /// Identificador de la Organizacion
        /// </summary>
        public int OrganizacionID { get; set; }

        /// <summary>
        /// Folio del Movimiento
        /// </summary>
        public long FolioMovimiento { get; set; }

        /// <summary>
        /// Tipo del Movimiento
        /// </summary>
        public int TipoMovimientoID { get; set; }

        /// <summary>
        /// Estatus del Movimiento
        /// </summary>
        public int EstatusID { get; set; }
    }
}
