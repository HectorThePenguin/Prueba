
using SIE.Services.Info.Atributos;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class VigilanciaInfo: BitacoraInfo, INotifyPropertyChanged
    {
        private int organizacionId;
        private string descripcion;
        private string direccion;
        private string codigoSap;

        /// <summary>
        /// Identificador de la organización
        /// </summary>
       
        [AtributoAyuda(Nombre = "PropiedadOcultaID", EstaEnContenedor = true, NombreContenedor = "ID")]
        [AtributoAyuda(Nombre = "PropiedadClaveBoletaVigilanciaPlacas", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPlacaPorID", PopUp = false)]
        public int ID
        {
            get { return organizacionId; }
            set
            {
                if (value != organizacionId)
                {
                    organizacionId = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }
        /// <summary>
        /// Cuenta sap del proveedor
        /// </summary>
        
        [AtributoAyuda(Nombre = "PropiedadCodigoSAPBoletaVigilancia", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerPorClaveSap", PopUp = false)]
        [AtributoAyuda(Nombre = "PropiedadCodigoSAPBoletaVigilanciaTransportista", EncabezadoGrid = "Clave", MetodoInvocacion = "ObtenerTrasportistaPorClaveSap", PopUp = false)]

        public string CodigoSAP
        {
            get
            {
                return codigoSap;
            }
            set
            {
                if (value != codigoSap)
                {
                    codigoSap = value;
                    NotifyPropertyChanged("CodigoSAP");
                }
            }
        }



        /// <summary>
        /// Tipo de Organización
        /// </summary>
        public TipoOrganizacionInfo TipoOrganizacion { get; set; }
        /// <summary>
        /// Descripcion de la organización
        /// </summary>

        [AtributoAyuda(Nombre = "PropiedadDescripcionBoletaVigilancia", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerProveedoresProductoPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionBoletaVigilanciaTransportista", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerTrasportistaPorPagina", PopUp = true)]
        [AtributoAyuda(Nombre = "PropiedadDescripcionBoletaVigilanciaCamion", EncabezadoGrid = "Descripción", MetodoInvocacion = "ObtenerCamionPorPagina", PopUp = true)]
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                if (value != descripcion)
                {
                    descripcion = value;
                    NotifyPropertyChanged("Descripcion");
                }
            }
        }

        /// <summary>
        /// Dirección de la Organizacion
        /// </summary>
        public string Direccion
        {
            get { return direccion; }
            set
            {
                if (value != direccion)
                {
                    direccion = value;
                    NotifyPropertyChanged("Direccion");
                }
            }
        }

        /// <summary>
        /// RFC de la Organizacion
        /// </summary>
        public string RFC { get; set; }

        /// <summary>
        /// Iva que se aplica en la Organizacion
        /// </summary>
        public IvaInfo Iva { get; set; }

        /// <summary>
        /// Producto seleccionado
        /// </summary>
        public ProductoInfo Producto { get; set; }

        public CamionInfo Camion { get; set; }

        public ProveedorInfo Proveedor { get; set; }

        public ProveedorInfo Transportista { get; set; }

        public ContratoInfo Contrato { get; set; }
 
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