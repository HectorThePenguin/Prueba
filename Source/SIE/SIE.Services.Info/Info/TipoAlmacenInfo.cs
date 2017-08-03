using System.Collections.Generic;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class TipoAlmacenInfo : BitacoraInfo, INotifyPropertyChanged
	{
        private string descripcion;
        private int tipoAmacenID;

        /// <summary> 
        ///	TipoAlmacenID  
        /// </summary> 
        public int TipoAlmacenID
        {
            get { return tipoAmacenID; }
            set
            {
                tipoAmacenID = value;
                NotifyPropertyChanged("TipoAlmacenID");
            }
        }

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

        public List<int> ListaTipoAlmacen { get; set; }

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
