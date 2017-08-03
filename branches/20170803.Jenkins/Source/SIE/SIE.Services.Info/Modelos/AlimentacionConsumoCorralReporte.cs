namespace SIE.Services.Info.Modelos
{
    public class AlimentacionConsumoCorralReporte : System.ComponentModel.INotifyPropertyChanged
    {
        public AlimentacionConsumoCorralReporte()
        {
            Detalle = new System.Collections.ObjectModel.ObservableCollection<AlimentacionConsumoCorralDetalle>();
            Detalle.CollectionChanged += Detalle_CollectionChanged;
            Totales = new System.Collections.ObjectModel.ObservableCollection<AlimentacionConsumoCorralTotal>();
            Totales.CollectionChanged += Totales_CollectionChanged;
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void notificarCambioPropiedad(string propiedad)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propiedad));
            }
        }

        void Detalle_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            notificarCambioPropiedad("Detalle");
        }
        void Totales_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            notificarCambioPropiedad("Totales");
        }

        System.Collections.ObjectModel.ObservableCollection<AlimentacionConsumoCorralDetalle> detalle;
        [Atributos.AtributoIgnorarColumnaExcel]
        public System.Collections.ObjectModel.ObservableCollection<AlimentacionConsumoCorralDetalle> Detalle
        {
            get
            {
                return detalle;
            }
            set
            {
                detalle = value;
                notificarCambioPropiedad("Detalle");
            }
        }
        System.Collections.ObjectModel.ObservableCollection<AlimentacionConsumoCorralTotal> totales;
        [Atributos.AtributoIgnorarColumnaExcel]
        public System.Collections.ObjectModel.ObservableCollection<AlimentacionConsumoCorralTotal> Totales
        {
            get
            {
                return totales;
            }
            set
            {
                totales = value;
                notificarCambioPropiedad("Totales");
            }
        }
        string proveedor;
        public string Proveedor
        {
            get
            {
                return proveedor;
            }
            set
            {
                proveedor = value;
                notificarCambioPropiedad("Proveedor");
            }
        }
        string rangoFechas;
        [Atributos.AtributoIgnorarColumnaExcel]
        public string RangoFechas
        {
            get
            {
                return rangoFechas;
            }
            set
            {
                rangoFechas = value;
                notificarCambioPropiedad("RangoFechas");
            }
        }
        string corral;
        public string Corral
        {
            get
            {
                return corral;
            }
            set
            {
                corral = value;
                notificarCambioPropiedad("Corral");
            }
        }
        string tipoGanado;
        public string TipoGanado
        {
            get
            {
                return tipoGanado;
            }
            set
            {
                tipoGanado = value;
                notificarCambioPropiedad("TipoGanado");
            }
        }
        string proceso;
        public string Proceso
        {
            get
            {
                return proceso;
            }
            set
            {
                proceso = value;
                notificarCambioPropiedad("Proceso");
            }
        }

    }
}
