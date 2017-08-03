namespace SIE.Services.Info.Info
{
    public class ContenedorAlmacenMovimientoCierreDia
    {
        public AlmacenInfo Almacen { get; set; }
        public AlmacenMovimientoInfo AlmacenMovimiento { get; set; }
        public AlmacenMovimientoDetalle AlmacenMovimientoDetalle { get; set; }
        public ProductoInfo Producto { get; set; }
        public long FolioAlmacen { get; set; }
    }
}
