using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Almacen")]
    public class AlmacenInfo : BitacoraInfo, INotifyPropertyChanged
    {
        private int almacenID;
        private string descripcion;
        private OrganizacionInfo organizacion;

        /// <summary> 
        ///	Almacén  
        /// </summary> 
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        [AtributoAyuda(Nombre = "PropiedadClaveRegistrarProgracionFletesInterna", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIdFiltroTipoAlmacen", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveReporteMedicamentosAplicados", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIdFiltroTipoAlmacen", PopUp = false)]
        public int AlmacenID
        {
            get { return almacenID; }
            set
            {
                almacenID = value;
                NotifyPropertyChanged("AlmacenID");
            }
        }

        /// <summary> 
        ///	Organización  
        /// </summary> 
        public int OrganizacionID { get; set; }

        /// <summary> 
        ///	Entidad Organización  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public OrganizacionInfo Organizacion
        {
            get { return organizacion; }
            set
            {
                organizacion = value;
                NotifyPropertyChanged("Organizacion");
            }
        }

        /// <summary> 
        ///	Código Almacén  
        /// </summary> 
        public string CodigoAlmacen { get; set; }

        /// <summary>
        /// Descripcion del almacen
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistrarProgramacionFletesInterna", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTipoAlmacen", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionReporteMedicamentosAplicados", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTipoAlmacen", PopUp = true)]
        public string Descripcion
        {
            get { return descripcion != null ? descripcion.Trim() : descripcion; }
            set
            {
                if (value != descripcion)
                {
                    descripcion = value != null ? value.Trim() : null;
                    NotifyPropertyChanged("Descripcion");
                }
            }
        }

        /// <summary> 
        ///	Tipo Almacén  
        /// </summary> 
        public int TipoAlmacenID { get; set; }

        /// <summary> 
        ///	Entidad Tipo Almacén  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public TipoAlmacenInfo TipoAlmacen { get; set; }

        /// <summary> 
        ///	Cuenta Inventario  
        /// </summary> 
        public string CuentaInventario { get; set; }

        /// <summary> 
        ///	Cuenta Inventario Tránsito  
        /// </summary> 
        public string CuentaInventarioTransito { get; set; }

        /// <summary> 
        ///	Cuenta Diferencias  
        /// </summary> 
        public string CuentaDiferencias { get; set; }

        /// <summary> 
        ///	Fecha Creación  
        /// </summary> 
        [BLToolkit.DataAccess.NonUpdatable]
        public System.DateTime FechaCreacion { get; set; }

        /// <summary> 
        ///	Fecha Modificación  
        /// </summary> 
        public System.DateTime? FechaModificacion { get; set; }

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
        /// <summary>
        /// Filtro para consultar por un conjunto de tipo de almacén.
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string FiltroTipoAlmacen { get; set; }

        /// <summary>
        /// Listado de tipos almacen
        /// </summary>
        public List<TipoAlmacenInfo> ListaTipoAlmacen { get; set; }

        /// <summary> 
        ///	Entidad Organización  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public ProveedorInfo Proveedor { get; set; }

        /// <summary>
        /// Listado de tipos almacen
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public List<AlmacenUsuarioInfo> ListaAlmacenUsuario { get; set; }
    }
}
