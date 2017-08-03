namespace SIE.Services.Info.Enums
{
    public enum TipoEstatus
    {
        [BLToolkit.Mapping.MapValue(1)] Embarque = 1,
        [BLToolkit.Mapping.MapValue(2)] Muertes = 2,
        [BLToolkit.Mapping.MapValue(3)] SalidasporVentaGanado = 3,
        [BLToolkit.Mapping.MapValue(4)] OrdenSacrificio = 4,
        [BLToolkit.Mapping.MapValue(5)] Distribucióndealimentos = 5,
        [BLToolkit.Mapping.MapValue(6)] Inventario = 6,
        [BLToolkit.Mapping.MapValue(7)] EntradaProducto = 7,
        [BLToolkit.Mapping.MapValue(8)] Pedido = 8,
        [BLToolkit.Mapping.MapValue(9)] Pesaje = 9,
        [BLToolkit.Mapping.MapValue(10)] SolicitudProductos = 10,
        [BLToolkit.Mapping.MapValue(11)] DiferenciaDeInventarios = 11,
        [BLToolkit.Mapping.MapValue(12)] Contratos = 12,
        [BLToolkit.Mapping.MapValue(13)] AutorizacionMateriaPrima = 13
    }
}