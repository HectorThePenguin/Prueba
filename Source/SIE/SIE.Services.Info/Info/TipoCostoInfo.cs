
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TipoCostoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion;

        /// <summary>
        /// Identificador del tipo de costo
        /// </summary>
        public int TipoCostoID { set; get; }
        /// <summary>
        /// Descripcion del tipo de costo
        /// </summary>
        public string Descripcion
        {
            get { return string.IsNullOrEmpty(descripcion) ? string.Empty : descripcion.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != descripcion)
                {
                    descripcion = value;
                    NotifyPropertyChanged("Descripcion");
                }
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
