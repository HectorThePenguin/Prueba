using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Sintoma")]
    public class SintomaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        /// <summary> 
        ///	SintomaID
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        private int sintomaID;

        /// <summary> 
        ///	Descripcion
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        private string descripcion = string.Empty;
        /// <summary> 
        ///	Sintoma  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int SintomaID
        {
            get { return sintomaID; }
            set
            {
                if (value != sintomaID)
                {
                    sintomaID = value;
                    NotifyPropertyChanged("SintomaID");
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
        ///	Fecha Creación  
        /// </summary> 
        [BLToolkit.DataAccess.NonUpdatable]
        public System.DateTime FechaCreacion { get; set; }

        /// <summary> 
        ///	Fecha Modificación  
        /// </summary> 
        public System.DateTime? FechaModificacion { get; set; }

        /// <summary> 
        ///	Orden para la paginacion  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public int Orden { get; set; }

        /// <summary> 
        ///	Indica si se pueden hacer modificaciones al Sintoma pantalla ProblemaSintoma
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public bool HabilitaEdicion { get; set; }

        /// <summary> 
        ///	Indica el ID de a que registro de Problema Sintoma pertenece, para pantalla ProblemaSintoma
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public int ProblemaSintomaID { get; set; }

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
