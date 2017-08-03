using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using BLToolkit.Data.Sql;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class PedidoDetalleDAL:DALBase
    {
        /// <summary>
        /// Obtiene los pedidos con estatus programados y parciales
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal List<PedidoDetalleInfo> ObtenerDetallePedido(PedidoInfo pedido)
        {
            List<PedidoDetalleInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPedidoDetalleDAL.ObtenerParametrosObtenerDetallePedido(pedido);
                DataSet ds = Retrieve("PedidoDetalle_ObtenerPorPedido", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPedidoDetalleDAL.ObtenerDetallePedido(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }

        /// <summary>
        /// Crea el detalle del pedido
        /// </summary>
        /// <param name="pedidoDetalle"></param>
        /// <returns></returns>
        internal bool Crear(List<PedidoDetalleInfo> pedidoDetalle)
        {
            bool retorno = true;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPedidoDetalleDAL.ObtenerParametrosCrear(pedidoDetalle);

                Create("PedidoDetalle_Crear", parameters);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return retorno;
        }

        /// <summary>
        /// Obtiene el detalle del pedido indicado.
        /// </summary>
        /// <param name="pedidoDetalleId"></param>
        /// <returns></returns>
        internal PedidoDetalleInfo ObtenerDetallePedidoPorId(int pedidoDetalleId)
        {
            PedidoDetalleInfo result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxPedidoDetalleDAL.ObtenerParametrosObtenerDetallePedidoPorId(pedidoDetalleId);
                DataSet ds = Retrieve("PedidoDetalle_ObtenerPorDetallePedidoId", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapPedidoDetalleDAL.ObtenerDetallePedidoPorId(ds);
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return result;
        }
    }
}
