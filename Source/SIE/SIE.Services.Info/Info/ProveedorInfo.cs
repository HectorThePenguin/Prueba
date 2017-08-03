using System.Collections.Generic;
using SIE.Services.Info.Atributos;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class ProveedorInfo : BitacoraInfo, INotifyPropertyChanged
    {
        public ProveedorInfo()
        {
            Choferes = new List<ChoferInfo>();
            Comisiones = new List<ComisionInfo>();
        }

        private int proveedorID;
        private string descripcion;
        private string codigoSAP;
        private TipoProveedorInfo tipoProveedor;
        /// <summary>
        ///     Identificador Proveedor .
        /// </summary>     
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyudaTipoProveedor",
                EncabezadoGrid = "Clave",
                MetodoInvocacion = "ObtenerPorIDFiltroTipoProveedor",
                PopUp = false,
                EstaEnContenedor = true,
                NombreContenedor = "Proveedor")]   
        [AtributoAyuda(Nombre = "PropiedadClave", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadOcultaCosteo", EstaEnContenedor = true, NombreContenedor = "Proveedor")]
        [AtributoAyuda(Nombre = "PropiedadOcultaRegistroProgramacionEmbarque", EstaEnContenedor = true, NombreContenedor = "Proveedor")]
        [AtributoAyuda(Nombre = "PropiedadOcultaCatalogo", EstaEnContenedor = true, NombreContenedor = "Proveedor")]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogoAyuda",
        EncabezadoGrid = "Clave",
        MetodoInvocacion = "ObtenerPorID",
        PopUp = false,
        EstaEnContenedor = true,
        NombreContenedor = "Proveedor")]
        [AtributoAyuda(Nombre = "PropiedadClaveBoletaVigilancia", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorID", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadOcultaBoletaVigilanciaProducto")]
        [AtributoInicializaPropiedad]
        public int ProveedorID
        {
            get
            {
                return proveedorID;
            }
            set
            {
                if (value != proveedorID)
                {
                    proveedorID = value;
                    NotifyPropertyChanged("ProveedorID");
                }
            }
        }

        /// <summary>
        ///     Nombre o Razón Social del Provedor
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadDescripcionPremezclaDistribucionIngredientes", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCrearContrato", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTiposProveedores", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCrearContratoAlmacen", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTiposProveedoresAlmacen", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionBasculaFleteInternoProducto", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerFoliosPorPaginaFletesInternos", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionEntradaMateriaPrima", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPaginaTiposProveedores", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRegistroProgramacionEmbarque", EncabezadoGrid = "Proveedor", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcion", EncabezadoGrid = "Proveedor", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCosteoGanado", EncabezadoGrid = "Proveedor"
                      , MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true
                      , NombreContenedor = "Proveedor")]

        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogo", EncabezadoGrid = "Proveedor"
                     , MetodoInvocacion = "ObtenerPorPagina", PopUp = true, EstaEnContenedor = true
                     , NombreContenedor = "Proveedor")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyudaTipoProveedor",
                        EncabezadoGrid = "Descripción",
                        MetodoInvocacion = "ObtenerPorPaginaTipoProveedor",
                        PopUp = true,
                        EstaEnContenedor = true,
                        NombreContenedor = "Proveedor")]
        [AtributoAyuda(Nombre = "PropiedadDescripcionBoletaVigilancia", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerTrasportistaPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionRecepcionProducto", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionBasculaFletesProducto", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorFolioFletesInternos", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionBoletaVigilanciaProducto", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerPorProductoContrato", PopUp = true)]
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
        ///     Identificador del Tipo de Proveedor .
        /// </summary>
        public TipoProveedorInfo TipoProveedor
        {
            get { return tipoProveedor; }
            set
            {
                tipoProveedor = value;
                NotifyPropertyChanged("TipoProveedor");
            }
        }

        /// <summary>
        ///  Codigo Sap del Proveedor
        /// </summary>
        [AtributoAyuda(Nombre = "PropiedadClaveCrearContrato", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorCodigoSAP", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClavePremezclaDistribucionIngredientes", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorCodigoSAP", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveBasculaFleteInternoProducto", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorFolioFletesInternos", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionCatalogoAyuda",
                EncabezadoGrid = "Descripción",
                MetodoInvocacion = "ObtenerPorPagina",
                PopUp = true,
                EstaEnContenedor = true,
                NombreContenedor = "Proveedor")]
        [AtributoAyuda(Nombre = "PropiedadClaveRegistroProgramacionEmbarque", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorCodigoSAP", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadClaveCosteoGanado", EncabezadoGrid = "Código SAP", MetodoInvocacion = "ObtenerPorCodigoSAP"
                      , PopUp = false, EstaEnContenedor = true, NombreContenedor = "Proveedor")]
        [AtributoAyuda(Nombre = "PropiedadClaveCatalogo", EncabezadoGrid = "Código SAP", MetodoInvocacion = "ObtenerPorCodigoSAP"
                     , PopUp = false, EstaEnContenedor = true, NombreContenedor = "Proveedor")]
        [AtributoAyuda(Nombre = "PropiedadCodigoSapEntradaMateriaPrima", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorCodigoSAP", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadCodigoSapRecepcionProducto", EncabezadoGrid = "Código SAP", MetodoInvocacion = "ObtenerPorCodigoSAP", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadCodigoSAPBoletaVigilanciaProducto", EncabezadoGrid = "Código SAP", MetodoInvocacion = "ObtenerPorProductoContratoCodigoSAP", PopUp = false)]        
        public string CodigoSAP
        {
            get
            {
                return codigoSAP;
            }
            set
            {
                if (value != codigoSAP)
                {
                    codigoSAP = value;
                    NotifyPropertyChanged("CodigoSAP");
                }
            }
        }

        public IList<TipoProveedorInfo> ListaTiposProveedor { get; set; }

        public List<ChoferInfo> Choferes { get; set; }

        public int ProductoID { get; set; }

        public int OrganizacionID { get; set; }

        /// <summary>
        /// Importe de la comision del proveedor
        /// </summary>
        public decimal ImporteComision { get;set; }

        /// <summary>
        /// AlmacenID del Proveedor
        /// </summary>
        public int AlmacenID { get; set; }

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
        ///    Correo del Ptoveedor
        /// </summary>
        public string Correo { get; set; }

        public List<ComisionInfo> Comisiones { get; set; }
        /// <summary>
        /// Retencion de ISR, configurada para el Proveedor
        /// </summary>
        public RetencionInfo RetencionISR { get; set; }
        /// <summary>
        /// Retencion de IVA, configurada para el Proveedor
        /// </summary>
        public RetencionInfo RetencionIVA { get; set; }
        /// <summary>
        /// IVA, configurada para el Proveedor
        /// </summary>
        public IvaInfo IVA { get; set; }

        /// <summary>
        /// Indica el Embarque de donde se van a buscar los proveedores
        /// </summary>
        public int EmbarqueID { get; set; }
        
    }
}