using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class RolInfo : BitacoraInfo, INotifyPropertyChanged
    {

        private string descripcion;
        private NivelAlertaInfo nivelAlerta;

        /// <summary> 
        ///	RolID  
        /// </summary> 
        public int RolID { get; set; }

        /// <summary> 
        ///	Descripcion  
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

        /// <summary>
        /// NivelAlerta
        /// </summary>
        public NivelAlertaInfo NivelAlerta
        {                
            get { return nivelAlerta; }
            set
            {
                if (value != nivelAlerta)
                {
                    nivelAlerta = value;
                    NotifyPropertyChanged("NivelAlerta");
                }
            }
        }

        //public bool EstatusRol { get; set; }

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
