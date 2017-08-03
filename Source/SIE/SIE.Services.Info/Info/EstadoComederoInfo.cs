using System.ComponentModel;
using SIE.Services.Info.Enums;
namespace SIE.Services.Info.Info
{
	public class EstadoComederoInfo : BitacoraInfo, INotifyPropertyChanged
	{
	    private string descripcion = string.Empty;
	    private string descripcionCorta = string.Empty;
		/// <summary> 
		///	EstadoComederoID  
		/// </summary> 
		public int EstadoComederoID { get; set; }

	    /// <summary> 
	    ///	Descripcion  
	    /// </summary> 
	    public string Descripcion
	    {
            get { return descripcion == null ? null : descripcion.Trim(); }
            set
            {
                string valor = value;
                descripcion = valor == null ? valor : valor.Trim();
                NotifyPropertyChanged("Descripcion");
            }
	    }

	    /// <summary> 
	    ///	DescripcionCorta  
	    /// </summary> 
	    public string DescripcionCorta
	    {
	        get { return descripcionCorta == null ? null : descripcionCorta.Trim(); }
	        set
	        {
	            string valor = value;
	            descripcionCorta = valor == null ? valor : valor.Trim();
	            NotifyPropertyChanged("DescripcionCorta");
	        }
	    }

	    /// <summary> 
		///	NoServir  
		/// </summary> 
		public bool NoServir { get; set; }

		/// <summary> 
		///	AjusteBase  
		/// </summary> 
		public decimal AjusteBase { get; set; }

		/// <summary> 
		///	Tendencia  
		/// </summary> 
		public Tendencia Tendencia { get; set; }

        /// <summary> 
        ///	Tendencia  
        /// </summary> 
        public int KilogramosCalculados { get; set; }

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
