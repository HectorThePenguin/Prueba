using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Filtros;

namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("SolicitudProducto")]
    public class SolicitudProductoInfo : BitacoraInfo, INotifyPropertyChanged
	{
        private bool isAutorizado;
        private bool guardar;
	    private TipoMovimiento tipoMovimiento;

        /// <summary>
        /// Constructor
        /// </summary>
	    public SolicitudProductoInfo()
	    {
	       Detalle = new List<SolicitudProductoDetalleInfo>();     
	    }

        /// <summary>
        /// Constructor
        /// </summary>
        public SolicitudProductoInfo(SolicitudProductoInfo info, IQueryable<SolicitudProductoDetalleInfo> detalle )
        {
            OrganizacionID = info.OrganizacionID;
            SolicitudProductoID = info.SolicitudProductoID;
            FolioSolicitud = info.FolioSolicitud;
            FechaSolicitud = info.FechaSolicitud;
            UsuarioIDSolicita = info.UsuarioIDSolicita;
            EstatusID = info.EstatusID;
            UsuarioIDAutoriza = info.UsuarioIDAutoriza;
            FechaAutorizado = info.FechaAutorizado;
            UsuarioIDEntrega = info.UsuarioIDEntrega;
            FechaEntrega = info.FechaEntrega;
            Activo = info.Activo;
            FechaCreacion = info.FechaCreacion;
            UsuarioCreacionID = info.UsuarioCreacionID;
            FechaModificacion = info.FechaModificacion;
            UsuarioModificacionID = info.UsuarioModificacionID;
            ObservacionUsuarioAutoriza = info.ObservacionUsuarioAutoriza;
            ObservacionUsuarioEntrega = info.ObservacionUsuarioEntrega;

            Detalle = detalle.Select(e => e).ToList();
        }

        /// <summary>
        /// Constructor
        /// </summary>
	    public SolicitudProductoInfo(SolicitudProductoInfo info, List<SolicitudProductoDetalleInfo> detalles)
        {
            OrganizacionID = info.OrganizacionID;
            SolicitudProductoID = info.SolicitudProductoID;
            FolioSolicitud = info.FolioSolicitud;
            FechaSolicitud = info.FechaSolicitud;
            UsuarioIDSolicita = info.UsuarioIDSolicita;
            EstatusID = info.EstatusID;
            UsuarioIDAutoriza = info.UsuarioIDAutoriza;
            FechaAutorizado = info.FechaAutorizado;
            UsuarioIDEntrega = info.UsuarioIDEntrega;
            FechaEntrega = info.FechaEntrega;
            Activo = info.Activo;
            FechaCreacion = info.FechaCreacion;
            UsuarioCreacionID = info.UsuarioCreacionID;
            FechaModificacion = info.FechaModificacion;
            UsuarioModificacionID = info.UsuarioModificacionID;
            ObservacionUsuarioAutoriza = info.ObservacionUsuarioAutoriza;
            ObservacionUsuarioEntrega = info.ObservacionUsuarioEntrega;

            Detalle = detalles;
        }

        /// <summary> 
		///	Solicitud Producto  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int SolicitudProductoID { get; set; }

        /// <summary> 
        ///	Organización  
        /// </summary> 
        public int OrganizacionID { get; set; }

        /// <summary> 
        ///	Organización  
        /// </summary> 
        public OrganizacionInfo Organizacion { get; set; }

        /// <summary> 
        ///	Centro Costo de la persona que solicita.  
        /// </summary> 
        public int CentroCostoID { get; set; }

        /// <summary> 
        ///	Centro Costo de la persona que solicita.  
        /// </summary> 
        public CentroCostoInfo CentroCosto { get; set; }

        /// <summary> 
        ///	Almacén  
        /// </summary> 
        public int? AlmacenID { get; set; }
        
        /// <summary> 
        ///	Entidad almacén
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public AlmacenInfo Almacen { get; set; }

        /// <summary> 
        ///	Entidad Camión Reparto
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public CamionRepartoInfo CamionReparto { get; set; }

		/// <summary> 
		///	Folio Solicitud  
		/// </summary> 
		public long FolioSolicitud { get; set; }

		/// <summary> 
		///	Fecha Solicitud  
		/// </summary> 
		public System.DateTime FechaSolicitud { get; set; }

		/// <summary> 
		///	Usuario Iolicita  
		/// </summary> 
		public int? UsuarioIDSolicita { get; set; }

		/// <summary> 
		///	Entidad UsuarioSolicita  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public UsuarioInfo UsuarioSolicita { get; set; }

		/// <summary> 
		///	Estatus  
		/// </summary> 
		public int EstatusID { get; set; }

		/// <summary> 
		///	Entidad Estatus  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public EstatusInfo Estatus { get; set; }

		/// <summary> 
		///	Usuario Autoriza  
		/// </summary> 
		public int? UsuarioIDAutoriza { get; set; }

		/// <summary> 
		///	Entidad Usuario Autoriza  
		/// </summary> 
        [BLToolkit.Mapping.MapIgnore]
		public UsuarioInfo UsuarioAutoriza { get; set; }

		/// <summary> 
		///	Fecha Autorizado  
		/// </summary> 
        public System.DateTime? FechaAutorizado { get; set; }

		/// <summary> 
		///	Usuario Intrega  
		/// </summary> 
		public int? UsuarioIDEntrega { get; set; }

		/// <summary> 
		///	Entidad Usuario Entrega  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public UsuarioInfo UsuarioEntrega { get; set; }

		/// <summary> 
		///	Fecha Entrega  
		/// </summary> 
		public System.DateTime? FechaEntrega { get; set; }

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
        ///	Observaciones del usuario Autorizador
        /// </summary> 
        public string ObservacionUsuarioAutoriza { get; set; }

        /// <summary> 
        ///	Observaciones del usuario que entrega
        /// </summary> 
        public string ObservacionUsuarioEntrega { get; set; }

        /// <summary> 
        ///	Detalle de Solicitud de Productos
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public  List<SolicitudProductoDetalleInfo> Detalle { get; set; }
        
        /// <summary> 
        ///	Detalle de Solicitud de Productos
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public ObservableCollection<SolicitudProductoDetalleInfo> DetalleGrid { get; set; }
        
        /// <summary>
        /// Contenedor para la ayuda
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public FolioSolicitudInfo Solicitud { set; get; }

        /// <summary>
        /// Contenedor para la ayuda
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public int AlmacenGeneralID { set; get; }

        /// <summary>
        /// Contenedor para la ayuda
        /// </summary>
        public long? AlmacenMovimientoID { set; get; }

        /// <summary> 
        ///	Indica si la solicitud ya fue autorizada.
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public bool IsAutorizado
        {
            get
            {
                return isAutorizado = (UsuarioIDAutoriza.HasValue && UsuarioIDAutoriza.Value > 0);
            }
            set
            {
                isAutorizado = value;
                NotifyPropertyChanged("IsAutorizado");
            }
        }

        /// <summary> 
        ///	Indica si se puede guardar la solicitud de productos.
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public bool Guardar
        {
            get
            {
                //guardar = !isAutorizado && !(Detalle == null || Detalle.Count == 0);
                return guardar;
            }
            set
            {
                guardar = value;
                NotifyPropertyChanged("Guardar");
            }
        }

        /// <summary> 
        ///	Indica si se puede guardar la solicitud de productos.
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public TipoMovimiento TipoMovimientoInventario
        {
            get { return tipoMovimiento; }
            set
            {
                tipoMovimiento = value;
                NotifyPropertyChanged("TipoMovimientoInventario");
            }
        }

        /// <summary>
        /// Se utiliza para el campo de la ayuda
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public FiltroProductoInfo FiltroProducto { get; set; }
        
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
