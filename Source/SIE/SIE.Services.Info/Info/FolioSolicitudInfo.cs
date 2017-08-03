using System;
using System.Collections;
using System.ComponentModel;

namespace SIE.Services.Info.Info
{
    public class FolioSolicitudInfo : INotifyPropertyChanged
    {
        private long folioSolicitud;
        private int folioID;
        private string descripcion;
        private string solicita;
        private string autoriza;
        private UsuarioInfo usuario;
        private ProductoInfo producto;
        private decimal cantidad;
        private CentroCostoInfo centroCosto;
        private Enums.EstatusEnum activo;
        private bool sinProducto;
        private IList idsProductos;
        private DateTime? fechaEntrega;

        public FolioSolicitudInfo()
        {
            activo = Enums.EstatusEnum.Activo;
        }
        
        /// <summary>
        /// Folio de la solicitud
        /// </summary>
        public int FolioID
        {
            get { return folioID; }
            set
            {
                folioID = value;
                NotifyPropertyChanged("FolioID");
            }
        }

        /// <summary>
        /// Folio de la solicitud
        /// </summary>
        public long FolioSolicitud
        {
            get { return folioSolicitud; }
            set
            {
                folioSolicitud = value;
                NotifyPropertyChanged("FolioSolicitud");
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
        /// Usuario
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public UsuarioInfo Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                NotifyPropertyChanged("Usuario");
            }
        }

        /// <summary>
        /// Usuario que solicita
        /// </summary>
        public int UsuarioIDSolicita { set; get; }

        /// <summary>
        /// Nombre del usuario que solicita
        /// </summary>
        public string Solicita
        {
            get { return string.IsNullOrEmpty(solicita) ? null : solicita.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != solicita)
                {
                    solicita = value;
                    NotifyPropertyChanged("Solicita");
                }
            }
        }
        
        /// <summary>
        /// Usuario que autoriza
        /// </summary>
        public int UsuarioIDAutoriza { set; get; }

        /// <summary>
        /// Usuario que autoriza
        /// </summary>
        public int UsuarioIDEntrega { set; get; }

        /// <summary>
        /// Nombre del usuario que autoriza
        /// </summary>
        public string Autoriza
        {
            get { return string.IsNullOrEmpty(autoriza) ? null : autoriza.Trim(); }
            set
            {
                string valor = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
                if (valor.Trim() != autoriza)
                {
                    autoriza = value;
                    NotifyPropertyChanged("Autoriza");
                }
            }
        }

        /// <summary>
        /// Usuario
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public ProductoInfo Producto
        {
            get { return producto; }
            set
            {
                producto = value;
                NotifyPropertyChanged("Producto");
            }
        }

        /// <summary>
        /// Organización del usuario
        /// </summary>
        public int OrganizacionID { set; get; }

        /// <summary>
        /// Estatus de la solicitud
        /// </summary>
        public int EstatusID { set; get; }

        /// <summary>
        /// Cantidad 
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public decimal Cantidad
        {
            get { return cantidad; }
            set
            {
                cantidad = value;
                NotifyPropertyChanged("Cantidad");
            }
        }

        /// <summary>
        /// Clase de costo
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public ClaseCostoProductoInfo ClaseCostoProducto { get; set; }

        /// <summary>
        /// Almacén
        /// </summary>
        public int AlmacenID { get; set; }

        /// <summary>
        /// Id del Almacén Destino
        /// </summary>
        public int AlmacenDestinoID { get; set; }

        /// <summary>
        /// Descripción del Almacén Destino
        /// </summary>
        public string AlmacenDestinoDescripcion { get; set; }

        /// <summary>
        /// Usuario
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public CentroCostoInfo CentroCosto
        {
            get { return centroCosto; }
            set
            {
                centroCosto = value;
                NotifyPropertyChanged("CentroCosto");
            }
        }

        /// <summary>
        /// Indica si el registro  se encuentra Activo
        /// </summary>
        public Enums.EstatusEnum Activo
        {
            get { return activo; }
            set
            {
                activo = value;
                NotifyPropertyChanged("Activo");
            }
        }

        /// <summary>
        /// Indica si el registro  se encuentra Activo
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public bool SinProducto
        {
            get
            {
                sinProducto = producto == null || producto.ProductoId == 0;
                return sinProducto;
            }
            set
            {
                sinProducto = value;
                NotifyPropertyChanged("SinProducto");
            }
        }

        [BLToolkit.Mapping.MapIgnore]
        public IList IdsProductos
        {
            get { return idsProductos; }
            set
            {
                idsProductos = value;
                NotifyPropertyChanged("IdsProductos");
            }
        }
        public DateTime? FechaEntrega
        {
            get { return fechaEntrega; }
            set
            {
                fechaEntrega = value;
                NotifyPropertyChanged("FechaEntrega");
            }
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
