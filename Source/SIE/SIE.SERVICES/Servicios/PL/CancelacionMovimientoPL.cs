using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System;

namespace SIE.Services.Servicios.PL
{
    public class CancelacionMovimientoPL
    {
        /// <summary>
        /// Funcion que cancela un movimiento de entrada por compra
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool CancelarEntradaCompra(EntradaProductoInfo entradaProducto, string justificacion)
        {
            try
            {
                CancelacionMovimientoBL cancelacionMovimientoBl = new CancelacionMovimientoBL();
                return cancelacionMovimientoBl.CancelarEntradaCompra(entradaProducto,justificacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }

        public bool CancelarEntradaTraspaso(EntradaProductoInfo entradaProducto, string justificacion)
        {
            try
            {
                CancelacionMovimientoBL cancelacionMovimientoBl = new CancelacionMovimientoBL();
                return cancelacionMovimientoBl.CancelarEntradaTraspaso(entradaProducto, justificacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }

        public bool CancelarVentaTraspaso(SalidaProductoInfo salidaProducto,string justificacion)
        {
            try
            {
                CancelacionMovimientoBL cancelacionMovimientoBl = new CancelacionMovimientoBL();
                return cancelacionMovimientoBl.CancelarVentaTraspaso(salidaProducto, justificacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }

        public bool CancelarPedidoTicket(PedidoCancelacionMovimientosInfo pedido, string justificacion)
        {
            try
            {
                CancelacionMovimientoBL cancelacionMovimientoBl = new CancelacionMovimientoBL();
                return cancelacionMovimientoBl.CancelarPedidoTicket(pedido, justificacion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }
    }
}
