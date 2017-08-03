using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("GrupoCorral")]
    public class GrupoCorralInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion = string.Empty;
        /// <summary> 
        ///	GrupoCorralID  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int GrupoCorralID { get; set; }

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
