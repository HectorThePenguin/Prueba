using System.ComponentModel;

namespace SIE.Services.Info.Info
{
	public class TipoProcesoInfo : BitacoraInfo, INotifyPropertyChanged
	{
	    private string descripcion;
		/// <summary> 
		///	TipoProcesoID  
		/// </summary> 
		public int TipoProcesoID { get; set; }

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
