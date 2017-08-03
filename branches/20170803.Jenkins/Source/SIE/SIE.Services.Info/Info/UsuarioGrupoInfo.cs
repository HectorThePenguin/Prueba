using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    public class UsuarioGrupoInfo : BitacoraInfo, INotifyPropertyChanged
	{
		/// <summary> 
		///	UsuarioGrupoID  
		/// </summary> 
		public int UsuarioGrupoID { get; set; }

		/// <summary> 
		///	Usuario  
		/// </summary> 
		public UsuarioInfo Usuario { get; set; }

		/// <summary> 
		///	Grupo  
		/// </summary> 
		public GrupoInfo Grupo { get; set; }

        /// <summary>
        /// Indica si el registro  se encuentra Activo
        /// </summary>
        public new EstatusEnum Activo
        {
            get { return base.Activo; }
            set
            {
                if (value != base.Activo)
                {
                    base.Activo = value;
                    NotifyPropertyChanged("Activo");
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
