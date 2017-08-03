using System.ComponentModel;
using System;

namespace SIE.Services.Info.Info
{
    public class TraspasoMpPaMedInfo : INotifyPropertyChanged
    {
        private TipoAlmacenInfo tipoAlmacenOrigen;
        private TipoAlmacenInfo tipoAlmacenDestino;
        private OrganizacionInfo organizacionOrigen;
        private OrganizacionInfo organizacionDestino;
        private AlmacenInfo almacenOrigen;
        private AlmacenInfo almacenDestino;
        private ProductoInfo productoOrigen;
        private ProductoInfo productoDestino;
        private ProveedorInfo proveedorOrigen;
        private ProveedorInfo proveedorDestino;
        private ContratoInfo contratoOrigen;
        private ContratoInfo contratoDestino;
        private int loteContratoOrigen;
        private int loteContratoDestino;
        private AlmacenInventarioLoteInfo loteMpOrigen;
        private AlmacenInventarioLoteInfo loteMpDestino;
        private decimal cantidadTraspasarOrigen;
        private decimal cantidadTraspasarDestino;
        private AlmacenInventarioInfo almacenInventarioOrigen;
        private AlmacenInventarioInfo almacenInventarioDestino;
        private string justificacionDestino;
        private CuentaSAPInfo cuentaContable;
        private UsuarioInfo usuario;
        private decimal precioTraspasoOrigen;
        private decimal precioTraspasoDestino;
        private decimal importeTraspaso;
        private long folioTraspaso;
        private DateTime fechaTraspaso;
        private int traspasoMateriaPrimaID;
        private bool esCancelacion;
        private long almacenMovimientoID;

        public TipoAlmacenInfo TipoAlmacenOrigen
        {
            get { return tipoAlmacenOrigen; }
            set
            {
                tipoAlmacenOrigen = value;
                NotifyPropertyChanged("TipoAlmacenOrigen");
            }
        }
        public TipoAlmacenInfo TipoAlmacenDestino
        {
            get { return tipoAlmacenDestino; }
            set
            {
                tipoAlmacenDestino = value;
                NotifyPropertyChanged("TipoAlmacenDestino");
            }
        }
        public OrganizacionInfo OrganizacionOrigen
        {
            get { return organizacionOrigen; }
            set
            {
                organizacionOrigen = value;
                NotifyPropertyChanged("OrganizacionOrigen");
            }
        }
        public OrganizacionInfo OrganizacionDestino
        {
            get { return organizacionDestino; }
            set
            {
                organizacionDestino = value;
                NotifyPropertyChanged("OrganizacionDestino");
            }
        }
        public AlmacenInfo AlmacenOrigen
        {
            get { return almacenOrigen; }
            set
            {
                almacenOrigen = value;
                NotifyPropertyChanged("AlmacenOrigen");
            }
        }
        public AlmacenInfo AlmacenDestino
        {
            get { return almacenDestino; }
            set
            {
                almacenDestino = value;
                NotifyPropertyChanged("AlmacenDestino");
            }
        }
        public ProductoInfo ProductoOrigen
        {
            get { return productoOrigen; }
            set
            {
                productoOrigen = value;
                NotifyPropertyChanged("ProductoOrigen");
            }
        }
        public ProductoInfo ProductoDestino
        {
            get { return productoDestino; }
            set
            {
                productoDestino = value;
                NotifyPropertyChanged("ProductoDestino");
            }
        }
        public ProveedorInfo ProveedorOrigen
        {
            get { return proveedorOrigen; }
            set
            {
                proveedorOrigen = value;
                NotifyPropertyChanged("ProveedorOrigen");
            }
        }
        public ProveedorInfo ProveedorDestino
        {
            get { return proveedorDestino; }
            set
            {
                proveedorDestino = value;
                NotifyPropertyChanged("ProveedorDestino");
            }
        }
        public ContratoInfo ContratoOrigen
        {
            get { return contratoOrigen; }
            set
            {
                contratoOrigen = value;
                NotifyPropertyChanged("ContratoOrigen");
            }
        }
        public ContratoInfo ContratoDestino
        {
            get { return contratoDestino; }
            set
            {
                contratoDestino = value;
                NotifyPropertyChanged("ContratoDestino");
            }
        }
        public int LoteContratoOrigen
        {
            get { return loteContratoOrigen; }
            set
            {
                loteContratoOrigen = value;
                NotifyPropertyChanged("LoteContratoOrigen");
            }
        }
        public int LoteContratoDestino
        {
            get { return loteContratoDestino; }
            set
            {
                loteContratoDestino = value;
                NotifyPropertyChanged("LoteContratoDestino");
            }
        }
        public AlmacenInventarioLoteInfo LoteMpOrigen
        {
            get { return loteMpOrigen; }
            set
            {
                loteMpOrigen = value;
                NotifyPropertyChanged("LoteMpOrigen");
            }
        }
        public AlmacenInventarioLoteInfo LoteMpDestino
        {
            get { return loteMpDestino; }
            set
            {
                loteMpDestino = value;
                NotifyPropertyChanged("LoteMpDestino");
            }
        }
        public AlmacenInventarioInfo AlmacenInventarioOrigen
        {
            get { return almacenInventarioOrigen; }
            set
            {
                almacenInventarioOrigen = value;
                NotifyPropertyChanged("AlmacenInventarioOrigen");
            }
        }
        public AlmacenInventarioInfo AlmacenInventarioDestino
        {
            get { return almacenInventarioDestino; }
            set
            {
                almacenInventarioDestino = value;
                NotifyPropertyChanged("AlmacenInventarioDestino");
            }
        }
        public decimal CantidadTraspasarOrigen
        {
            get { return cantidadTraspasarOrigen; }
            set
            {
                cantidadTraspasarOrigen = value;
                NotifyPropertyChanged("CantidadTraspasarOrigen");
            }
        }
        public decimal CantidadTraspasarDestino
        {
            get { return cantidadTraspasarDestino; }
            set
            {
                cantidadTraspasarDestino = value;
                NotifyPropertyChanged("CantidadTraspasarDestino");
            }
        }
        public string JustificacionDestino
        {
            get { return justificacionDestino; }
            set
            {
                justificacionDestino = value;
                NotifyPropertyChanged("JustificacionDestino");
            }
        }
        public CuentaSAPInfo CuentaContable
        {
            get { return cuentaContable; }
            set
            {
                cuentaContable = value;
                NotifyPropertyChanged("CuentaContable");
            }
        }

        public UsuarioInfo Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                NotifyPropertyChanged("Usuario");
            }
        }

        public decimal ImporteTraspaso
        {
            get { return importeTraspaso; }
            set
            {
                importeTraspaso = value;
                NotifyPropertyChanged("ImporteTraspaso");
            }
        }
        public decimal PrecioTraspasoOrigen
        {
            get { return precioTraspasoOrigen; }
            set
            {
                precioTraspasoOrigen = value;
                NotifyPropertyChanged("PrecioTraspasoOrigen");
            }
        }
        public decimal PrecioTraspasoDestino
        {
            get { return precioTraspasoDestino; }
            set
            {
                precioTraspasoDestino = value;
                NotifyPropertyChanged("PrecioTraspasoDestino");
            }
        }
        public long FolioTraspaso
        {
            get { return folioTraspaso; }
            set
            {
                folioTraspaso = value;
                NotifyPropertyChanged("FolioTraspaso");
            }
        }

        public DateTime FechaTraspaso
        {
            get { return fechaTraspaso; }
            set
            {
                fechaTraspaso = value;
                NotifyPropertyChanged("FechaTraspaso");
            }
        }

        public int TraspasoMateriaPrimaID
        {
            get { return traspasoMateriaPrimaID; }
            set
            {
                traspasoMateriaPrimaID = value;
                NotifyPropertyChanged("TraspasoMateriaPrimaID");
            }
        }

        public bool EsCancelacion
        {
            get { return esCancelacion; }
            set
            {
                esCancelacion = value;
                NotifyPropertyChanged("EsCancelacion");
            }
        }

        public long AlmacenMovimientoID
        {
            get { return almacenMovimientoID; }
            set
            {
                almacenMovimientoID = value;
                NotifyPropertyChanged("AlmacenMovimientoID");
            }
        }



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
