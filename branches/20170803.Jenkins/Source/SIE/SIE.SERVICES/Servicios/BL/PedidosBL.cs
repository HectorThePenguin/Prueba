using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Infos;
using SIE.Base.Log;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class PedidosBL
    {
        /// <summary>
        /// Crea un pedido y su detalle
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="tipoFolio"></param>
        /// <returns></returns>
        internal PedidoInfo Crear(PedidoInfo pedido, TipoFolio tipoFolio)
        {
            PedidoInfo pedidoGuardado = new PedidoInfo();
            try
            {
                Logger.Info();

                var pedidosDal = new PedidosDAL();
                using (var transaccion = new TransactionScope())
                {
                    pedidoGuardado = pedidosDal.Crear(pedido, tipoFolio);
                    if (pedidoGuardado != null)
                    {
                        var pedidosDetalleBl = new PedidoDetalleBL();
                        foreach (PedidoDetalleInfo pedidoDetalle in pedido.DetallePedido)
                        {
                            pedidoDetalle.UsuarioCreacion = pedido.UsuarioCreacion;
                            pedidoDetalle.PedidoId = pedidoGuardado.PedidoID;
                            pedidoDetalle.Observaciones = pedido.Observaciones;
                        }
                        pedidosDetalleBl.Crear(pedido.DetallePedido);
                        pedidoGuardado = ObtenerPedidoPorFolio(pedidoGuardado);
                    }
                    transaccion.Complete();
                }
                return pedidoGuardado;
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return pedidoGuardado;
        }

        /// <summary>
        /// Obtiene los pedidos Programados y parciales
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal List<PedidoInfo> ObtenerPedidosProgramadosYParciales(PedidoInfo pedido)
        {
            var listaPedidos = new List<PedidoInfo>();
            try
            {
                Logger.Info();

                var pedidosDal = new PedidosDAL();
                listaPedidos = pedidosDal.ObtenerPedidosProgramadosYParciales(pedido);
                if (listaPedidos != null)
                {
                    foreach (PedidoInfo listaPedido in listaPedidos)
                    {
                        if (listaPedido != null)
                        {
                            EstableceDetallePedido(listaPedido);
                        }
                       
                    }
                }
                return listaPedidos;
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return listaPedidos;
        }

        /// <summary>
        /// Establece los datos y el detalle del pedido
        /// </summary>
        /// <param name="pedido"></param>
        private void EstableceDetallePedido(PedidoInfo pedido)
        {
            var pedidoDetalleDl = new PedidoDetalleBL();
            var organizacionBl = new OrganizacionBL();
            var almacenBl = new AlmacenBL();
            var usuarioBl = new UsuarioBL();

            var almacen = pedido.Almacen;
            var organizacion = pedido.Organizacion;
            var usuarioCreacion = pedido.UsuarioCreacion;
            var usuarioModificacion = pedido.UsuarioModificacion;

            if (almacen != null && almacen.AlmacenID > 0)
            {
                pedido.Almacen = almacenBl.ObtenerPorID(almacen.AlmacenID);
            }

            if (organizacion != null && organizacion.OrganizacionID > 0)
            {
                pedido.Organizacion = organizacionBl.ObtenerPorID(organizacion.OrganizacionID);
            }

            if (usuarioCreacion != null && usuarioCreacion.UsuarioID > 0)
            {
                pedido.UsuarioCreacion = usuarioBl.ObtenerPorID(usuarioCreacion.UsuarioID);
            }

            if (usuarioModificacion != null && usuarioModificacion.UsuarioID > 0)
            {
                pedido.UsuarioModificacion = usuarioBl.ObtenerPorID(usuarioModificacion.UsuarioID);
            }

            pedido.DetallePedido = pedidoDetalleDl.ObtenerDetallePedido(pedido);

        }

        /// <summary>
        /// Obtiene todos los pedidos segun el estatus activo indicado.
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        internal List<PedidoInfo> ObtenerPedidosTodos(int organizacionId,EstatusEnum estatus)
        {
            var listaPedidos = new List<PedidoInfo>();
            try
            {
                Logger.Info();

                var pedidosDal = new PedidosDAL();
                listaPedidos = pedidosDal.ObtenerPedidosTodos(organizacionId,estatus);

                if (listaPedidos != null)
                {
                    
                    foreach (PedidoInfo pedido in listaPedidos)
                    {
                        if (pedido != null)
                        {
                            EstableceDetallePedido(pedido);
                        }
                        
                    }
                }

            }catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaPedidos;
        }

        /// <summary>
        /// Obtiene todos los pedidos paginados por el folio pedido.
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedidoInfo"></param>
        /// <returns></returns>
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosPorFolioPaginado(PaginacionInfo pagina, PedidoInfo pedidoInfo)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var listaPedidos = new List<PedidoInfo>();
                var pedidosDal = new PedidosDAL();

                 resultado = pedidosDal.ObtenerPedidosPorFolioPaginado(pagina, pedidoInfo);

                if (resultado != null)
                {
                    listaPedidos = (List<PedidoInfo>)resultado.Lista;

                    if (listaPedidos != null)
                    {

                        foreach (PedidoInfo pedido in listaPedidos)
                        {
                            if (pedido != null)
                            {
                                EstableceDetallePedido(pedido);
                            }
                            
                        }
                    }
                    
                }
                

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }


        /// <summary>
        /// Obtiene todos los pedidos que se encuentran en estatus Solicitados o
        /// Programados.
        /// </summary>
        /// <returns></returns>
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosSolicitadosProgramados(PaginacionInfo paginacion,PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;

            
            try
            {
                resultado = ObtenerPedidosPorFolioPaginado(paginacion, pedido);

                if (resultado != null)
                {
                    var listaPedidos = resultado.Lista;
                    if (listaPedidos != null)
                    {
                        listaPedidos = (from pedidos in listaPedidos
                                        where (pedidos.EstatusPedido.EstatusId == (int)Estatus.PedidoSolicitado
                                               || pedidos.EstatusPedido.EstatusId == (int)Estatus.PedidoProgramado)
                                        select pedidos).ToList();

                        resultado.Lista = listaPedidos;
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }


        /// <summary>
        /// Obtiene el pedido por el folio pedido.
        /// </summary>
        /// <param name="folioPedido"></param>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal PedidoInfo ObtenerPedidoPorFolio(int folioPedido,int organizacionId)
        {
            PedidoInfo pedido;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();

                pedido = pedidosDal.ObtenerPedidoPorFolio(folioPedido,organizacionId);

                if (pedido != null)
                {
                    EstableceDetallePedido(pedido);
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
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosProgramadosPaginado(PaginacionInfo paginacion, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
                resultado = pedidosDal.ObtenerPedidosProgramadosPaginado(paginacion, pedido);
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
        internal PedidoInfo ObtenerPedidosProgramadosPorFolioPedido(int folioPedido, int organizacionID)
        {
            PedidoInfo resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
                resultado = pedidosDal.ObtenerPedidosProgramadosPorFolioPedido(folioPedido, organizacionID);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        internal ResultadoInfo<PedidoInfo> ObtenerPorFolioProgramadoPaginado(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
                resultado = pedidosDal.ObtenerPorFolioProgramadoPaginado(pagina, pedido);
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
        internal PedidoInfo ObtenerPedidoPorFolio(PedidoInfo pedidoInfo)
        {
            PedidoInfo pedido;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();

                pedido = pedidosDal.ObtenerPedidoPorFolio(pedidoInfo);

                if (pedido != null)
                {
                    EstableceDetallePedido(pedido);
                    if (pedido.DetallePedido != null)
                    {
                        pedido.DetallePedido =
                            pedido.DetallePedido.Where(pedidoDetalleInfo => pedidoDetalleInfo.Producto != null).ToList();


                        if (pedido.DetallePedido.Count == 0)
                        {
                            pedido = null;
                        }
                    }
                    else
                    {
                        pedido = null;
                    }
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
        /// Obtiene folios pedido por filtro
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedidoInfo"></param>
        /// <returns></returns>
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosPorFiltro(PaginacionInfo pagina, PedidoInfo pedidoInfo)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();

                resultado = pedidosDal.ObtenerPedidosPorFiltro(pagina, pedidoInfo);

                if (resultado != null)
                {
                    var listaPedidos = (List<PedidoInfo>)resultado.Lista;

                    if (listaPedidos != null)
                    {

                        foreach (var pedido in listaPedidos.Where(pedido => pedido != null))
                        {
                            EstableceDetallePedido(pedido);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene folios pedido por filtro
        /// </summary>
        /// <param name="pagina"></param>
        /// <param name="pedidoInfo"></param>
        /// <returns></returns>
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosPorFiltroCancelacion(PaginacionInfo pagina, PedidoInfo pedidoInfo)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();

                resultado = pedidosDal.ObtenerPedidosPorFiltroCancelacion(pagina, pedidoInfo);

                if (resultado != null)
                {
                    var listaPedidos = (List<PedidoInfo>)resultado.Lista;

                    if (listaPedidos != null)
                    {

                        foreach (var pedido in listaPedidos.Where(pedido => pedido != null))
                        {
                            EstableceDetallePedido(pedido);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Metodo que actualiza el estatus de un pedido
        /// </summary>
        internal void ActualizarEstatusPedido(PedidoInfo pedidoInfo)
        {
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
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
                var pedidosDal = new PedidosDAL();
                var pedidoInfo = pedidosDal.ObtenerPedidoPorTicketPesaje(pesajeMateriaPrimaInfo, organizacionId);

                if (pedidoInfo != null)
                {
                    EstableceDetallePedido(pedidoInfo);
                }
                return pedidoInfo;
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
        internal ResultadoInfo<PedidoInfo> ObtenerPedidosCompletoPaginado(PaginacionInfo pagina, PedidoInfo pedido)
        {
            ResultadoInfo<PedidoInfo> resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
                resultado = pedidosDal.ObtenerPedidosCompletoPaginado(pagina, pedido);
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
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal PedidoInfo ObtenerPedidosCompleto(PedidoInfo pedido)
        {
            PedidoInfo resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
                resultado = pedidosDal.ObtenerPedidosCompleto(pedido);
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
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        internal List<PedidoPendienteLoteModel> ObtenerPedidosPendientesPorLote(int almacenInventarioLoteID)
        {
            List<PedidoPendienteLoteModel> resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
                resultado = pedidosDal.ObtenerPedidosPendientesPorLote(almacenInventarioLoteID);
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
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        internal List<PedidoPendienteLoteModel> ObtenerPedidosProgramadosPorLote(int almacenInventarioLoteID)
        {
            List<PedidoPendienteLoteModel> resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
                resultado = pedidosDal.ObtenerPedidosProgramadosPorLote(almacenInventarioLoteID);
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
        /// <param name="almacenInventarioLoteID"></param>
        /// <returns></returns>
        internal List<PedidoPendienteLoteModel> ObtenerPedidosEntregadosPorLote(int almacenInventarioLoteID)
        {
            List<PedidoPendienteLoteModel> resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
                resultado = pedidosDal.ObtenerPedidosEntregadosPorLote(almacenInventarioLoteID);
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
            int resultado;
            try
            {
                Logger.Info();
                var pedidosDal = new PedidosDAL();
                resultado = pedidosDal.ObtenerPedidosProgramadosPorLoteCantidadProgramada(almacenInventarioLoteId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }
    }
}
