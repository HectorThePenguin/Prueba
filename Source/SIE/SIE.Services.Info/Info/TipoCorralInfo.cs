using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{

    [BLToolkit.DataAccess.TableName("TipoCorral")]
    public class TipoCorralInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int tipoCorralID;
        private string descripcion;

        /// <summary> 
        ///	TipoCorralID  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int TipoCorralID
        {
            get { return tipoCorralID; }
            set
            {
                if (value != tipoCorralID)
                {
                    tipoCorralID = value;
                    NotifyPropertyChanged("TipoCorralID");
                }
            }
        }

		/// <summary> 
		///	Descripcion  
		/// </summary> 
        public string Descripcion
        {
            get { return string.IsNullOrEmpty(descripcion) ? string.Empty : descripcion.Trim(); }
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
        /// Grupo Corral
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public GrupoCorralInfo GrupoCorral { get; set; }

        public int GrupoCorralID { get; set; }

        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        [BLToolkit.DataAccess.NonUpdatable]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Fecha de modificacion del registro
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
