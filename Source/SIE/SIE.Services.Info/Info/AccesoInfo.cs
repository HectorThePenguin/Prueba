using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class AccesoInfo : INotifyPropertyChanged
    {
        private int accesoId;
        /// <summary>
        ///     Identificador Acceso .
        /// </summary>
        public int AccesoId
        {
            get { return accesoId; }
            set
            {
                if (value != accesoId)
                {
                    accesoId = value;
                    NotifyPropertyChanged("AccesoId");
                }
            }
        }

        /// <summary>
        ///     Acceso Description.
        /// </summary>
        public string Descripcion { get; set; }

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