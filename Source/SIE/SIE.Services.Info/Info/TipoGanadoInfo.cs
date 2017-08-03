using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TipoGanadoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion;
        private int tipoGanadoID;

        /// <summary>
        /// Identificador de la Tabla
        /// </summary>
        public int TipoGanadoID
        {
            set
            {
                tipoGanadoID = value;
                NotifyPropertyChanged("TipoGanadoID");
            }
            get { return tipoGanadoID; }
        }

        /// <summary>
        /// Descripción del Tipo de Ganado
        /// </summary>
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                string valor = value;
                descripcion = valor == null ? string.Empty : valor.Trim();
                NotifyPropertyChanged("Descripcion");
            }

        }
        /// <summary>
        /// Sexo del Tipo de Ganado
        /// </summary>
        public Enums.Sexo Sexo { set; get; }
        /// <summary>
        /// Peso Minimo del Tipo de Ganado
        /// </summary>
        public decimal PesoMinimo { set; get; }
        /// <summary>
        /// Peso Maximo del Tipo de Ganado
        /// </summary>
        public decimal PesoMaximo { set; get; }

        /// <summary>
        /// Peso de Salida del Tipo de Ganado
        /// </summary>
        public int PesoSalida { set; get; }

        public string PesoMaximoString { get; set; }
        public string PesoMinimoString { get; set; }

        public override string ToString()
        {
            return Descripcion;
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
