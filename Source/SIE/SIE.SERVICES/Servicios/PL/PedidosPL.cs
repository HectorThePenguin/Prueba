using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class PedidosPL
    {
        /// <summary>
        /// Guarda un pedido y su detalle
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="tipoFolio"></param>
        public PedidoInfo Crear(PedidoInfo pedido,TipoFolio tipoFolio)
        {
            try
            {
                Logger.Info();
                var pedisosBl = new PedidosBL();
                return pedisosBl.Crear(pedido,tipoFolio);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtener Los Pedidos programados y parciales
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public List<PedidoInfo> ObtenerPedidosProgramadosYParciales(PedidoInfo pedido)
        {
            try
            {
                Logger.Info();
                var pedisosBl = new PedidosBL();
                return pedisosBl.ObtenerPedidosProgramadosYParciales(pedido);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }


        /// <summary>
        /// Obtiene todos los Pedidos filtrando por Activo o Inactivo.
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public List<PedidoInfo> ObtenerTodos(int organizacionId,EstatusEnum estatus)
        {
            List<PedidoInfo> pedidos = null;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                pedidos = pedidosBl.ObtenerPedidosTodos(organizacionId,estatus);
                
            }catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }

            return pedidos;             
        }

        /// <summary>
        /// Obtiene los pedidos paginados filtrando por folio.
        /// </summary>
        /// <param name="paginacion"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public ResultadoInfo<PedidoInfo> ObtenerPedidosPorFolioPaginado(PaginacionInfo paginacion,PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;

            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosPorFolioPaginado(paginacion, pedido);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }


        /// <summary>
        /// Obtiene los pedidos paginados filtrando por folio y que los pedidos esten solicitados o programados.
        /// </summary>
        /// <param name="paginacion"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public ResultadoInfo<PedidoInfo> ObtenerPedidosSolicitadosProgramados(PaginacionInfo paginacion, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;

            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosSolicitadosProgramados(paginacion, pedido);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene el pedido por folio pedido.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public PedidoInfo ObtenerPedidoPorFolioPedido(PedidoInfo pedido)
        {
            try
            {
                if (pedido.FolioPedido > 0)
                {
                    var pedidosBl = new PedidosBL();
                    pedido = pedidosBl.ObtenerPedidoPorFolio(pedido);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return pedido;
        }

        /// <summary>
        /// Obtiene el pedido por folio pedido.
        /// </summary>
        /// <param name="folioPedido"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public PedidoInfo ObtenerPedidoPorFolio(int folioPedido,int organizacionId)
        {
            PedidoInfo pedido = null;

            try
            {
                if (folioPedido > 0)
                {
                    var pedidosBl = new PedidosBL();
                    pedido = pedidosBl.ObtenerPedidoPorFolio(folioPedido,organizacionId);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return pedido;
        }

        /// <summary>
        /// Obtiene los pedidos programados para 
        /// la funcionalidad de Calidad Pase a Proceso
        /// </summary>
        /// <param name="paginacion"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public ResultadoInfo<PedidoInfo> ObtenerPedidosProgramadosPaginado(PaginacionInfo paginacion, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;

            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosProgramadosPaginado(paginacion, pedido);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene los pedidos programados para 
        /// la funcionalidad de Calidad Pase a Proceso
        /// </summary>
        /// <returns></returns>
        public PedidoInfo ObtenerPedidosProgramadosPorFolioPedido(int folioPedido, int organizacionID)
        {
            PedidoInfo resultado;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosProgramadosPorFolioPedido(folioPedido, organizacionID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene folio pedido con estatus programado
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public ResultadoInfo<PedidoInfo> ObtenerPorFolioProgramadoPaginado(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPorFolioProgramadoPaginado(pagina, pedido);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un pedido por folio
        /// </summary>
        /// <returns></returns>
        public PedidoInfo ObtenerPedidoPorFolio(PedidoInfo pedidoInfo)
        {
            PedidoInfo pedido;

            try
            {
                    var pedidosBl = new PedidosBL();
                    pedido = pedidosBl.ObtenerPedidoPorFolio(pedidoInfo);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return pedido;
        }

        /// <summary>
        /// Obtiene pedidos por filtro
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public ResultadoInfo<PedidoInfo> ObtenerPedidosPorFiltro(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosPorFiltro(pagina, pedido);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene pedidos por filtro
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public ResultadoInfo<PedidoInfo> ObtenerPedidosPorFiltroCancelacion(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosPorFiltroCancelacion(pagina, pedido);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Metodo que obtiene un pedido por ticket
        /// </summary>
        /// <param name="pesajeMateriaPrimaInfo"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public PedidoInfo ObtenerPedidoPorTicketPesaje(PesajeMateriaPrimaInfo pesajeMateriaPrimaInfo, int organizacionId)
        {
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                PedidoInfo result = pedidosBl.ObtenerPedidoPorTicketPesaje(pesajeMateriaPrimaInfo, organizacionId);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Metodo que actualiza el estatus de un pedido
        /// </summary>
        public void ActualizarEstatusPedido(PedidoInfo pedidoInfo)
        {
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosBL();
                pedidosDal.ActualizarEstatusPedido(pedidoInfo);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
        /// <summary>
        /// Obtiene pedidos por pagina
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public ResultadoInfo<PedidoInfo> ObtenerPedidosCompletoPaginado(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosCompletoPaginado(pagina, pedido);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene pedido por folio
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public PedidoInfo ObtenerPedidosCompleto(PedidoInfo pedido)
        {
            PedidoInfo resultado;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosCompleto(pedido);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene pedido por folio
        /// </summary>
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        public List<PedidoPendienteLoteModel> ObtenerPedidosPendientesPorLote(int almacenInventarioLoteID)
        {
            List<PedidoPendienteLoteModel> resultado;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosPendientesPorLote(almacenInventarioLoteID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene pedido por folio
        /// </summary>
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        public List<PedidoPendienteLoteModel> ObtenerPedidosProgramadosPorLote(int almacenInventarioLoteID)
        {
            List<PedidoPendienteLoteModel> resultado;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosProgramadosPorLote(almacenInventarioLoteID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene pedido por folio
        /// </summary>
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        public List<PedidoPendienteLoteModel> ObtenerPedidosEntregadosPorLote(int almacenInventarioLoteID)
        {
            List<PedidoPendienteLoteModel> resultado;
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                resultado = pedidosBl.ObtenerPedidosEntregadosPorLote(almacenInventarioLoteID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        public int ObtenerPedidosProgramadosPorLoteCantidadProgramada(int almacenInventarioLoteId)
        {
            try
            {
                Logger.Info();
                var pedidosBl = new PedidosBL();
                int result = pedidosBl.ObtenerPedidosProgramadosPorLoteCantidadProgramada(almacenInventarioLoteId);
                return result;
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
