using System.ComponentModel;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    public class IndicadorInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion;
        private int indicadorId;

        /// <summary>
        /// Id del indicador
        /// </summary>
        [AtributoInicializaPropiedad]
        public int IndicadorId
        {
            get { return indicadorId; }
            set
            {
                indicadorId = value;
                NotifyPropertyChanged("IndicadorId");
            }
        }

        /// <summary>
        /// Descripcion del indicador
        /// </summary>
        public string Descripcion
        {
            get { return descripcion == null ? string.Empty : descripcion.Trim(); }
            set
            {
                descripcion = value;
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
