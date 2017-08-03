using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("TipoCuenta")]
    public class TipoCuentaInfo : BitacoraInfo, INotifyPropertyChanged
	{
        private int tipoCuentaID;
        private string descripcion;
		/// <summary> 
		///	TipoCuentaID  
		/// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int TipoCuentaID
        {
            get { return tipoCuentaID; }
            set
            {
                if (value != tipoCuentaID)
                {
                    tipoCuentaID = value;
                    NotifyPropertyChanged("TipoCuentaID");
                }
            }
        }

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
