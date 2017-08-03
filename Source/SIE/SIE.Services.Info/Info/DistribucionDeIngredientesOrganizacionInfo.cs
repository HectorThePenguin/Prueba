
using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Annotations;

namespace SIE.Services.Info.Info
{
    public class DistribucionDeIngredientesOrganizacionInfo : INotifyPropertyChanged
    {
        private decimal _costoUnitario;
        private decimal _costoTotal;
        private int _cantidadSurtir;
        private AlmacenInventarioLoteInfo _lote;
        private List<AlmacenInventarioLoteInfo> _lotes;
        private bool _habilitado;
        private OrganizacionInfo _organizacion;
        private int _cantidadExistente;
        private int _cantidadNueva;
        public AlmacenMovimientoInfo AlmaceMovimiento { get; set; }

        public OrganizacionInfo Organizacion
        {
            get
            {
                return _organizacion;
            }
            set
            {
                _organizacion = value;
                NotifyPropertyChanged("Organizacion");
            }
        }

        public List<AlmacenInventarioLoteInfo> LotesOrganizacion
        {
            get
            {
                return _lotes;
            }
            set
            {
                _lotes = value;
                NotifyPropertyChanged("LotesOrganizacion");
                NotifyPropertyChanged("Lote");
            }
        }

        public int CantidadSurtir
        {
            get
            {
                return _cantidadSurtir;
            }
            set
            {
                if (value <= (_cantidadExistente + _cantidadSurtir))
                {
                    _cantidadSurtir = value;
                    _costoTotal = _costoUnitario * _cantidadSurtir;
                }
                _cantidadNueva = value;
                NotifyPropertyChanged("CostoTotal");
            }
        }

        public decimal CostoTotal
        {
            get
            {
                return _costoTotal;
            }
            set
            {
                _costoTotal = value;
            }
        }

        public decimal CostoUnitario
        {
            get
            {
                return _costoUnitario;
            }
            set
            {
                _costoUnitario = value;
                _costoTotal = _costoUnitario*_cantidadSurtir;
                NotifyPropertyChanged("CostoTotal");
            }
        }

        public int CantidadExistente
        {
            get
            {
                return _cantidadExistente;
            }
            set
            {
                _cantidadExistente = value;
            }
        }

        public AlmacenInventarioLoteInfo Lote
        {
            get
            {
                return _lote;
            }
            set
            {
                _lote = value;
                NotifyPropertyChanged("Lote");
            }
        }

        public bool Habilitado
        {
            get
            {
                return _habilitado;
            }
            set
            {
                _habilitado = value;
                NotifyPropertyChanged("Habilitado");
            }
        }

        public int CantidadNueva
        {
            get { return _cantidadNueva; } set { _cantidadNueva = value; }
        }

        public int PremezclaDistribucionID { get; set; }

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
