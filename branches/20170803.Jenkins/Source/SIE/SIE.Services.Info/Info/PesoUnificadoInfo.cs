using System.ComponentModel;
namespace SIE.Services.Info.Info
{
    public class PesoUnificadoInfo : INotifyPropertyChanged
    {
        private decimal pesoOrigen;
        public EntradaGanadoInfo EntradaGanado { get; set; }

        public decimal PesoOrigen
        {
            get { return pesoOrigen; }
            set
            {
                pesoOrigen = value;
                NotifyPropertyChanged("PesoOrigen");
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
