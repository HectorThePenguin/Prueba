using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class PedidoDetalleBL
    {
        /// <summary>
        /// Obtiene los pedidos Programados y parciales
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal List<PedidoDetalleInfo> ObtenerDetallePedido(PedidoInfo pedido)
        {
            try
            {
                Logger.Info();
                var pedidoDal = new PedidoDetalleDAL();
                var pedidoDetalle = pedidoDal.ObtenerDetallePedido(pedido);

                if (pedidoDetalle != null)
                {
                    var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                    var programacionMateriaPrimaBl = new ProgramacionMateriaPrimaBL();
                    var productoBl = new ProductoBL();

                    foreach (PedidoDetalleInfo pedidoDetalleInfo in pedidoDetalle)
                    {
                        pedidoDetalleInfo.InventarioLoteDestino =
                            almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                                pedidoDetalleInfo.InventarioLoteDestino.AlmacenInventarioLoteId);

                        pedidoDetalleInfo.ProgramacionMateriaPrima =
                            programacionMateriaPrimaBl.ObtenerProgramacionMateriaPrima(pedidoDetalleInfo);

                        pedidoDetalleInfo.Producto = productoBl.ObtenerPorID(pedidoDetalleInfo.Producto);
                    }
                }
                return pedidoDetalle;
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pedidoDetalle"></param>
        internal bool Crear(List<PedidoDetalleInfo> pedidoDetalle)
        {
            bool resultado = true;
            try
            {
                var pedidoDetalleDal = new PedidoDetalleDAL();
                resultado = pedidoDetalleDal.Crear(pedidoDetalle);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }

        /// <summary>
        /// Obtiene el detalle del pedido indicado
        /// </summary>
        /// <param name="pedidoDetalleId"></param>
        /// <returns></returns>
        internal PedidoDetalleInfo ObtenerDetallePedidoPorId(int pedidoDetalleId)
        {
            try
            {
                Logger.Info();
                var pedidoDal = new PedidoDetalleDAL();
                var pedidoDetalle = pedidoDal.ObtenerDetallePedidoPorId(pedidoDetalleId);

                if (pedidoDetalle != null)
                {
                    var almacenInventarioLoteBl = new AlmacenInventarioLoteBL();
                    var programacionMateriaPrimaBl = new ProgramacionMateriaPrimaBL();
                    var productoBl = new ProductoBL();

                    pedidoDetalle.InventarioLoteDestino =
                        almacenInventarioLoteBl.ObtenerAlmacenInventarioLotePorId(
                            pedidoDetalle.InventarioLoteDestino.AlmacenInventarioLoteId);

                    pedidoDetalle.ProgramacionMateriaPrima =
                        programacionMateriaPrimaBl.ObtenerProgramacionMateriaPrima(pedidoDetalle);

                    pedidoDetalle.Producto = productoBl.ObtenerPorID(pedidoDetalle.Producto);
                }
                return pedidoDetalle;
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
