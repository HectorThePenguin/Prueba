using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("CentroCosto")]
    public class CentroCostoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int centroCostoID;
        private string descripcion;
        private string centroCostoSAP;

        /// <summary> 
        ///	Centro Costo  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        [Atributos.AtributoInicializaPropiedad]
        public int CentroCostoID
        {
            get { return centroCostoID; }
            set
            {
                centroCostoID = value;
                NotifyPropertyChanged("CentroCostoID");
            }
        }

        /// <summary> 
        ///	Cuenta SAP del centro de costo  
        /// </summary>      
        public string CentroCostoSAP
        {
            get { return string.IsNullOrEmpty(centroCostoSAP) ? null : centroCostoSAP.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != centroCostoSAP)
                {
                    centroCostoSAP = value;
                    NotifyPropertyChanged("CentroCostoSAP");
                }
            }
        }

        /// <summary>
        /// Descripción
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
        ///	Area Departamento  
        /// </summary> 
        public string AreaDepartamento { get; set; }

        /// <summary> 
        ///	Fecha Creación  
        /// </summary> 
        [BLToolkit.DataAccess.NonUpdatable]
        public System.DateTime FechaCreacion { get; set; }

        /// <summary> 
        ///	Fecha Modificación  
        /// </summary> 
        public System.DateTime? FechaModificacion { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        //[BLToolkit.DataAccess.SqlIgnore]
        public int AutorizadorID { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        //[BLToolkit.DataAccess.SqlIgnore]
        public List<CentroCostoUsuarioInfo> ListaCentroCostoUsuario { get; set; }

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