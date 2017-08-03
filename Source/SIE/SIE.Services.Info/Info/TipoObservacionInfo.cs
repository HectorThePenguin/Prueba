using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("TipoObservacion")]
	public class TipoObservacionInfo : BitacoraInfo, INotifyPropertyChanged
	{
	    private int tipoObservacionID;
	    private string descripcion = string.Empty;
		/// <summary> 
		///	TipoObservacionID  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int TipoObservacionID
        {
            get { return tipoObservacionID; }
		    set
		    {
		        if (value != tipoObservacionID)
		        {
		            tipoObservacionID = value;
                    NotifyPropertyChanged("TipoObservacionID");
		        }
		    }
        }

		/// <summary> 
		///	Descripcion  
		/// </summary> 
        public string Descripcion
        {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                if (value != descripcion)
                {
                    string valor = value;
                    descripcion = valor == null ? valor : valor.Trim();
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
