using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class AlmacenMovimientoCostoInfo
    {
        private bool tieneCuenta;

        /// <summary>
        /// Identificador del Almacen Movimiento Costo
        /// </summary>
        public int AlmacenMovimientoCostoId { get; set; }

        /// <summary>
        /// Identificador del Almacen Movimiento
        /// </summary>
        public long AlmacenMovimientoId { get; set; }

        /// <summary>
        /// Identificador del Proveedor
        /// </summary>
        public int ProveedorId { get; set; }

        /// <summary>
        /// Identificador del Costo
        /// </summary>
        public int CostoId { get; set; }

        /// <summary>
        /// Importe del costo
        /// </summary>
        public decimal Importe { get; set; }

        /// <summary>
        /// Cantidad del costo
        /// </summary>
        public decimal Cantidad { get; set; }

        /// <summary>
        ///      Indica si el registro  se encuentra Activo
        /// </summary>
        public EstatusEnum Activo { get; set; }

        /// <summary>
        /// Identificador del Usuario Creacion
        /// </summary>
        public int UsuarioCreacionId { get; set; }

        /// <summary>
        /// Descripcion del costo
        /// </summary>
        public string DescripcionCosto { get; set; }

        /// <summary>
        /// Identificador del Proveedor
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Cuenta sap del costo
        /// </summary>
        public CuentaSAPInfo CuentaSap { get; set; }

        /// <summary>
        /// Identificador del Costo
        /// </summary>
        public CostoInfo Costo { get; set; }

        /// <summary>
        /// Identificador del Costo
        /// </summary>
        public bool Iva { get; set; }

        /// <summary>
        /// Identificador del Costo
        /// </summary>
        public bool Retencion { get; set; }

        /// <summary>
        /// Indica si el costo se aplicara a cuenta, de lo contrario se aplica a proveedor
        /// </summary>
        public bool TieneCuenta
        {
            get { return tieneCuenta; }
            set
            {
                if (value != tieneCuenta)
                {
                    tieneCuenta = value;
                    NotifyPropertyChanged("TieneCuenta");
                }
            }
        }

        /// <summary>
        /// Indentificador de contrato
        /// </summary>
        public ContratoInfo Contrato { get; set; }

        /// <summary>
        /// Identifiacador de la cuenta sap
        /// </summary>
        public int CuentaSAPID { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
