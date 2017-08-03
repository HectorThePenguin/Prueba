using System;

namespace SIE.Services.Info.Info
{
    public class PagoTransferenciaInfo: BitacoraInfo
    {
        public int PagoId { get; set; }
        public int CentroAcopioId { get; set; }
        public string CentroAcopioDescripcion { get; set; }
        public int BancoId { get; set; }
        public string BancoDescripcion { get; set; }
        public long FolioEntrada { get; set; }
        public long ProveedorId { get; set; }
        public string ProveedorDescripcion { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
       
        public string CodigoAutorizacion { get; set; }
        public DateTime FechaPago { get; set; }
        public int UsuarioId { get; set; }
        
    }
}
