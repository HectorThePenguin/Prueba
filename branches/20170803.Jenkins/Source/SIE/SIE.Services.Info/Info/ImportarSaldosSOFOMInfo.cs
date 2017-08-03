using System;

namespace SIE.Services.Info.Info
{
    public class ImportarSaldosSOFOMInfo
    {
        public int CreditoID { get; set; }
        public string Nombre { get; set; }

        public string FechaAlta { get; set; }
        public string FechaVencimiento { get; set; }
        public double Saldo { get; set; }
        public TipoCreditoInfo TipoCredito { get; set; }
        public int UsuarioCreacionID { get; set; }
        public int UsuarioModificacionID { get; set; }

        public bool Existe { get; set; }
        public string Proveedor { get; set; }
		public string Centro { get; set; }
        public string Ganadera { get; set; }  		
    }
}