using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class ListaPreciosCentrosInfo : INotifyPropertyChanged
    {

        public int OrganizacionId { get; set; }
        public string Organizacion { get; set; }
        public int SociedadId { get; set; }
        public int ZonaId { get; set; }

        public int MachoPesadoId { get; set; }
        public int ToreteId { get; set; }
        public int BecerroLigeroId { get; set; }
        public int BecerroId { get; set; }
        public int VaquillaTipo2Id { get; set; }
        public int HembraPesadaId { get; set; }
        public int VaquillaId { get; set; }
        public int BecerraId { get; set; }
        public int BecerraLigeraId { get; set; }
        public int ToretePesadoId { get; set; }

        private double _machoPesado;
        private double _torete;
        private double _becerroLigero;
        private double _becerro;
        private double _vaquillaTipo2;
        private double _hembraPesada;
        private double _vaquilla;
        private double _becerra;
        private double _becerraLigera;
        private double _toretePesado;

        private double _pesoPromedioMachoPesado;
        private double _pesoPromedioTorete;
        private double _pesoPromedioBecerroLigero;
        private double _pesoPromedioBecerro;
        private double _pesoPromedioVaquillaTipo2;
        private double _pesoPromedioHembraPesada;
        private double _pesoPromedioVaquilla;
        private double _pesoPromedioBecerra;
        private double _pesoPromedioBecerraLigera;
        private double _pesoPromedioToretePesado;

        public double MachoPesado
        {
            get { return _machoPesado; }
            set
            {
                _machoPesado = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("MachoPesado");
            }
        }

        public double PesoPromedioMachoPesado
        {
            get { return _pesoPromedioMachoPesado; }
            set
            {
                _pesoPromedioMachoPesado = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioMachoPesado");
            }
        }

        public double Torete
        {
            get { return _torete; }
            set
            {
                _torete = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("Torete");
            }
        }

        public double PesoPromedioTorete
        {
            get { return _pesoPromedioTorete; }
            set
            {
                _pesoPromedioTorete = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioTorete");
            }
        }

        public double BecerroLigero
        {
            get { return _becerroLigero; }
            set
            {
                _becerroLigero = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("BecerroLigero");
            }
        }

        public double PesoPromedioBecerroLigero
        {
            get { return _pesoPromedioBecerroLigero; }
            set
            {
                _pesoPromedioBecerroLigero = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioBecerroLigero");
            }
        }

        public double Becerro
        {
            get { return _becerro; }
            set
            {
                _becerro = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("Becerro");
            }
        }

        public double PesoPromedioBecerro
        {
            get { return _pesoPromedioBecerro; }
            set
            {
                _pesoPromedioBecerro = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioBecerro");
            }
        }

        public double VaquillaTipo2
        {
            get { return _vaquillaTipo2; }
            set
            {
                _vaquillaTipo2 = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("VaquillaTipo2");
            }
        }

        public double PesoPromedioVaquillaTipo2
        {
            get { return _pesoPromedioVaquillaTipo2; }
            set
            {
                _pesoPromedioVaquillaTipo2 = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioVaquillaTipo2");
            }
        }

        public double HembraPesada
        {
            get { return _hembraPesada; }
            set
            {
                _hembraPesada = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("HembraPesada");
            }
        }

        public double PesoPromedioHembraPesada
        {
            get { return _pesoPromedioHembraPesada; }
            set
            {
                _pesoPromedioHembraPesada = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioHembraPesada");
            }
        }

        public double Vaquilla
        {
            get { return _vaquilla; }
            set
            {
                _vaquilla = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("Vaquilla");
            }
        }

        public double PesoPromedioVaquilla
        {
            get { return _pesoPromedioVaquilla; }
            set
            {
                _pesoPromedioVaquilla = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioVaquilla");
            }
        }

        public double Becerra
        {
            get { return _becerra; }
            set
            {
                _becerra = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("Becerra");
            }
        }

        public double PesoPromedioBecerra
        {
            get { return _pesoPromedioBecerra; }
            set
            {
                _pesoPromedioBecerra = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioBecerra");
            }
        }

        public double BecerraLigera
        {
            get { return _becerraLigera; }
            set
            {
                _becerraLigera = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("BecerraLigera");
            }
        }

        public double PesoPromedioBecerraLigera
        {
            get { return _pesoPromedioBecerraLigera; }
            set
            {
                _pesoPromedioBecerraLigera = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioBecerraLigera");
            }
        }

        public double ToretePesado
        {
            get { return _toretePesado; }
            set
            {
                _toretePesado = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("ToretePesado");
            }
        }

        public double PesoPromedioToretePesado
        {
            get { return _pesoPromedioToretePesado; }
            set
            {
                _pesoPromedioToretePesado = value.ToString().Length > 0 ? value : 0;
                NotifyPropertyChanged("PesoPromedioToretePesado");
            }
        }

        public OrganizacionInfo CentroAcopio { get; set; }
        public ZonaInfo Zona { get; set; }
                
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
