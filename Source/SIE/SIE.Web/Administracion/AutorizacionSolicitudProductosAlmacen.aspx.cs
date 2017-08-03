using System;
using System.Collections.Generic;
using System.Linq;
using SIE.Base.Infos;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;
using SIE.Services.Servicios.PL;

namespace SIE.Web.Administracion
{
    public partial class AutorizacionSolicitudProductosAlmacen : PageBase
    {
        /// <summary>
        /// Usuario Autorizador
        /// </summary>
        private  static  UsuarioInfo Usuario
        {
            get
            {
                var seguridad = (SeguridadInfo) ObtenerSeguridad();
                seguridad = seguridad ?? new SeguridadInfo { Usuario = new UsuarioInfo() };
                return seguridad.Usuario;
            }
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Obtiene el almacén general que tenga configurado
        /// el usuario.
        /// </summary>
        private static AlmacenInfo ObtenerAlmacenGenerarl(int organizacionId)
        {
            var almacenDaL = new AlmacenPL();
            AlmacenInfo almacenInfo = null;
            IList<AlmacenInfo> almacenes = almacenDaL.ObtenerAlmacenPorOrganizacion(organizacionId);
            if (almacenes != null && almacenes.Count > 0)
            {
                almacenInfo = almacenes.FirstOrDefault(a => a.TipoAlmacenID == (int)TipoAlmacenEnum.GeneralGanadera
                                                            && a.Activo == EstatusEnum.Activo);
            }
            return almacenInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solicitud"></param>
        /// <param name="solicitudProductoBL"></param>
        /// <returns></returns>
        private static List<SolicitudProductoDetalleModel> ValidaDisponibilidad(SolicitudProductoInfo solicitud, SolicitudProductoBL solicitudProductoBL)
        {
            dynamic result;

            IEnumerable<int> idsProductos = solicitud.Detalle
                .Where(d => d.Producto.FamiliaId != (int)FamiliasEnum.HerramientaYEquipo || d.Producto.FamiliaId != (int)FamiliasEnum.Combustibles)
                .Select(d => d.ProductoID).Distinct().ToList();
            SolicitudProductoInfo solicitudGuardar = solicitudProductoBL.ObtenerPorID(solicitud);
            var filtro = new FolioSolicitudInfo
            {
                OrganizacionID = solicitudGuardar.OrganizacionID,
                IdsProductos = idsProductos.ToList(),
                EstatusID = Estatus.SolicitudProductoAutorizado.GetHashCode(),
                Activo = EstatusEnum.Activo
            };

            IList<AlmacenInventarioInfo> existencia = ObtenerExistencia(idsProductos);
            IList<SolicitudProductoInfo> solicitudesAutorizadas = solicitudProductoBL.ObtenerSolicitudesAutorizadas(filtro);

            var productosValidar = solicitud.Detalle.Select(d => new
            {
                d.ProductoID,
                d.Cantidad,
                d.Producto.FamiliaId
            }).ToList();

            var autorizadas = (from p in solicitudesAutorizadas.SelectMany(sd => sd.Detalle)
                               where p.EstatusID == Estatus.SolicitudProductoAutorizado.GetHashCode()
                               group p by p.ProductoID
                               into pg
                               select new
                                          {
                                              ProductoID = pg.Key,
                                              Autorizada = pg.Sum(c => c.Cantidad)
                                          }).ToList();
            List<SolicitudProductoDetalleModel> query = (from p in productosValidar
                        join a in autorizadas on p.ProductoID equals a.ProductoID into gj
                        from pa in gj.DefaultIfEmpty()
                        select new SolicitudProductoDetalleModel
                        {
                            ProductoID = p.ProductoID,
                            Cantidad = p.Cantidad,
                            Existencia = ObtenerCantidadExistencia(p.ProductoID, p.FamiliaId, p.Cantidad, existencia, (pa == null ? 0 : pa.Autorizada)),/* Para los  */
                            Autorizada = (pa == null ? 0 : pa.Autorizada),
                            IsDisponible = (ObtenerCantidadExistencia(p.ProductoID, p.FamiliaId, p.Cantidad, existencia, (pa == null ? 0 : pa.Autorizada)) - (pa == null ? 0 : pa.Autorizada)) >= 0
                        }).ToList();
            return query;
        }

        /// <summary>
        /// Validar la existencia del producto
        /// </summary>
        /// <param name="productoID"></param>
        /// <param name="familiaId"></param>
        /// <param name="cantidad"></param>
        /// <param name="existencia"></param>
        /// <param name="paAutorizada"></param>
        /// <returns></returns>
        private static decimal ObtenerCantidadExistencia(int productoID, int familiaId, decimal cantidad, IList<AlmacenInventarioInfo> existencia,
            decimal paAutorizada)
        {
            decimal cant = 0;
            if (familiaId == (int)FamiliasEnum.HerramientaYEquipo || familiaId == (int)FamiliasEnum.Combustibles)
            {
                return (paAutorizada == 0 ? cantidad : paAutorizada + cantidad);
            }
            
            var listaExistencias = (from p in existencia
                where p.ProductoID == productoID
                select new { p.Cantidad }
                ).ToList();
            var firstOrDefault = listaExistencias.FirstOrDefault();
            if (firstOrDefault != null) cant = firstOrDefault.Cantidad;

            return cant;
        }

        /// <summary>
        /// Obtiene una solicitud de productos por su ID.
        /// </summary>
        /// <param name="solicitudProductoID"></param>
        /// <returns></returns>
        private static SolicitudProductoInfo ObtenerSolicitudPorId(int solicitudProductoID)
        {
            SolicitudProductoInfo solicitud;
            using (var solicitudProductoBL = new SolicitudProductoBL())
            {
                solicitud =
                    solicitudProductoBL.ObtenerPorID(new SolicitudProductoInfo {SolicitudProductoID = solicitudProductoID});
            }
            return solicitud;
        }

        /// <summary>
        /// Consulta el folio de solicitud de productos al almacén.
        /// </summary>
        /// <param name="folioSolicitudInfo"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static IList<FolioSolicitudInfo> ObtenerPorPagina(FolioSolicitudInfo folioSolicitudInfo)
        {
            var pagina = new PaginacionInfo { Inicio = 1, Limite = 15 };

            folioSolicitudInfo.Usuario = new UsuarioInfo();
            folioSolicitudInfo.OrganizacionID = Usuario.OrganizacionID;
            folioSolicitudInfo.UsuarioIDAutoriza = Usuario.UsuarioID;
            folioSolicitudInfo.EstatusID = Estatus.SolicitudProductoPendiente.GetHashCode();
            folioSolicitudInfo.Activo = EstatusEnum.Activo;

            IList<FolioSolicitudInfo> result = null;
            ResultadoInfo<FolioSolicitudInfo>  solicitudes;

            using (var solicitudProductoBL = new SolicitudProductoBL())
            {
                solicitudes = solicitudProductoBL.ObtenerPorPagina(pagina, folioSolicitudInfo);
            }
            
            if (solicitudes != null && solicitudes.TotalRegistros > 0)
            {
                result = solicitudes.Lista;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folioSolicitud"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static  IList<SolicitudProductoDetalleModel> ObtenerPorFolioSolicitud(int folioSolicitud)
        {
            try
            {
                IList<SolicitudProductoDetalleModel> productos = null;
                var folio = new FolioSolicitudInfo
                                {
                                    OrganizacionID = Usuario.OrganizacionID,
                                    FolioSolicitud = folioSolicitud,
                                    UsuarioIDAutoriza = Usuario.UsuarioID,
                                    EstatusID = Estatus.SolicitudProductoPendiente.GetHashCode(),
                                    Activo = EstatusEnum.Activo
                                };

                using (var solicitudProductoBL = new SolicitudProductoBL())
                {
                    FolioSolicitudInfo result = solicitudProductoBL.ObtenerPorFolioSolicitud(folio);
                    if (result != null)
                     {
                         var solicitud = ObtenerSolicitudPorId(result.FolioID);
                         if (solicitud != null)
                         {
                             if (solicitud.UsuarioIDSolicita != Usuario.UsuarioID)
                             {
                                 var disponibilidad = ValidaDisponibilidad(solicitud, solicitudProductoBL);
                                 productos = new List<SolicitudProductoDetalleModel>();

                                 var estatusAutorizados = new[] { Estatus.SolicitudProductoAutorizado.GetHashCode(), Estatus.SolicitudProductoRecibido.GetHashCode(), Estatus.SolicitudProductoEntregado.GetHashCode() };
                                 var estatusDisponibles = new[] { Estatus.SolicitudProductoPendiente.GetHashCode(), Estatus.SolicitudProductoAutorizado.GetHashCode() };

                                 solicitud.Detalle.ForEach(d =>
                                 {
                                     var registro = disponibilidad.FirstOrDefault(e => e.ProductoID == d.ProductoID);
                                     if (d.Activo == EstatusEnum.Activo)
                                     {
                                         productos.Add(new SolicitudProductoDetalleModel
                                         {
                                             OrganizacionID = solicitud.OrganizacionID,
                                             SolicitudProductoDetalleId =
                                                 d.SolicitudProductoDetalleID,
                                             SolicitudProductoId = d.SolicitudProductoID,
                                             FolioSolicitud = solicitud.FolioSolicitud,
                                             FechaSolicitud = solicitud.FechaSolicitud,
                                             ProductoID = d.ProductoID,
                                             Producto = d.Producto.Descripcion,
                                             Cantidad = d.Cantidad,
                                             Existencia = registro != null ? registro.Existencia : 0,
                                             Autorizada = registro != null ? registro.Autorizada : 0,
                                             IsDisponible = registro != null && registro.IsDisponible && estatusDisponibles.Contains(d.EstatusID),
                                             UnidadMedicion = d.Producto.UnidadMedicion.Descripcion,
                                             Descripcion = d.Concepto ?? string.Empty,
                                             ClaseCosto = d.ClaseCostoProducto != null
                                                    ? d.ClaseCostoProducto.CuentaSAP.CuentaSAP
                                                    : string.Empty,
                                             EstatusID = d.EstatusID,
                                             IsAutorizado = estatusAutorizados.Contains(d.EstatusID),
                                             ObservacionUsuarioAutoriza = solicitud.ObservacionUsuarioAutoriza,
                                             Activo = d.Activo == EstatusEnum.Activo
                                         });
                                     }
                                 });
                             }
                         }
                     }
                }
                return productos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene la existencia del almacen
        /// </summary>
        /// <param name="idProductos"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static IList<AlmacenInventarioInfo> ObtenerExistencia(IEnumerable<int> idProductos)
        {
            IList<ProductoInfo> producto = idProductos.Select(id => new ProductoInfo {ProductoId = id}).ToList();
            int organizacionId = Usuario.OrganizacionID;
            AlmacenInfo almacen = ObtenerAlmacenGenerarl(organizacionId);
            int almacenId = almacen.AlmacenID;

            var almacenInventarioBL = new AlmacenInventarioPL();
            IList<AlmacenInventarioInfo> result = almacenInventarioBL.ExistenciaPorProductos(almacenId, producto);
            return result;
        }
            
        /// <summary>
        /// Obtiene el usuario logueado
        /// </summary>
        /// <returns></returns>
         [System.Web.Services.WebMethod]
        public static UsuarioInfo ObtenerUsuario()
        {
            return Usuario;
        }

        /// <summary>
        /// Guarda la autorización de solicitud de productos al almacén.
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        [System.Web.Services.WebMethod]
        public static bool Guardar(SolicitudProductoInfo solicitud)
        {
            using (var solicitudProductoBL = new SolicitudProductoBL())
            {                              
                SolicitudProductoInfo solicitudGuardar = solicitudProductoBL.ObtenerPorID(solicitud);
                solicitudGuardar.EstatusID = Estatus.SolicitudProductoAutorizado.GetHashCode();
                solicitudGuardar.UsuarioIDAutoriza = Usuario.UsuarioID;
                solicitudGuardar.FechaAutorizado = DateTime.Now;
                solicitudGuardar.UsuarioModificacionID = Usuario.UsuarioID;
                solicitudGuardar.ObservacionUsuarioAutoriza = solicitud.ObservacionUsuarioAutoriza;

                foreach (var det in solicitudGuardar.Detalle)
                {
                    var renglon =
                        solicitud.Detalle.FirstOrDefault(d => d.SolicitudProductoDetalleID == det.SolicitudProductoDetalleID);
                    if (renglon != null)
                    {
                        if (renglon.Activo == EstatusEnum.Activo)
                        {
                            det.EstatusID = Estatus.SolicitudProductoAutorizado.GetHashCode();
                        }
                        else
                        {
                            det.Activo = EstatusEnum.Inactivo;
                        }
                    }
                }
                bool inactivar = solicitudGuardar.Detalle.All(e => e.Activo != EstatusEnum.Activo);
                if (inactivar)
                {
                    solicitudGuardar.Activo = EstatusEnum.Inactivo;
                }

                solicitudProductoBL.Guardar(solicitudGuardar);                
            }
            return true;
        }

        [System.Web.Services.WebMethod]
        public static  bool ValidaRolUsuario()
        {
            var resultado = false;
            try
            {
                var usuarioPL = new UsuarioPL();
                UsuarioInfo usuario = usuarioPL.ObtenerSupervisorID(Usuario.UsuarioID);
                if (usuario != null && usuario.Operador != null && usuario.Operador.Rol != null)
                {
                    resultado = (usuario.Operador.Rol.RolID == Roles.Autorizador.GetHashCode());
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return resultado;
        }
    }
}
