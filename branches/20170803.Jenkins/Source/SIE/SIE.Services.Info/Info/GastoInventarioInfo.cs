using System;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    /// <summary>
    /// Clase que representa al gasto de inventario
    /// </summary>
    public class GastoInventarioInfo
    {
        public int GastoInventarioID { get; set; }
        public CuentaSAPInfo CuentaSAP { get; set; }
        public CorralInfo Corral { get; set; }
        public ProveedorInfo Proveedor { get; set; }
        public CostoInfo Costo { get; set; }
        public bool TieneCuenta { get; set; }
        public bool IVA { get; set; }
        public bool Retencion { get; set; }
        public string CuentaGasto { get; set; }
        public string CentroCosto { get; set; }
        public decimal Importe { get; set; }
        public string Factura { get; set; }
        public OpcionGastoInventarioEnum TipoGasto { get; set; }
        public int TotalCorrales { get; set; }
        public TipoCostoInfo TipoMovimiento { get; set; }
        public OrganizacionInfo Organizacion { get; set; }
        public string Observaciones { get; set; }
        public long FolioGasto { get; set; }
        public DateTime FechaGasto { get; set; }
        public EstatusEnum Activo { get; set; }
        public int UsuarioId { get; set; }
        public TipoFolio TipoFolio { get; set; }
    }
}
