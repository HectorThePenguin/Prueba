using System.ComponentModel;
using System.Text;
using SIE.Services.Info.Annotations;

namespace SIE.Services.Info.Info
{
    public class CostoEntradaMateriaPrimaInfo : INotifyPropertyChanged
    {
        private bool tieneCuenta;
        private bool habilitarCheckCuenta;
        private bool editarFlete;
        private bool permitirEliminar;
        private decimal importe;
        private bool iva;
        private bool retencion;
        private bool esEditado;
        

        public CostoEntradaMateriaPrimaInfo()
        {
            habilitarCheckCuenta = true;
            EditarIvaRetencion = true;
            esEditado = true;
        }

        public bool CheckearCuenta
        {
            get
            {
                habilitarCheckCuenta = !EsFlete;
                return habilitarCheckCuenta;
            }
            set { habilitarCheckCuenta = value; }
        }

        public bool EditarImporte
        {
            get
            {
                editarFlete = !EsFlete;
                return editarFlete;
            }
            set { editarFlete = value; }
        }

        public bool PermitirEliminar
        {
            get
            {
                permitirEliminar = !EsFlete;
                return permitirEliminar;
            }
            set { permitirEliminar = value; }
        }

        /// <summary>
        /// Datos del costo
        /// </summary>
        public CostoInfo Costos { get; set; }
        /// <summary>
        /// Etiqueta que se mostrara en el grid de costos
        /// </summary>
        public string EtiquetaCostos {
            get
            {
                if (Costos == null)
                {
                    return string.Empty;
                }
                var etiqueta = new StringBuilder();
                etiqueta.Append(Costos.ClaveContable);
                etiqueta.Append(" ");
                etiqueta.Append(Costos.Descripcion);
                return etiqueta.ToString();
            }
        }
        /// <summary>
        /// Indica si cuenta con cuenta
        /// </summary>
        
        public bool TieneCuenta {
            get
            {
                return tieneCuenta;
            }
            set
            {
                if (value != tieneCuenta)
                {
                    tieneCuenta = value;
                    NotifyPropertyChanged("TieneCuenta");
                }
            }

        }

        public CuentaSAPInfo Cuentas { get; set; }
        /// <summary>
        /// Datos del proveedor
        /// </summary>
        public ProveedorInfo Provedor { get; set; }
        /// <summary>
        /// Importe del gasto
        /// </summary>
        public decimal Importe {
            get
            {
                /*if (EsFlete)
                {
                    if (FleteDetalle != null)
                    {
                        importe = FleteDetalle.Tarifa * KilosOrigen;
                    }
                }*/
                return importe;
            }
            set
            {
                if (value != importe)
                {
                    importe = value;
                    NotifyPropertyChanged("Importe");
                }
            }
        }

       
        /// <summary>
        /// Indica se tiene iva
        /// </summary>
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

        public bool EditarIvaRetencion { get; set; }
        /// <summary>
        /// indica si cuenta con retencion
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
        /// Indica si el costo es de un flete
        /// </summary>
        public bool EsFlete { get; set; }
        /// <summary>
        /// Informacion del flete
        /// </summary>
        public FleteInfo Flete { get; set; }
        /// <summary>
        /// Detalle del flete
        /// </summary>
        public FleteDetalleInfo FleteDetalle { get; set; }

        public decimal KilosEntrada { get; set; }

        public decimal KilosOrigen { get; set; }

        public string CuentaSap { get; set; }

        public string DescripcionCuenta { get; set; }

        public string ProveedoriD { get; set; }

        public string NombrePreveedor { get; set; }

        public string ClaveCosto { get; set; }

        public string DescripcionCosto{ get; set; }

        public string Observaciones { get; set; }

        public long AlmacenMovimientoID { get; set; }

        public bool EsEditado
        {
            get
            {
                return esEditado;
            }
            set
            {
                esEditado = value;
                NotifyPropertyChanged("EsEditado");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
