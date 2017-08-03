using System;
using System.ComponentModel;
using SIE.Services.Info.Info;

namespace SIE.Services.Info.Modelos
{
    public class ConfiguracionReimplanteModel : BitacoraInfo, INotifyPropertyChanged
    {
        private CorralInfo corral;
        private LoteInfo lote;
        private TipoGanadoInfo tipoGanado;
        private int diasEngorda;
        private LoteProyeccionInfo loteProyeccion;
        private DateTime fechaDisponible;
        private int pesoOrigen;
        private int totalDias;
        

        /// <summary>
        /// Propiedad que almacena el Corral
        /// </summary>
        public CorralInfo Corral
        {
            get { return corral; }
            set
            {
                corral = value;
                NotifyPropertyChanged("Corral");
            }
        }
        /// <summary>
        /// Propiedad que almacena los datos del Lote
        /// </summary>
        public LoteInfo Lote
        {
            get { return lote; }
            set
            {
                lote = value; 
                NotifyPropertyChanged("Lote");
            }
        }
        /// <summary>
        /// Propiedad que almacena el Tipo de Ganado
        /// </summary>
        public TipoGanadoInfo TipoGanado
        {
            get { return tipoGanado; }
            set
            {
                tipoGanado = value;
                NotifyPropertyChanged("TipoGanado");
            }
        }
        /// <summary>
        /// Propiedad que almacena los Días de Engorda reales
        /// </summary>
        public int DiasEngorda
        {
            get { return diasEngorda; }
            set
            {
                diasEngorda = value;
                NotifyPropertyChanged("DiasEngorda");
            }
        }
        /// <summary>
        /// Propiedad que almacena la proyeccion del lote
        /// </summary>
        public LoteProyeccionInfo LoteProyeccion
        {
            get { return loteProyeccion; }
            set
            {
                loteProyeccion = value;
                NotifyPropertyChanged("LoteProyeccion");
            }
        }
        /// <summary>
        /// Propiedad que almacena la fecha de sacrificio del lote
        /// </summary>
        public DateTime FechaDisponible
        {
            get { return fechaDisponible; }
            set
            {
                fechaDisponible = value;
                NotifyPropertyChanged("FechaDisponible");
            }
        }
        /// <summary>
        /// Propiedad que almacena el peso promedio de compra de los animales
        /// </summary>
        public int PesoOrigen
        {
            get { return pesoOrigen; }
            set
            {
                pesoOrigen = value;
                NotifyPropertyChanged("PesoOrigen");
            }
        }
        /// <summary>
        /// Propiedad que almacena el total de dias a sacrificio
        /// </summary>
        public int TotalDias
        {
            get { return totalDias; }
            set
            {
                totalDias = value;
                NotifyPropertyChanged("TotalDias");
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
