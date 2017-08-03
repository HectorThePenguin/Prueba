using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxPedidoDAL
    {
        /// <summary>
        /// Obtiene los parametros necesarios para obtener los pedidos programados y parciales
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPedidosProgramadosYParciales(PedidoInfo pedido)
        {   
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@PedidoEstatusIdUno", (int)Estatus.PedidoParcial},
                        {"@PedidoEstatusIdDos", (int)Estatus.PedidoProgramado},
                        {"@OrganizacionId", pedido.Organizacion.OrganizacionID},
                        {"@FolioPedido", pedido.FolioPedido},
                        {"@Activo", (int)EstatusEnum.Activo}
                        
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para obtener todos los pedidos.
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerTodos(int organizacionId,EstatusEnum estatus)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Activo", (int)estatus},
                        {"@OrganizacionID",organizacionId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para consultar con paginacion los pedidos por folio.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPedidosPorFolioPaginado(PaginacionInfo pagina,PedidoInfo pedido)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Folio",pedido.FolioPedido},
                        {"@OrganizacionID",pedido.Organizacion.OrganizacionID},
                        {"@Activo", EstatusEnum.Activo},
                        {"@EstatusID",pedido.EstatusPedido.EstatusId},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros necesarios para obtener el pedido por folio pedido.
        /// </summary>
        /// <param name="folioPedido"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorFolio(int folioPedido,int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Folio", folioPedido},
                        {"@Activo",EstatusEnum.Activo},
                        {"@OrganizacionID",organizacionId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para ejecutar el procedimiento
        /// almacenado de Pedido_ObtenerFolioPorPagina
        /// </summary>
        /// <param name="paginacion"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPedidosProgramadosPaginado(PaginacionInfo paginacion, PedidoInfo pedido)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", pedido.Organizacion.OrganizacionID},
                        {"@Activo", pedido.Activo},
                        {"@DescripcionAlmacen", pedido.Almacen.Descripcion},
                        {"@Inicio", paginacion.Inicio},
                        {"@Limite", paginacion.Limite},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene los parametros para ejecutar el procedimiento
        /// almacenado de Pedido_ObtenerFolioPorFolioPedido
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPedidosProgramadosPorFolioPedido(int folioPedido, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@OrganizacionID", organizacionID},
                        {"@FolioPedido", folioPedido},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        internal static Dictionary<string, object> ObtenerParametrosPorFolioPaginado(PaginacionInfo pagina, PedidoInfo pedido)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {                        
                        {"@OrganizacionID", pedido.Organizacion.OrganizacionID},
                        {"@DescripcionAlmacen", pedido.Almacen.Descripcion},
                        {"@Activo", pedido.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite},
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene un pedido por folio
        /// </summary>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPorFolio(PedidoInfo pedidoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Folio", pedidoInfo.FolioPedido},
                        {"@Activo", EstatusEnum.Activo},
                        {"@OrganizacionID", pedidoInfo.Organizacion.OrganizacionID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene folios de pedido por filtro
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPedidosPorFiltro(PaginacionInfo pagina, PedidoInfo filtro)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                XElement element = null;
                if (filtro.ListaEstatusPedido == null)
                {
                    if (filtro.EstatusPedido != null)
                    {
                        element = new XElement("ROOT", new XElement("Datos",
                                                                   new XElement("estatusPedido",
                                                                                filtro.EstatusPedido.EstatusId)));
                    }
                }
                else
                {
                     element = new XElement("ROOT",
                                                from estatusPedido in filtro.ListaEstatusPedido
                                                select new XElement("Datos",
                                                                    new XElement("estatusPedido",
                                                                                 estatusPedido.EstatusId)));
                }
                

                parametros = new Dictionary<string, object>
                    {
                        {"@Folio", filtro.FolioPedidoBusqueda},
                        {"@OrganizacionID", filtro.Organizacion.OrganizacionID},
                        {"@xmlEstatusPedido", element.ToString()},
                        {"@Activo", filtro.Activo},
                        {"@Inicio", pagina.Inicio},
                        {"@Limite", pagina.Limite}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Metodo que actualiza el estatus de un pedido
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosActualizarEstatusPedido(PedidoInfo pedidoInfo)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@PedidoID", pedidoInfo.PedidoID},
                        {"@EstatusID", pedidoInfo.EstatusPedido.EstatusId},
                        {"UsuarioModificacionID", pedidoInfo.UsuarioModificacion.UsuarioModificacionID}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene parametro detalle por 
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerPedidoPorTicketPesaje(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Ticket", pesajeMateriaPrimaInfo.Ticket},
                                {"@Activo", pesajeMateriaPrimaInfo.Activo},
                                {"@OrganizacionID", organizacionId}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        
        /// <summary>
        /// Obtiene los parametros para crear
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(PedidoInfo pedido,TipoFolio tipoFolio)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@OrganizacionID", pedido.Organizacion.OrganizacionID},
                                {"@AlmacenID", pedido.Almacen.AlmacenID},
                                {"@EstatusID", pedido.EstatusPedido.EstatusId},
                                {"@UsuarioCrecionID",pedido.UsuarioCreacion.UsuarioCreacionID},
                                {"@TipoFolioID",tipoFolio.GetHashCode()},
                                {"@Observaciones",pedido.Observaciones},
                                {"@Activo",pedido.Activo.GetHashCode()}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para
        /// la ejecucion del procedimiento almacenado
        /// Pedido_ObtenerPorPaginaRecibido
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPedidoCompletoPaginado(PaginacionInfo pagina, PedidoInfo pedido)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@OrganizacionID", pedido.Organizacion.OrganizacionID},
                                {"@Descripcion", pedido.Observaciones},
                                {"@Inicio", pagina.Inicio},
                                {"@Limite", pagina.Limite},
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimiento almacenado Pedido_ObtenerRecibido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosPedidoCompleto(PedidoInfo pedido)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@OrganizacionID", pedido.Organizacion.OrganizacionID},
                                {"@FolioPedido", pedido.FolioPedido},
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimiento almacenado EntradaMateriaPrima_ObtenerPedidoPendienteLote
        /// </summary>
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPedidosPendientesPorLote(int almacenInventarioLoteID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AlmacenInventarioLoteID", almacenInventarioLoteID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimiento almacenado EntradaMateriaPrima_ObtenerPedidoPendienteLote
        /// </summary>
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPedidosProgramadosPorLote(int almacenInventarioLoteID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AlmacenInventarioLoteID", almacenInventarioLoteID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene los parametros necesarios para la ejecucion del
        /// procedimiento almacenado Pedido_ObtenerCantidadEntregadaLote
        /// </summary>
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerPedidosEntregadosPorLote(int almacenInventarioLoteID)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@AlmacenInventarioLoteID", almacenInventarioLoteID}
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerPedidosProgramadosPorLoteCantidadProgramada(int almacenInventarioLoteId)
        {
            try
            {
                Logger.Info();
                var parametros =
                        new Dictionary<string, object>
                            {
								{"@Lote", almacenInventarioLoteId},
                                {"@Activo", EstatusEnum.Activo.GetHashCode()},
                            };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        internal static Dictionary<string, object> ObtenerParametrosObtenerPedidosPorFiltroCancelacion(PaginacionInfo pagina, PedidoInfo pedido)
        {
            try{
                var xml = new XElement("ROOT",
                                        from estatusPedido in pedido.ListaEstatusPedido
                                        select new XElement("EstatusPedido",
                                                            new XElement("EstatusID",
                                                                            estatusPedido.EstatusId)));
                
                var parametros = new Dictionary<string, object>
                {
                    {"@Folio",pedido.FolioPedidoBusqueda},
                    {"@OrganizacionID", pedido.Organizacion.OrganizacionID},
                    {"@Fecha",pedido.FechaPedido},
                    {"@XMLEstatus",xml.ToString()},
                    {"@Activo", pedido.Activo},
                    {"@Inicio", pagina.Inicio},
                    {"@Limite", pagina.Limite}
                };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
