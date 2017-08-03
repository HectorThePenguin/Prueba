using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.MateriaPrima
{
    public partial class AbastoDeMateriaPrima : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Método que regresa los folios de Pedidos que tienen estatus de Programado y Parciales.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<PedidoInfo> ObtenerFolios(int folioPedido)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            int organizacionId = 0;
            List<PedidoInfo> listaPedidos = null;
            var pedidosPl = new PedidosPL();

            if (seguridad != null)
            {
                organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
            }

            listaPedidos = pedidosPl.ObtenerPedidosProgramadosYParciales(new PedidoInfo
            {
                FolioPedido = folioPedido,// 0 Regresa todos los folios.
                Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                Activo = EstatusEnum.Activo
            });

            List<PedidoInfo> listaPedidoGenerico = null;

            if (listaPedidos != null)
            {
                foreach (var pedido in listaPedidos)
                {
                    if (pedido.DetallePedido != null)
                    {
                        int contadorProducto = 0, contadorPesaje = 0;

                        foreach (PedidoDetalleInfo pedidoDetalle in pedido.DetallePedido)
                        {
                            contadorProducto++;

                            var detalle = new ParametrosDetallePedidoInfo
                            {
                                Producto = pedidoDetalle.Producto,
                                PedidoDetalleId = pedidoDetalle.PedidoDetalleId,
                                LoteProceso = pedidoDetalle.InventarioLoteDestino,
                                CantidadSolicitada = pedidoDetalle.CantidadSolicitada
                            };

                            if (pedidoDetalle.ProgramacionMateriaPrima != null)
                            {
                                foreach (var programacion in pedidoDetalle.ProgramacionMateriaPrima)
                                {
                                    if (programacion.PesajeMateriaPrima != null)
                                    {
                                        foreach (var pesajeMateriaPrima in
                                                programacion.PesajeMateriaPrima.Where(
                                                    pesajeMateriaPrima => pesajeMateriaPrima.PesoTara > 0))
                                        {
                                            contadorPesaje++;
                                        }
                                    }
                                }
                            }
                        }

                        if (contadorPesaje != contadorProducto)
                        {
                            if (listaPedidoGenerico == null)
                            {
                                listaPedidoGenerico = new List<PedidoInfo>();
                            }

                            listaPedidoGenerico.Add(pedido);
                        }
                    }
                }
            }

            if (listaPedidoGenerico != null)
            {
                listaPedidoGenerico = listaPedidoGenerico.OrderBy(x => x.FolioPedido).ToList();
            }
            return listaPedidoGenerico;
        }

        /// <summary>
        /// Método que regresa los folios de Pedidos que tienen estatus de Programado y Parciales.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<ParametrosDetallePedidoInfo> ObtenerPedidoFolio(int folioPedido)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            int organizacionId = 0;
            List<PedidoInfo> listaPedidos = null;
            var pedidosPl = new PedidosPL();

            try
            {
                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                listaPedidos = pedidosPl.ObtenerPedidosProgramadosYParciales(new PedidoInfo
                {
                    FolioPedido = folioPedido, // 0 Regresa todos los folios.
                    Organizacion = new OrganizacionInfo {OrganizacionID = organizacionId},
                    Activo = EstatusEnum.Activo
                });

                List<ParametrosDetallePedidoInfo> pedidoDetalleGenerico = null;

                if (listaPedidos != null)
                {
                    PedidoInfo pedido = listaPedidos.First();

                    if (pedido.DetallePedido != null)
                    {
                        foreach (PedidoDetalleInfo pedidoDetalle in pedido.DetallePedido)
                        {
                            var agregarProducto = true;

                            var detalle = new ParametrosDetallePedidoInfo
                            {
                                Producto = pedidoDetalle.Producto,
                                PedidoDetalleId = pedidoDetalle.PedidoDetalleId,
                                LoteProceso = pedidoDetalle.InventarioLoteDestino,
                                CantidadSolicitada = pedidoDetalle.CantidadSolicitada
                            };

                            if (pedidoDetalle.ProgramacionMateriaPrima != null)
                            {
                                foreach (var programacion in pedidoDetalle.ProgramacionMateriaPrima
                                    )
                                {
                                    if (programacion.PesajeMateriaPrima != null)
                                    {
                                        foreach (
                                            var pesajeMateriaPrima in
                                                programacion.PesajeMateriaPrima.Where(
                                                    pesajeMateriaPrima => pesajeMateriaPrima.PesoTara > 0))
                                        {
                                            agregarProducto = false;
                                        }
                                    }
                                }

                                detalle.CantidadEntregada =
                                    pedidoDetalle.ProgramacionMateriaPrima.Sum(
                                        programacionMateria => programacionMateria.CantidadEntregada);
                            }

                            detalle.CantidadPendiente = pedidoDetalle.CantidadSolicitada - detalle.CantidadEntregada;

                            if (pedidoDetalle.Producto == null)
                            {
                                agregarProducto = false;
                            }

                            if (agregarProducto)
                            {
                                if (pedidoDetalleGenerico == null)
                                {
                                    pedidoDetalleGenerico = new List<ParametrosDetallePedidoInfo>();
                                }

                                pedidoDetalleGenerico.Add(detalle);
                            }
                        }
                    }
                }
                return pedidoDetalleGenerico;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que consulta la programacion de materia prima para el pedido detalle seleccionado
        /// </summary>
        /// <param name="folioDetallePedido"></param>
        /// <param name="folioPedido"></param>
        /// <returns></returns>
        [WebMethod]
        public static PedidoDetalleInfo ObtenerProgramacionPedidoDetalle(int folioDetallePedido, int folioPedido)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            int organizacionId = 0;
            var pedidosPl = new PedidosPL();

            try
            {
                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                List<PedidoInfo> listaPedidos = pedidosPl.ObtenerPedidosProgramadosYParciales(new PedidoInfo
                {
                    FolioPedido = folioPedido, // 0 Regresa todos los folios.
                    Organizacion = new OrganizacionInfo { OrganizacionID = organizacionId },
                    Activo = EstatusEnum.Activo
                });

                PedidoDetalleInfo pedidoDetalleInfo = null;

                if (listaPedidos != null)
                {
                    PedidoInfo pedido = listaPedidos.First();
                    foreach (PedidoDetalleInfo pedidoDetalle in pedido.DetallePedido)
                    {
                        if (pedidoDetalle.PedidoDetalleId == folioDetallePedido)
                        {
                            var productoPl = new ProductoPL();
                            var productoForraje = productoPl.ObtenerProductoForraje(pedidoDetalle.Producto);
                            if (productoForraje != null)
                            {
                                pedidoDetalle.Producto.Forraje = true;
                            }
                            pedidoDetalleInfo = pedidoDetalle;
                            break;
                        }
                    }
                }

                return pedidoDetalleInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Método que consulta los proveedores de fletes.
        /// </summary>
        /// <param name="proveedor"></param>
        /// <param name="productoId"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ProveedorInfo> ObtenerProveedoresFleteros(ProveedorInfo proveedor, int productoId)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            int organizacionId = 0;

            try
            {
                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                var proveedorPl = new ProveedorPL();

                List<ProveedorInfo> listaProveedores = proveedorPl.ObtenerProveedoresEnFletesInternos(productoId, organizacionId);

                if (proveedor.CodigoSAP != string.Empty)
                {
                    if (listaProveedores != null)
                    {
                        listaProveedores = listaProveedores.Where(lp => lp.CodigoSAP == proveedor.CodigoSAP.PadLeft(10, '0')).ToList();
                    }
                }
                else if (proveedor.Descripcion != string.Empty)
                {
                    if (listaProveedores != null)
                    {
                        listaProveedores = listaProveedores.Where(lp => lp.Descripcion.Contains(proveedor.Descripcion.ToUpper(CultureInfo.InvariantCulture))).ToList();
                    }
                }

                if (listaProveedores != null)
                {
                    listaProveedores = listaProveedores.OrderBy(x => x.CodigoSAP).ToList();
                }

                return listaProveedores;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), e);
            }
        }

        /// <summary>
        /// Consulta los choferes que tiene asignado el proveedor.
        /// </summary>
        /// <param name="choferDescripcion"></param>
        /// <param name="proveedorChofer"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ChoferInfo> ObtenerChoferes(string choferDescripcion, ProveedorChoferInfo proveedorChofer)
        {
            try
            {
                var choferPl = new ProveedorChoferPL();
                var listaChoferes = new List<ChoferInfo>();

                List<ProveedorChoferInfo> listaProveedorChofer = choferPl.ObtenerProveedorChoferPorProveedorId(proveedorChofer.Proveedor.ProveedorID);

                if (listaProveedorChofer != null)
                {
                    foreach (ProveedorChoferInfo proveedorChoferInfo in listaProveedorChofer)
                    {
                        if (choferDescripcion != string.Empty)
                        {
                            if (proveedorChoferInfo.Chofer.NombreCompleto.Contains(choferDescripcion.ToUpper()))
                            {
                                listaChoferes.Add(proveedorChoferInfo.Chofer);
                            }
                        }
                        else
                        {
                            if (proveedorChofer.Chofer.ChoferID == 0)
                            {
                                listaChoferes.Add(proveedorChoferInfo.Chofer);
                            }
                            else if(proveedorChoferInfo.Chofer.ChoferID == proveedorChofer.Chofer.ChoferID)
                            {
                                listaChoferes.Add(proveedorChoferInfo.Chofer);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    listaChoferes = null;
                }

                if (listaChoferes != null)
                {
                    listaChoferes = listaChoferes.OrderBy(x => x.ChoferID).ToList();
                }

                return listaChoferes;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), e);
            }
        }

        /// <summary>
        /// Consulta las placas del proveedor.
        /// </summary>
        /// <param name="placaDescripcion"></param>
        /// <param name="camionInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<CamionInfo> ObtenerPlacas(string placaDescripcion, CamionInfo camionInfo)
        {
            try
            {
                var camionPl = new CamionPL();
                var listaCamiones = new List<CamionInfo>();

                List<CamionInfo> listaCamionInfo = camionPl.ObtenerPorProveedorID(camionInfo.Proveedor.ProveedorID);

                if (listaCamionInfo != null)
                {
                    foreach (CamionInfo camion in listaCamionInfo)
                    {
                        if (placaDescripcion != string.Empty)
                        {
                            if (camion.PlacaCamion.Contains(placaDescripcion.ToUpper()))
                            {
                                listaCamiones.Add(camion);
                            }
                        }
                        else
                        {
                            if (camionInfo.CamionID == 0)
                            {
                                listaCamiones.Add(camion);
                            }
                            else if (camion.CamionID == camionInfo.CamionID)
                            {
                                listaCamiones.Add(camion);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    listaCamiones = null;
                }

                if (listaCamiones != null)
                {
                    listaCamiones = listaCamiones.OrderBy(x => x.CamionID).ToList();
                }

                return listaCamiones;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), e);
            }
        }

        /// <summary>
        ///  Método que regresa los folios de Pedidos que tienen estatus de Programado y Parciales, en base al ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="folio"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<ParametrosDetallePedidoInfo> ObtenerPedidoPorTicket(int ticket, int folio)
        {
            var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
            int organizacionId = 0;
            var pedidosPl = new PedidosPL();

            try
            {
                if (seguridad != null)
                {
                    organizacionId = seguridad.Usuario.Organizacion.OrganizacionID;
                }

                List<ParametrosDetallePedidoInfo> pedidoDetalleGenerico = null;

                var pedidoSeleccionado = pedidosPl.ObtenerPedidoPorFolio(folio, organizacionId);

                if (pedidoSeleccionado != null)
                {
                    foreach (PedidoDetalleInfo pedidoDetalle in pedidoSeleccionado.DetallePedido)
                    {
                        bool agregarProducto = false;

                        var detalle = new ParametrosDetallePedidoInfo
                        {
                            Producto = pedidoDetalle.Producto,
                            PedidoDetalleId = pedidoDetalle.PedidoDetalleId,
                            LoteProceso = pedidoDetalle.InventarioLoteDestino,
                            CantidadSolicitada = pedidoDetalle.CantidadSolicitada
                        };

                        if (pedidoDetalle.ProgramacionMateriaPrima != null)
                        {
                            foreach (var programacion in pedidoDetalle.ProgramacionMateriaPrima
                                )
                            {
                                if (programacion.PesajeMateriaPrima != null)
                                {
                                    foreach (
                                        var pesajeMateriaPrima in
                                            programacion.PesajeMateriaPrima.Where(
                                                pesajeMateriaPrima =>
                                                    pesajeMateriaPrima.PesoTara > 0 &&
                                                    pesajeMateriaPrima.Ticket == ticket))
                                    {
                                        agregarProducto = true;
                                    }
                                }
                            }

                            detalle.CantidadEntregada =
                                pedidoDetalle.ProgramacionMateriaPrima.Sum(
                                    programacionMateria => programacionMateria.CantidadEntregada);
                        }

                        detalle.CantidadPendiente = pedidoDetalle.CantidadSolicitada -
                                                    detalle.CantidadEntregada;

                        if (agregarProducto)
                        {
                            if (pedidoDetalleGenerico == null)
                            {
                                pedidoDetalleGenerico = new List<ParametrosDetallePedidoInfo>();
                            }

                            pedidoDetalleGenerico.Add(detalle);
                        }
                    }
                }

                return pedidoDetalleGenerico;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que consulta la programacion de materia prima para el pedido detalle seleccionado por ticket
        /// </summary>
        /// <param name="folioDetallePedido"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [WebMethod]
        public static PedidoDetalleInfo ObtenerProgramacionPedidoDetalleTicket(int folioDetallePedido, int ticket)
        {
            try
            {
                PedidoDetalleInfo pedidoDetalleInfo = null;

                var pedidoDetallePl = new PedidoDetallePL();
                var pedidoDetalle = pedidoDetallePl.ObtenerDetallePedidoPorId(folioDetallePedido);

                if (pedidoDetalle.ProgramacionMateriaPrima != null)
                {
                    foreach (var programacion in pedidoDetalle.ProgramacionMateriaPrima)
                    {
                        if (programacion.PesajeMateriaPrima != null)
                        {
                            foreach (
                                var pesajeMateriaPrima in
                                    programacion.PesajeMateriaPrima.Where(
                                        pesajeMateriaPrima => pesajeMateriaPrima.Ticket == ticket))
                            {
                                var listapesajeTicket = new List<PesajeMateriaPrimaInfo>();
                                var listaProgramacionTicket = new List<ProgramacionMateriaPrimaInfo>();
                                listapesajeTicket.Add(pesajeMateriaPrima);
                                programacion.PesajeMateriaPrima = listapesajeTicket;

                                listaProgramacionTicket.Add(programacion);
                                pedidoDetalle.ProgramacionMateriaPrima = listaProgramacionTicket;

                                var productoPl = new ProductoPL();
                                var productoForraje = productoPl.ObtenerProductoForraje(pedidoDetalle.Producto);
                                if (productoForraje != null)
                                {
                                    pedidoDetalle.Producto.Forraje = true;
                                }
                                pedidoDetalleInfo = pedidoDetalle;
                                break;
                            }
                        }
                    }
                }

                return pedidoDetalleInfo;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Actualiza el pesaje que viene de bascula de pase a proceso.
        /// </summary>
        /// <param name="pesajeMateriaPrima"></param>
        /// <param name="programacionMateriaPrima"></param>
        /// <param name="pedido"></param>
        [WebMethod]
        public static void ActualizarPesajeMateriaPrimaTicket(PesajeMateriaPrimaInfo pesajeMateriaPrima, ProgramacionMateriaPrimaInfo programacionMateriaPrima, int pedido)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                var usuarioId = 0;

                if (seguridad != null)
                {
                    usuarioId = seguridad.Usuario.UsuarioID;
                }

                var abastoMateriaPrimaPl = new AbastoMateriaPrimaPL();
                var proveedorChoferPl = new ProveedorChoferPL();

                if (pesajeMateriaPrima.ProveedorChofer.Proveedor.ProveedorID > 0
                    && pesajeMateriaPrima.ProveedorChofer.Chofer.ChoferID > 0)
                {
                    pesajeMateriaPrima.ProveedorChofer = proveedorChoferPl.ObtenerProveedorChoferPorProveedorIdChoferId(
                        pesajeMateriaPrima.ProveedorChofer.Proveedor.ProveedorID,
                        pesajeMateriaPrima.ProveedorChofer.Chofer.ChoferID);
                }
                pesajeMateriaPrima.UsuarioModificacionID = usuarioId;
                pesajeMateriaPrima.UsuarioCreacionID = usuarioId;
                programacionMateriaPrima.UsuarioModificacion = new UsuarioInfo {UsuarioID = usuarioId};

                abastoMateriaPrimaPl.ActualizarAbastoDeMateriaPrima(pesajeMateriaPrima, programacionMateriaPrima);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Crea un nuevo pesaje de materia prima y actualiza el estatus del pedido a Parcial
        /// </summary>
        /// <param name="pesajeMateriaPrima"></param>
        /// <param name="programacionMateriaPrima"></param>
        /// <param name="pedido"></param>
        [WebMethod]
        public static void CrearPesajeMateriaPrima(PesajeMateriaPrimaInfo pesajeMateriaPrima, ProgramacionMateriaPrimaInfo programacionMateriaPrima, int pedido)
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;
                var usuarioId = 0;
                var pedidoInfo = new PedidoInfo();

                if (seguridad != null)
                {
                    usuarioId = seguridad.Usuario.UsuarioID;
                    pedidoInfo = new PedidoInfo
                    {
                        FolioPedido = pedido,
                        Organizacion = seguridad.Usuario.Organizacion
                    };
                }

                var abastoMateriaPrimaPl = new AbastoMateriaPrimaPL();
                var pedidoPL = new PedidosPL();

                pedidoInfo = pedidoPL.ObtenerPedidoPorFolio(pedidoInfo);
                if(pedidoInfo !=null)
                {
                    pedido = pedidoInfo.PedidoID;
                }

                var proveedorChoferPl = new ProveedorChoferPL();

                if (pesajeMateriaPrima.ProveedorChofer.Proveedor.ProveedorID > 0 &&
                    pesajeMateriaPrima.ProveedorChofer.Chofer.ChoferID > 0)
                {
                    pesajeMateriaPrima.ProveedorChofer = proveedorChoferPl.ObtenerProveedorChoferPorProveedorIdChoferId(
                        pesajeMateriaPrima.ProveedorChofer.Proveedor.ProveedorID,
                        pesajeMateriaPrima.ProveedorChofer.Chofer.ChoferID);
                    pesajeMateriaPrima.ProveedorChoferID = pesajeMateriaPrima.ProveedorChofer.ProveedorChoferID;
                }
                else
                {
                    pesajeMateriaPrima.ProveedorChoferID = 0;
                }
                pesajeMateriaPrima.UsuarioModificacionID = usuarioId;
                pesajeMateriaPrima.UsuarioCreacionID = usuarioId;
                pesajeMateriaPrima.UsuarioIDSurtido = usuarioId;
                pesajeMateriaPrima.UsuarioIDRecibe = usuarioId;

                pesajeMateriaPrima.EstatusID = Estatus.PedidoParcial.GetHashCode();
                pesajeMateriaPrima.TipoPesajeID = TipoPesajeEnum.Tolva.GetHashCode();
                pesajeMateriaPrima.Activo = true;
                pesajeMateriaPrima.FechaSurtido = DateTime.Today;
                pesajeMateriaPrima.FechaRecibe = DateTime.Today;

                programacionMateriaPrima.UsuarioModificacion = new UsuarioInfo { UsuarioID = usuarioId };


                

                abastoMateriaPrimaPl.GuardarAbastoDeMateriaPrima(pesajeMateriaPrima, programacionMateriaPrima, pedido);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}