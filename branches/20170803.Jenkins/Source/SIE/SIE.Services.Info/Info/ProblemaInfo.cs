using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Problema")]
    public class ProblemaInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int problemaID;
        private string descripcion;

        /// <summary> 
        ///	ProblemaID  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity, BLToolkit.Mapping.MapField("ProblemaID")]
        public int ProblemaID
        {
            get { return problemaID; }
            set
            {
                if (value != problemaID)
                {
                    problemaID = value;
                    NotifyPropertyChanged("ProblemaID");
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
        /// Identificador del tipo de problema
        /// </summary>
        public int TipoProblemaID { get; set; }

        /// <summary> 
        ///	Tipo de Problema
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public TipoProblemaInfo TipoProblema { get; set; }
        
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        [BLToolkit.DataAccess.NonUpdatable]
        public DateTime FechaCreacion { get; set; }
        
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }
        
        /// <summary>
        /// Indica si el problema fue seleccionado
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public bool isCheked { get; set; }
        
        /// <summary>
        /// Lista de tratamientos
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public IList<TratamientoInfo> Tratamientos { get; set; }

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
