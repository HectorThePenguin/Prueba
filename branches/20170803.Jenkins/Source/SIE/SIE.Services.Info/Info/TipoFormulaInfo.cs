using System.ComponentModel;

namespace SIE.Services.Info.Info
{
	public class TipoFormulaInfo : BitacoraInfo, INotifyPropertyChanged
	{
	    private string descripcion;
		/// <summary> 
		///	TipoFormulaID  
		/// </summary> 
		public int TipoFormulaID { get; set; }

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

        /// <summary>
        /// Fecha de Creacion
        /// </summary>
        public System.DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de ultima modificacion
        /// </summary>
        public System.DateTime FechaModificacion { get; set; }

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

