using System.ComponentModel;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("CentroCostoUsuario")]
    public class CentroCostoUsuarioInfo : BitacoraInfo, INotifyPropertyChanged
    {
        [BLToolkit.Mapping.MapIgnore]
        private bool autoriza;
        [BLToolkit.Mapping.MapIgnore]
        private AutorizaEnum autorizaEnum;
        /// <summary> 
        ///	Centro Costo Usuario  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        public int CentroCostoUsuarioID { get; set; }

        /// <summary> 
        ///	Usuario  
        /// </summary> 
        public int UsuarioID { get; set; }

        /// <summary> 
        ///	Entidad Usuario  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public UsuarioInfo Usuario { get; set; }

        /// <summary> 
        ///	Centro Costo  
        /// </summary> 
        public int CentroCostoID { get; set; }

        /// <summary> 
        ///	Entidad Centro Costo  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public CentroCostoInfo CentroCosto { get; set; }

        /// <summary> 
        ///	Autoriza  
        /// </summary> 
        public bool Autoriza
        {
            get { return autoriza; }
            set
            {
                autoriza = value;
                if (value)
                {
                    AutorizaEnum = AutorizaEnum.Si;
                }
                else
                {
                    AutorizaEnum = AutorizaEnum.No;
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
        ///	propiedad para indicar si tiene Cambios
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public bool TieneCambios { get; set; }

        [BLToolkit.Mapping.MapIgnore]
        //[BLToolkit.DataAccess.SqlIgnore]
        public AutorizaEnum AutorizaEnum
        {
            get { return autorizaEnum; }
            set
            {
                autorizaEnum = value;
                NotifyPropertyChanged("AutorizaEnum");
            }
        }

        public CentroCostoUsuarioInfo Clone()
        {
            var tratamientoProductoClone = new CentroCostoUsuarioInfo
            {
                CentroCostoUsuarioID = CentroCostoUsuarioID,
                UsuarioID = UsuarioID,
                Usuario = Usuario,
                CentroCostoID = CentroCostoID,
                CentroCosto = CentroCosto,
                Autoriza = Autoriza,
                TieneCambios = TieneCambios,
                Activo = Activo,
                UsuarioCreacionID = UsuarioCreacionID,
                UsuarioModificacionID = UsuarioModificacionID
            };
            return tratamientoProductoClone;
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
