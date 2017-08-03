using System.ComponentModel;
namespace SIE.Services.Info.Info
{
	public class ParametroOrganizacionInfo : BitacoraInfo, INotifyPropertyChanged
	{
	    private string valor = string.Empty;
		/// <summary> 
		///	ParametroOrganizacionID  
		/// </summary> 
		public int ParametroOrganizacionID { get; set; }

		/// <summary> 
		///	ParametroID  
		/// </summary> 
		public ParametroInfo Parametro { get; set; }

		/// <summary> 
		///	OrganizacionID  
		/// </summary> 
		public OrganizacionInfo Organizacion { get; set; }

	    /// <summary> 
	    ///	Valor  
	    /// </summary> 
	    public string Valor
	    {
            get { return valor == null ? string.Empty : valor.Trim(); }
	        set
	        {
                string val = value;
                valor = val == null ? val : val.Trim();
	            NotifyPropertyChanged("Valor");
	        }
	    }

	    /// <summary> 
        /// Objeto para utilizar de filtro en la pantalla Parametro Organización
        /// </summary> 
        public OrganizacionInfo OrganizacionFiltro { get; set; }

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
