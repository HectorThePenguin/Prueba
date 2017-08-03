using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Mapeos
{
    internal class MapPedidoDetalleDAL
    {
        /// <summary>
        /// Obtiene los datos de los pedidos programados y parciales
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static List<PedidoDetalleInfo> ObtenerDetallePedido(DataSet ds)
        {
            List<PedidoDetalleInfo> listaPedidos;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                listaPedidos = (from info in dt.AsEnumerable()
                                select new PedidoDetalleInfo
                                {
                                    PedidoDetalleId = info.Field<int>("PedidoDetalleID"),
                                    PedidoId = info.Field<int>("PedidoID"),
                                    Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID") },
                                    InventarioLoteDestino = new AlmacenInventarioLoteInfo{AlmacenInventarioLoteId = info.Field<int>("InventarioLoteIDDestino")},
                                    CantidadSolicitada = info.Field<decimal>("CantidadSolicitada")
                                }).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return listaPedidos;
        }

        /// <summary>
        /// Obtiene los datos de los pedidos programados y parciales
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        internal static PedidoDetalleInfo ObtenerDetallePedidoPorId(DataSet ds)
        {
            PedidoDetalleInfo detallePedido;
            try
            {
                Logger.Info();
                var dt = ds.Tables[ConstantesDAL.DtDatos];
                detallePedido = (from info in dt.AsEnumerable()
                                select new PedidoDetalleInfo
                                {
                                    PedidoDetalleId = info.Field<int>("PedidoDetalleID"),
                                    PedidoId = info.Field<int>("PedidoID"),
                                    Producto = new ProductoInfo { ProductoId = info.Field<int>("ProductoID") },
                                    InventarioLoteDestino = new AlmacenInventarioLoteInfo { AlmacenInventarioLoteId = info.Field<int>("InventarioLoteIDDestino") },
                                    CantidadSolicitada = info.Field<decimal>("CantidadSolicitada")
                                }).First();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return detallePedido;
        }
    }
}
