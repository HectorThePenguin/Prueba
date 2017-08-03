using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class RecepcionMateriaPrimaDAL : DALBase
    {
        /// <summary>
        /// Obtiene los pedidos con estatus parcial
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal List<PedidoInfo> ObtenerPedidosParciales(PedidoInfo pedido)
        {
            List<PedidoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRecepcionMateriaPrimaDAL.ObtenerParametrosPedidosParciales(pedido);
                DataSet ds = Retrieve("EntradaMateriaPrima_ObtenerPedidosPorEstatus", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRecepcionMateriaPrimaDAL.ObtenerPedidosParcial(ds);
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
        /// Obtiene el surtido de un pedido
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        internal List<SurtidoPedidoInfo> ObtenerSurtidoPedidos(PedidoInfo pedido)
        {
            List<SurtidoPedidoInfo> result = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRecepcionMateriaPrimaDAL.ObtenerParametrosSurtidoPedidos(pedido);
                DataSet ds = Retrieve("EntradaMateriaPrima_ObtenerSurtidoPorPedido", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapRecepcionMateriaPrimaDAL.ObtenerSurtidosPedido(ds, pedido);
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
        /// Actualiza ell pesaje de la materia prima
        /// </summary>
        /// <param name="listaSurtido"></param>
        /// <param name="usuarioModificacionId"></param>
        /// <returns></returns>
        internal bool ActualizarPesajeMateriaPrima(List<SurtidoPedidoInfo> listaSurtido, int usuarioModificacionId)
        {

            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxRecepcionMateriaPrimaDAL.ObtenerParametrosActualizarPesajeMateriaPrima(listaSurtido, usuarioModificacionId);
                Update("RecepcionMateriaPrima_ActualizarPesajeMateriaPrima", parameters);
                return true;

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

        }
    }
}
