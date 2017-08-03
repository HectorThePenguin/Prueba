using System;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("TipoServicio")]
    public class TipoServicioInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private string descripcion;
        /// <summary> 
        ///	TipoServicioID  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity, BLToolkit.Mapping.MapField("TipoServicioID")]
        public int TipoServicioId { get; set; }

        /// <summary> 
        ///	HoraInicio  
        /// </summary> 
        public string HoraInicio { get; set; }

        /// <summary> 
        ///	HoraFin  
        /// </summary> 
        public string HoraFin { get; set; }

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
        /// Descripcion que se muestra en el combo
        /// </summary>
        [BLToolkit.DataAccess.SqlIgnore, BLToolkit.Mapping.MapIgnore]
        public string DescripcionCombo
        {
            get { return (TipoServicioId == 0 ? "" : TipoServicioId + " - ") + Descripcion; }
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

        [BLToolkit.Mapping.MapIgnore]
        public LoteInfo Lote { get; set; }

        public override string ToString()
        {
            return (TipoServicioId == 0 ? "" : TipoServicioId + " - ") + Descripcion;
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
