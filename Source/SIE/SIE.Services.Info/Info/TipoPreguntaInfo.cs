using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("TipoPregunta")]
	public class TipoPreguntaInfo : BitacoraInfo, INotifyPropertyChanged
	{
	    /// <summary> 
	    ///	Descripcion
	    /// </summary> 
	    [BLToolkit.Mapping.MapIgnore] private string descripcion = string.Empty;

		/// <summary> 
		///	TipoPreguntaID  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int TipoPreguntaID { get; set; }

        /// <summary> 
        ///	Descripcion  
        /// </summary> 
        public string Descripcion
        {
            get { return string.IsNullOrEmpty(descripcion) ? null : descripcion.Trim(); }
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
        /// Fecha de Creacion
        /// </summary>
        [BLToolkit.DataAccess.NonUpdatable]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de Modificacion
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

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
