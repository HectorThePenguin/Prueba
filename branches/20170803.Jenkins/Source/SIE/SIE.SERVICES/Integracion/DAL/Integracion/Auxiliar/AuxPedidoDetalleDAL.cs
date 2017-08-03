using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxPedidoDetalleDAL
    {
        /// <summary>
        /// Obtiene los parametros necesarios para obtener el detalle del pedido.
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDetallePedido(PedidoInfo pedido)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@PedidoId", pedido.PedidoID}
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
        /// Obtiene el diccionario para crear el detalle
        /// </summary>
        /// <param name="pedidoDetalle"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosCrear(List<PedidoDetalleInfo> pedidoDetalle)
        {
            try
            {
                Logger.Info();

                var xml =
                   new XElement("ROOT",
                       from pedidoDetalleInfo in pedidoDetalle
                           select 
                                new XElement("PedidoDetalle",
                                   new XElement("PedidoID", pedidoDetalleInfo.PedidoId),
                                   new XElement("ProductoID", pedidoDetalleInfo.Producto.ProductoId),
                                   new XElement("CantidadSolicitada", pedidoDetalleInfo.CantidadSolicitada),
                                   new XElement("AlmacenInventarioLoteID", pedidoDetalleInfo.InventarioLoteDestino.AlmacenInventarioLoteId),
                                   new XElement("Observaciones", pedidoDetalleInfo.Observaciones),
                                   new XElement("UsuarioCreacionID", pedidoDetalleInfo.UsuarioCreacion.UsuarioCreacionID)
                           )
                );

                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@XMLPedidoDetalle", xml.ToString()}
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
        /// Obtiene los parametros necesarios para obtener el detalle del pedido.
        /// </summary>
        /// <param name="pedidoDetalleId"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosObtenerDetallePedidoPorId(int pedidoDetalleId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@PedidoDetalleId", pedidoDetalleId}
                    };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
