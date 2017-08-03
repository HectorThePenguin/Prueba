using System.ComponentModel;
namespace SIE.Services.Info.Info
{
	[BLToolkit.DataAccess.TableName("SolicitudProductoDetalle")]
	public class SolicitudProductoDetalleInfo : BitacoraInfo,  INotifyPropertyChanged
	{
        private bool eliminar;
        private bool entregado;
        private bool editar;
	    private bool recibido;

        /// <summary>
        /// Constructor
        /// </summary>
        public SolicitudProductoDetalleInfo()
	    {
	            
	    }

        /// <summary>
        /// Cosntructos
        /// </summary>
        /// <param name="info"></param>
        /// <param name="producto"></param>
        /// <param name="estatus"></param>
        public SolicitudProductoDetalleInfo(SolicitudProductoDetalleInfo info, ProductoInfo producto, EstatusInfo estatus)
        {
            SolicitudProductoDetalleID = info.SolicitudProductoDetalleID;
            SolicitudProductoID = info.SolicitudProductoID;
            ProductoID = info.ProductoID;
            Cantidad = info.Cantidad;
            EstatusID = info.EstatusID;
            Activo = info.Activo;
            Producto = producto;
            Estatus = estatus;
        }
        
		/// <summary> 
		///	Solicitud Producto Detalle  
		/// </summary> 
		[BLToolkit.DataAccess.PrimaryKey, BLToolkit.DataAccess.Identity]
		public int SolicitudProductoDetalleID { get; set; }

		/// <summary> 
		///	Solicitud Producto  
		/// </summary> 
		public int SolicitudProductoID { get; set; }

		/// <summary> 
		///	Entidad Solicitud Producto  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public SolicitudProductoInfo SolicitudProducto { get; set; }

		/// <summary> 
		///	Producto  
		/// </summary> 
		public int ProductoID { get; set; }

		/// <summary> 
		///	Entidad Producto  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
		public ProductoInfo Producto { get; set; }

		/// <summary> 
		///	Cantidad  
		/// </summary> 
		public decimal Cantidad { get; set; }

        /// <summary> 
        ///	Cantidad  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public decimal PrecioPromedio { get; set; }
        
        /// <summary> 
		///	Entidad Centro Costo  
		/// </summary> 
		[BLToolkit.Mapping.MapIgnore]
        public ClaseCostoProductoInfo ClaseCostoProducto { get; set; }

        /// <summary> 
        ///	Camión reparto
        /// </summary> 
        public int? CamionRepartoID { get; set; }

        /// <summary> 
        ///	Entidad Camion Reparto
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public CamionRepartoInfo CamionReparto { get; set; }

        /// <summary> 
        ///	Camión reparto
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public string Concepto{ get; set; }

        ///// <summary> 
        /////	Entidad camión reparto
        ///// </summary> 
        //[BLToolkit.Mapping.MapIgnore]
        //public CamionRepartoInfo CamionReparto { get; set; }

        /// <summary> 
        ///	Entidad Centro Costo  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public CuentaSAPInfo CuentaSAP { get; set; }

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
		///	Fecha Creación  
		/// </summary> 
		[BLToolkit.DataAccess.NonUpdatable]
		public System.DateTime FechaCreacion { get; set; }

		/// <summary> 
		///	Fecha Modificación  
		/// </summary> 
		public System.DateTime? FechaModificacion { get; set; }

        /// <summary> 
        ///	Indica si se puede eliminar el registro  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public bool Eliminar
        {
            get
            {
                return eliminar;
            }
            set
            {
                eliminar = value;
                NotifyPropertyChanged("Eliminar");
            }
        }

        /// <summary> 
        ///	Indica si el registro ha sido entregado  
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public bool Entregado
        {
            get
            {
                return entregado;
            }
            set
            {
                entregado = value;
                NotifyPropertyChanged("Entregado");
            }
        }

        /// <summary> 
        ///	Indica si el registro si ya fue recibido
        /// </summary> 
        [BLToolkit.Mapping.MapIgnore]
        public bool Recibido
        {
            get
            {
                return recibido;
            }
            set
            {
                recibido = value;
                NotifyPropertyChanged("Recibido");
            }
        }

        /// <summary>
        /// Indica si el registro se puede editar
        /// </summary>
        [BLToolkit.Mapping.MapIgnore]
        public bool Editar
        {
            get
            {
                return editar;
            }
            set
            {
                editar = value;
                NotifyPropertyChanged("Editar");
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
