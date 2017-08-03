using System.ComponentModel;
namespace SIE.Services.Info.Info
{
	public class CondicionJaulaInfo : BitacoraInfo, INotifyPropertyChanged
	{
	    private string descripcion;
		/// <summary> 
		///	CondicionJaulaID  
		/// </summary> 
		public int CondicionJaulaID { get; set; }

	    /// <summary> 
	    ///	Descripcion  
	    /// </summary> 
	    public string Descripcion
	    {
            get { return string.IsNullOrWhiteSpace(descripcion) ? string.Empty : descripcion; }
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
 