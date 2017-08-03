using System.ComponentModel;

namespace SIE.Services.Info.Info
{
	public class TipoParametroInfo : BitacoraInfo, INotifyPropertyChanged
	{
	    private string descripcion;
		/// <summary> 
		///	TipoParametroID  
		/// </summary> 
		public int TipoParametroID { get; set; }

	    /// <summary> 
	    ///	Descripcion  
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
