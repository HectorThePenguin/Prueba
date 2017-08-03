
namespace SIE.Services.Info.Enums
{
    public enum TipoReferenciaAnimalCosto
    {
        Default = 0,           // 0.- Default
        Manejo = 1,            // 1.- Manejo --> AlmacenMovimientoID
        Reparto = 2,           // 2.- Reparto --> RepartoID ConsumoAlimento
        Costeo = 3,            // 3.- Costeo --> EntradaganadoCosteoID
        GastosAlInventario = 4 // 4.- Gastos al Inventario --> GastoInventarioID //4.- Gasto --> Foliador(Gasto al Inventario)
    }
}
