using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidad;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Base.Vista;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.PL;

namespace SIE.Web.PlantaAlimentos
{
    public partial class RecepcionPaseProceso :  PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Valida si existe almacen par 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static Response<OrganizacionInfo> ValidarAlmacenOrganizacion()
        {
            try
            {
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var almacenPl = new AlmacenPL();
                    IList<AlmacenInfo> almacenes = almacenPl.ObtenerAlmacenPorOrganizacion(seguridad.Usuario.Organizacion.OrganizacionID);
                    if (almacenes != null)
                    {
                        if (almacenes.Count > 0)
                        {
                            return Response<OrganizacionInfo>.CrearResponseVacio<OrganizacionInfo>(true,
                            "CONALMACEN");
                        }
                    }
                    return Response<OrganizacionInfo>.CrearResponse(false,seguridad.Usuario.Organizacion,
                        "SINALMACEN");
                }
                return Response<OrganizacionInfo>.CrearResponse(false, new OrganizacionInfo(), "SESION");
            }
            catch (Exception ex)
            {
                return Response<OrganizacionInfo>.CrearResponse<OrganizacionInfo>(false,new OrganizacionInfo(),
                            ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los pedidos que se encuentren en estatus pendiente
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static List<PedidoInfo> ObtenerPedidosParciales()
        {
            var listaPedidosPendientes = new List<PedidoInfo>();
            
            try
            {
                var recepcionMateriaPrimaPl = new RecepcionMateriaPrimaPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var pedido = new PedidoInfo
                    {
                        Organizacion = seguridad.Usuario.Organizacion,
                        FolioPedido = 0
                    };
                    List<PedidoInfo> tmpPedidos = recepcionMateriaPrimaPl.ObtenerPedidosParciales(pedido);
                    if (tmpPedidos != null)
                    {
                        listaPedidosPendientes.AddRange(tmpPedidos.Where(tmpPedido => tmpPedido.EstatusPedido.EstatusId == (int) Estatus.PedidoParcial));
                    }
                }
            }
            catch (ExcepcionDesconocida)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaPedidosPendientes;
        }

        /// <summary>
        /// Metodo para obtener los datos del folio capturado
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<PedidoInfo> ObtenerPedidoParcialesPorFolio(int folio)
        {
            var listaPedidosPendientes = new List<PedidoInfo>();
            try
            {
                var recepcionMateriaPrimaPl = new RecepcionMateriaPrimaPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var pedido = new PedidoInfo
                    {
                        Organizacion = seguridad.Usuario.Organizacion,
                        FolioPedido = folio
                    };

                    var tmpListaPedidosPendientes = recepcionMateriaPrimaPl.ObtenerPedidosParciales(pedido);

                    if (tmpListaPedidosPendientes != null)
                    {
                        listaPedidosPendientes.AddRange(tmpListaPedidosPendientes.Where(tmpPedido => tmpPedido.EstatusPedido.EstatusId == (int) Estatus.PedidoParcial));
                    }
                }

                return listaPedidosPendientes;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }
        /// <summary>
        /// Consulta el folio en el textbox de la ayuda sin el dialogo de ayuda
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [WebMethod]
        public static Response<PedidoInfo> ObtenerPedidoParcialesPorFolioUnico(int folio)
        {
            var listaPedidosPendientes = new List<PedidoInfo>();
            try
            {
                var recepcionMateriaPrimaPl = new RecepcionMateriaPrimaPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var pedido = new PedidoInfo
                    {
                        Organizacion = seguridad.Usuario.Organizacion,
                        FolioPedido = folio
                    };

                    listaPedidosPendientes = recepcionMateriaPrimaPl.ObtenerPedidosParciales(pedido);
                }
                if (listaPedidosPendientes != null)
                {
                    if (listaPedidosPendientes.Count > 0)
                    {
                        if (listaPedidosPendientes[0].EstatusPedido.EstatusId == (int) Estatus.PedidoParcial)
                        {
                            return Response<PedidoInfo>.CrearResponse(true, listaPedidosPendientes[0], "OK");
                        }
                        return Response<PedidoInfo>.CrearResponse(false, listaPedidosPendientes[0],
                            "ESTATUSINCORRECTO");
                    }
                }
                return Response<PedidoInfo>.CrearResponse(false, new PedidoInfo(), "SINRESULTADOS");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene el surtido del pedido seleccionado
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<SurtidoPedidoInfo> ObtenerSurtidoPorFolio(int folio)
        {
            var listaDeSurtido = new List<SurtidoPedidoInfo>();
            try
            {
                var recepcionMateriaPrimaPl = new RecepcionMateriaPrimaPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    var pedido = new PedidoInfo
                    {
                        Organizacion = seguridad.Usuario.Organizacion,
                        FolioPedido = folio
                    };
                    listaDeSurtido = recepcionMateriaPrimaPl.ObtenerSurtidoPedido(pedido);
                }

                return listaDeSurtido;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }

        /// <summary>
        /// Guarda la recepcion de materia prima
        /// </summary>
        /// <param name="listaSurtido"></param>
        /// <returns></returns>
        [WebMethod]
        public static bool ActualizarRecepcionMateriaPrima(List<SurtidoPedidoInfo> listaSurtido, PedidoInfo pedido)
        {
            bool resultado = false;
            try
            {

                var recepcionMateriaPrimaPl = new RecepcionMateriaPrimaPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad != null)
                {
                    pedido.UsuarioModificacion = seguridad.Usuario;
                    if (recepcionMateriaPrimaPl.ActualizarRecepcionMateriaPrima(listaSurtido, pedido))
                    {
                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
            return resultado;
        }
        /// <summary>
        /// Finaliza el Pedido para que ya no pueda pesar los
        /// </summary>
        /// <param name="pedido"></param>
        [WebMethod]
        public static void FinalizarPedido(int pedido)
        {
            try
            {
                var pedidoPl = new PedidosPL();
                var seguridad = HttpContext.Current.Session["Seguridad"] as SeguridadInfo;

                if (seguridad == null) return;
                
                pedidoPl.ActualizarEstatusPedido(new PedidoInfo
                {
                    PedidoID = pedido,
                    UsuarioModificacion = seguridad.Usuario,
                    EstatusPedido = new EstatusInfo
                    {
                        EstatusId = (int)Estatus.PedidoCompletado
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new SystemException(ex.Message);
            }
        }
    }
}