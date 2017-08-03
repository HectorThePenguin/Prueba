using System.ComponentModel;
using System.Collections.Generic;
namespace SIE.Services.Info.Info
{
    public class EntradaGanadoCostoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private decimal importe;
        private bool tieneCuenta;
        private bool editarTieneCuenta;
        private bool editarIvaRetencion;
        private bool iva;
        private bool retencion;
        private bool costoEmbarque;
        

        /// <summary>
        /// Identificador de la tabla 
        /// </summary>
        public int EntradaGanadoCostoID { get; set; }

        /// <summary>
        /// Identificador de Entrada Ganado Costeo
        /// </summary>
        public int EntradaGanadoCosteoID { get; set; }

        /// <summary>
        /// Costo de la Entrada de Ganado
        /// </summary>
        public CostoInfo Costo { get; set; }

        /// <summary>
        /// Cuenta de Provision del Costo
        /// </summary>
        public string CuentaProvision { get; set; }

        /// <summary>
        /// Indica si el Costo tiene Cuenta, o es por Proveedor
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
        /// Proveedor al que corresponde el Costo
        /// </summary>
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Número de Documento del Costo
        /// </summary>
        public string NumeroDocumento { get; set; }

        /// <summary>
        /// Importe del Costo
        /// </summary>
        public decimal Importe
        {
            get { return importe; }
            set
            {
                if (value != importe)
                {
                    importe = value;
                    NotifyPropertyChanged("Importe");
                }
            }
        }

        public int ProveedorComisionID { get; set; }

        /// <summary>
        /// Indica si el Costo aplica IVA
        /// </summary>
        public bool Iva
        {
            get { return iva; }
            set
            {
                if (value != iva)
                {
                    iva = value;
                    NotifyPropertyChanged("Iva");
                }
            }
        }

        /// <summary>
        /// Indica si el Costo aplica Retención
        /// </summary>
        public bool Retencion
        {
            get { return retencion; }
            set
            {
                if (value != retencion)
                {
                    retencion = value;
                    NotifyPropertyChanged("Retencion");
                }
            }
        }

        /// <summary>
        /// Indica si el Costo viene de Origen
        /// </summary>
        public bool Origen { get; set; }

        /// <summary>
        /// Indica si el registro se puede editar en el Grid Entrada Ganado Costeo
        /// </summary>
        public bool EditarNumeroDocumento { get; set; }

        /// <summary>
        /// Indica si el Costo viene del Embarque, para que solo se puedan editar ciertos campos
        /// </summary>
        public bool CostoEmbarque
        {
            get { return costoEmbarque; }
            set
            {
                if (value != costoEmbarque)
                {
                    costoEmbarque = value;
                    NotifyPropertyChanged("CostoEmbarque");
                }
            }
        }

        /// <summary>
        /// Indica si al Costo se le puede editar la cuenta o el proveedor
        /// </summary>
        public bool EditarCuentaProveedor { get; set; }

        /// <summary>
        /// Indica si al Costo se le puede editar el check de Cuenta
        /// </summary>
        public bool EditarTieneCuenta
        {
            get { return editarTieneCuenta; }
            set
            {
                if (value != editarTieneCuenta)
                {
                    editarTieneCuenta = value;
                    NotifyPropertyChanged("EditarTieneCuenta");
                }
            }
        }

        /// <summary>
        /// Indica si al Costo se le puede editar el check de Iva y Retención
        /// </summary>
        public bool EditarIvaRetencion
        {
            get { return editarIvaRetencion; }
            set
            {
                if (value != editarIvaRetencion)
                {
                    editarIvaRetencion = value;
                    NotifyPropertyChanged("EditarIvaRetencion");
                }
            }
        }

        /// <summary>
        /// Muestra la Descripcion del Tipo de cuenta de Provision
        /// </summary>
        public string DescripcionCuenta { get; set; }

        //public List<ComisionInfo> ListaComisiones { get; set; }
        #region Miembros de INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
