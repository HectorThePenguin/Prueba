using System.Collections.Generic;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.ORM
{
    public abstract class SolicitudProductoAccessor : BLToolkit.DataAccess.DataAccessor<SolicitudProductoInfo>
    {

        /// <summary>
        /// Obtiene y actualiza el folio de la
        /// solicitud de productos
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="tipoFolioID"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        [BLToolkit.DataAccess.SprocName("Folio_Obtener")]
        public abstract int ObtenerFolioSolicitud(
             int organizacionId, int tipoFolioID, [BLToolkit.DataAccess.Direction.ReturnValue("@Folio")] int folio);

        /// <summary>
        /// Obtiene una lista de folios de 
        /// solicitud de productos a almacén
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="usuarioIDSolicita"></param>
        /// <param name="usuarioIDAutoriza"></param>
        /// <param name="solicita"></param>
        /// <param name="autoriza"></param>
        /// <param name="estatusId"></param>
        /// <param name="activo"></param>
        /// <param name="inicio"></param>
        /// <param name="limite"></param>
        /// <returns></returns>
        [BLToolkit.DataAccess.SprocName("SolicitudProducto_ObtenerPorPagina")]
        public abstract IList<FolioSolicitudInfo> ObtenerPorPagina(
            int organizacionId,
            int usuarioIDSolicita,
            int usuarioIDAutoriza,
            string solicita,
            string autoriza,
            int estatusId,
            EstatusEnum activo,
            int inicio,
            int limite
            );
        
        /// <summary>
        /// Actualiza el inventario del almacén 
        /// por medio de una lista de productos.
        /// </summary>
        /// <param name="almacenInventarioXML"></param>
        [BLToolkit.DataAccess.SprocName("AlmacenInventario_ActualizarPorProductos")]
        public abstract void ActualizaInventario(string almacenInventarioXML);

        /// <summary>
        /// Crea un registro de almacén movimiento
        /// </summary>
        /// <returns></returns>
        [BLToolkit.DataAccess.SprocName("AlmacenMovimiento_Crear")]
        public abstract long CrearMovimientoAlmacen(
            int  almacenID, 
            int  tipoMovimientoID,
            int? proveedorID,
            int status,
            int? usuarioCreacionID,
            string observaciones
            );

        /// <summary>
        /// Crea el detalle de almacén movimiento
        /// </summary>
        /// <param name="xmlAlmacenMovimientoDetalle"></param>
        [BLToolkit.DataAccess.SprocName("AlmacenMovimientoDetalle_GuardarDetalleXml")]
        public abstract void CrearAlmacenMovimientoDetalle(string xmlAlmacenMovimientoDetalle);
    }
}
