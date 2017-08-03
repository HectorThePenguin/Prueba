using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class PedidoDetallePL
    {
        /// <summary>
        /// Obtener el detalle del pedido ingresado
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public List<PedidoDetalleInfo> ObtenerDetallePedido(PedidoInfo pedido)
        {
            try
            {
                Logger.Info();
                var pedisosBl = new PedidoDetalleBL();
                return pedisosBl.ObtenerDetallePedido(pedido);
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
        /// Obtener el detalle del pedido ingresado
        /// </summary>
        /// <param name="pedidoDetalleId"></param>
        /// <returns></returns>
        public PedidoDetalleInfo ObtenerDetallePedidoPorId(int pedidoDetalleId)
        {
            try
            {
                Logger.Info();
                var pedisosBl = new PedidoDetalleBL();
                return pedisosBl.ObtenerDetallePedidoPorId(pedidoDetalleId);
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
