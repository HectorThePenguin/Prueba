using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TipoEmbarqueInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descrripcion;
        /// <summary>
        ///     Identificador tipo de embarque
        /// </summary>
        public int TipoEmbarqueID { get; set; }

        /// <summary>
        ///     Descipción del tipo de embarque
        /// </summary>
        public string Descripcion
        {
            get { return descrripcion == null ? string.Empty : descrripcion.Trim(); }
            set 
            { 
                string valor = value;
                descrripcion = valor == null ? string.Empty : valor.Trim();
                NotifyPropertyChanged("Descripcion");
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