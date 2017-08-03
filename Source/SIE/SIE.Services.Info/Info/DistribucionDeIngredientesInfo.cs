using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SIE.Services.Info.Annotations;

namespace SIE.Services.Info.Info
{
    public class DistribucionDeIngredientesInfo:INotifyPropertyChanged
    {
        private List<DistribucionDeIngredientesOrganizacionInfo> _listaOrganizaciones ;
        private int _cantidadTotal;


        public List<PremezclaDistribucionCostoInfo> ListaPremezclaDistribucionCosto { get; set; }

        public int PremezclaDistribucionID { get; set; }

        public DateTime FechaEntrada { get; set; }

        public long AlmaceMovimientoID { get; set; }

        /// <summary>
        /// Producto de l
        /// </summary>
        public ProductoInfo Producto { get; set; }

        public ProveedorInfo Proveedor { get; set; }

        public List<DistribucionDeIngredientesOrganizacionInfo> ListaOrganizaciones
        {
            get
            {
                return _listaOrganizaciones;
            }
            set
            {
                _listaOrganizaciones = value;
                _cantidadTotal = _listaOrganizaciones.Sum(org => org.CantidadSurtir);
                NotifyPropertyChanged("CantidadTotal");
            }
        }

        public int CantidadExistente { get; set; }

        public decimal CostoUnitario { get; set; }

        public int CantidadTotal { get { return _cantidadTotal; } set { _cantidadTotal = value; } }

        public decimal CostoTotal { get; set; }

        public int UsuarioId { get; set; }

        public int Iva { get; set; }

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
