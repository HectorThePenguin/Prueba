namespace SIE.Services.Info.Modelos
{
    public class SolicitudProductoDetalleModel
    {
        /// <summary>
        /// Organización a la que pertenece el registro
        /// </summary>
        public int OrganizacionID { set; get; }
        /// <summary>
        /// Identificador del registro detalle de la solictud de productos 
        /// </summary>
        public int SolicitudProductoDetalleId { set; get; }
        /// <summary>
        /// Identificador del registro de la solicitud de productos
        /// </summary>
        public int SolicitudProductoId { set; get; }
        /// <summary>
        /// Folio de la Solicitud
        /// </summary>
        public long FolioSolicitud { set; get; }
        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public System.DateTime FechaSolicitud { set; get; }
        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int ProductoID { set; get; }
        /// <summary>
        /// Descripción del producto
        /// </summary>
        public string Producto { set; get; }
        /// <summary>
        /// Cantidad Solicitada
        /// </summary>
        public decimal Cantidad { set; get; }
        /// <summary>
        /// Unidad de medición
        /// </summary>
        public string UnidadMedicion { set; get; }
        /// <summary>
        /// Descripción del centro de costo
        /// </summary>
        public string Descripcion { set; get; }
        /// <summary>
        /// Clase de costo
        /// </summary>
        public string ClaseCosto { set; get; }
        /// <summary>
        /// Cuenta SAP del centro de costo
        /// </summary>
        public string CentroCosto { set; get; }
        /// <summary>
        /// Estatus de la solicitud
        /// </summary>
        public int EstatusID { set; get; }
        /// <summary>
        /// Existencia
        /// </summary>
        public decimal Existencia { set; get; }
        /// <summary>
        /// Cantidad autorizada
        /// </summary>
        public decimal Autorizada { set; get; }
        /// <summary>
        /// Vefifica si esta autorizada
        /// </summary>
        public bool IsAutorizado { set; get; }
        /// <summary>
        /// Indica si el producto tiene disponibilidad en el inventario
        /// </summary>
        public bool IsDisponible { set; get; }
        /// <summary>
        /// Observaciones del usuario autorizador
        /// </summary>
        public string ObservacionUsuarioAutoriza { set; get; }
        /// <summary>
        /// Ibservaciones del usuario quentrega
        /// </summary>
        public string ObservacionUsuarioEntrega { set; get; }
        /// <summary>
        /// Indica si el registro esta Activo
        /// </summary>
        public bool Activo { set; get; }
    }
}