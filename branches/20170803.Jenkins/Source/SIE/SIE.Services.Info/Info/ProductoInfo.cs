using System.Collections.Generic;
using System.ComponentModel;
using SIE.Services.Info.Atributos;
using SIE.Services.Info.Enums;

namespace SIE.Services.Info.Info
{
    [BLToolkit.DataAccess.TableName("Producto")]
    public class ProductoInfo : BitacoraInfo, INotifyPropertyChanged
    {
        public ProductoInfo()
        {
            Dosis = 0;
            Renglon = 0;
        }

       public ProductoInfo(ProductoInfo info, UnidadMedicionInfo um)
       {
           ProductoId = info.ProductoId;
           Descripcion = info.Descripcion;
           productoDescripcion = info.descripcion;
           SubfamiliaId = info.SubfamiliaId;
           UnidadId = info.UnidadId;
           ManejaLote = info.ManejaLote;
           Activo = info.Activo;
           FechaCreacion = info.FechaCreacion;
           UsuarioCreacionID = info.UsuarioCreacionID;
           FechaModificacion = info.FechaModificacion;
           UsuarioModificacionID = info.UsuarioModificacionID;
           UnidadMedicion = um;
           DescripcionUnidad = info.DescripcionUnidad;
       }

        /// <summary>
        /// Identificador del producto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        private int productoId;

        /// <summary>
        /// Descripción del producto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        private string descripcion;

        /// <summary>
        /// Descripción del producto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        private string productoDescripcion;

        /// <summary>
        /// Descripción de Familia
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        private string descripcionFamilia;

        /// <summary>
        /// Descripción de Sub familia
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        private string descripcionSubFamilia;

        /// <summary>
        /// Descripción de Sub familia
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        private string descripcionUnidad;
       
        /// <summary>
        /// Identificador del Producto
        /// </summary>
        [BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
        [AtributoAyuda(Nombre = "PropiedadProductoIDIngredientesFamilias", EncabezadoGrid = "Id", MetodoInvocacion = "ObtenerIngredientesPorIDFamilias", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadProductoIDTratamientoCentros", EncabezadoGrid = "Id", MetodoInvocacion = "Centros_ObtenerPorProductoIDSubFamilia", PopUp = false, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadProductoIDTratamiento", EncabezadoGrid = "Id", MetodoInvocacion = "ObtenerPorProductoIDSubFamilia", PopUp = false, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadClaveFormulaEdicion", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadClaveBoletaVigilancia", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadProductoID", EncabezadoGrid = "Id", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadProductoIDProgramacion", EncabezadoGrid = "Id", MetodoInvocacion = "ObtenerPorIDSinActivo", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveRegistrarProgramacionMateriaPrima", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveConfiguracionPremezclas", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIdSubFamiliaId", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveAyudaPorductoLoteExtistencia", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIDLoteExistencia", PopUp = false, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadClaveRecepcionProducto", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveProgramacionMateriaPrima", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorProductoIdTengaProgramacionFleteInterna", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveReporteInventarioMateriaPrima", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCrearContrato", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIdFiltroFamiliaSubfamilias", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadProductoSolicitudIDProgramacion", EncabezadoGrid = "Id", MetodoInvocacion = "ObtenerPorIDSinActivo", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveMuestreoTamanoFibra", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorIdSubFamiliaId", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadProductoMonitoreoSilosID", EncabezadoGrid = "Id", MetodoInvocacion = "ObtenerPorIDFamiliaIdSubFamiliaId", PopUp = false)]
        [AtributoInicializaPropiedad]
        public int ProductoId
        {
            get { return productoId; }
            set
            {
                if (value != productoId)
                {
                    productoId = value;
                    NotifyPropertyChanged("ProductoId");
                }
            }
        }

        /// <summary>
        /// Descripción del producto
        /// </summary>
        //[AtributoAyuda(Nombre = "PropiedadDescripcionIngredientesFamilias", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerIngredientesPorFamiliasPaginado", PopUp = true,  EstaEnContenedor = true, NombreContenedor = "Producto")]
        //[AtributoAyuda(Nombre = "PropiedadProductoIDIngredientesFamilias", EncabezadoGrid = "Id", MetodoInvocacion = "ObtenerIngredientesPorIDFamilias", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionIngredientesFamilias", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerIngredientesPorFamiliasPaginado", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcion", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
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
        /// Descripción del producto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        
        //[AtributoAyuda(Nombre = "PropiedadDescripcionIngredientesFamilias", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerIngredientesPorFamiliasPaginado", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionIngredientesFamilias", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerIngredientesPorFamiliasPaginado", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionProductoTratamientoCentros", EncabezadoGrid = "Descripción", MetodoInvocacion = "Centros_ObtenerPorPaginaSubFamilia", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionProductoTratamiento", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaSubFamilia", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionFormulaEdicion", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerProductosPorPagina", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionBoletaVigilancia", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerProductosPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadProductoDescripcion", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorFamiliaPaginado", PopUp = true,EstaEnContenedor = true,NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionConfiguracionPremezclas", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadProductoAyudaLoteExistencia", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaLoteExistencia", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistrarProgramacionMateriaPrima", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaFiltroAlmacen", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRecepcionProducto", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerCompletoPorFamilia", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionProgramacionMateriaPrima", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTengaProgramacionFletesInterna", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionReporteInventarioMateriaPrima", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadProductoSolicitudDescripcion", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorFamiliasPaginado", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCrearContrato", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaFiltroFamiliaSubfamilias", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionMuestreoTamanoFibra", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadProductoMonitoreoSilosDescripcion", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorDescripcionSubFamilia", PopUp = true, EstaEnContenedor = true, NombreContenedor = "Producto")]
        public string ProductoDescripcion
        {
            get { return string.IsNullOrEmpty(productoDescripcion) ? string.Empty : productoDescripcion.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != productoDescripcion)
                {
                    productoDescripcion = value;
                    NotifyPropertyChanged("ProductoDescripcion");
                }
            }
        }

        /// <summary>
        /// Identificador de la subfamilia
        /// </summary>
        [BLToolkit.Mapping.MapField("SubFamiliaID")]
        public int SubfamiliaId { get; set; }
        
        /// <summary>
        /// Identificador de la unidad
        /// </summary>
        [BLToolkit.Mapping.MapField("UnidadID")]
        public int UnidadId { get; set; }

        /// <summary>
        /// Dosis del producto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int Dosis { get; set; }        
        
        /// <summary>
        /// Numero de renglon
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int Renglon { get; set; }
        
        /// <summary>
        /// Descripcion de la Familia
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string DescripcionFamilia
        {
            get { return string.IsNullOrEmpty(descripcionFamilia) ? string.Empty : descripcionFamilia.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != descripcionFamilia)
                {
                    descripcionFamilia = value;
                    NotifyPropertyChanged("DescripcionFamilia");
                }
            }
        }

        /// <summary>
        /// Descripcion de la Subfamilia
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string DescripcionSubFamilia
        {
            get { return string.IsNullOrEmpty(descripcionSubFamilia) ? string.Empty : descripcionSubFamilia.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != descripcionSubFamilia)
                {
                    descripcionSubFamilia = value;
                    NotifyPropertyChanged("DescripcionSubFamilia");
                }
            }
        }

        /// <summary>
        /// Descripcion de la Unidad de Medida
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string DescripcionUnidad
        {
            get { return string.IsNullOrEmpty(descripcionUnidad) ? string.Empty : descripcionUnidad.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != descripcionUnidad)
                {
                    descripcionUnidad = value;
                    NotifyPropertyChanged("DescripcionUnidad");
                }
            }
        }

        /// <summary>
        /// Identificador de Familia
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int FamiliaId { get; set; }

        /// <summary>
        /// Lista de Familias
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public IList<FamiliaInfo> Familias { get; set; }

        /// <summary>
        /// Lista de SubFamilias
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public IList<SubFamiliaInfo> SubFamilias { get; set; }

        /// <summary>
        /// Lista de Unidades de Medicion
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public IList<UnidadMedicionInfo> UnidadesMedidcion { get; set; }

        /// <summary>
        /// SubFamilia del Producto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public SubFamiliaInfo SubFamilia { get; set; }

        /// <summary>
        /// SubFamilia del Producto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public FamiliaInfo Familia { get; set; }
        
        /// <summary>
        /// SubFamilia del Producto
        /// </summary>
        [BLToolkit.Mapping.Association(CanBeNull = true, ThisKey = "UnidadId", OtherKey = "UnidadID")]
        public UnidadMedicionInfo UnidadMedicion { get; set; }

        /// <summary>
        /// Bandera para indicar si el producto maneha lote
        /// </summary>
        public bool ManejaLote { get; set; }

        /// <summary>
        /// Bandera para indicar si el producto maneha lote
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public ManejaLoteEnum ManejaLoteEnum { get; set; }

        /// <summary>
        /// Lista de familias a filtrar
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string FiltroFamilia { get; set; }

        /// <summary>
        /// Contiene el AlmacenID del producto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int AlmacenID { get; set; }

        /// <summary>
        /// Contiene el AlmacenID del producto
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int? CuentaSAPID { get; set; }

        /// <summary>
        /// Indica si el producto es forraje valido
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public bool Forraje { get; set; }

        /// <summary>
        /// Indica si el producto es Premezcla (SubFamilia = MicroIngredientes)
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public bool EsPremezcla { get; set; }

        private int indicadorID;

        /// <summary>
        /// Indica si el producto es Premezcla (SubFamilia = MicroIngredientes)
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int IndicadorID
        {
            get { return indicadorID; }
            set
            {
                indicadorID = value;
                NotifyPropertyChanged("IndicadorID");
            }
        }

        private int tratamientoID;
        /// <summary>
        /// Indica si el producto es Premezcla (SubFamilia = MicroIngredientes)
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int TratamientoID
        {
            get { return tratamientoID; }
            set
            {
                tratamientoID = value;
                NotifyPropertyChanged("TratamientoID");
            }
        }
        [BLToolkit.Mapping.MapIgnore] 
        private string materialSAP;
        /// <summary>
        /// Codigo del producto en SAP
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public string MaterialSAP
        {
            get { return materialSAP; }
            set
            {
                materialSAP = value;
                NotifyPropertyChanged("MaterialSAP");
            }
        }

        [BLToolkit.Mapping.MapIgnore]
        public OrganizacionInfo Organizacion { get; set; }


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
