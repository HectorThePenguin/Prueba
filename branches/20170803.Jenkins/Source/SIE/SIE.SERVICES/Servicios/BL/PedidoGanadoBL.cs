using System.Transactions;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SIE.Services.Servicios.BL
{
    internal class PedidoGanadoBL
    {
        /// <summary>
        /// Metrodo Para Guardar en  la tabla PedidoGanado
        /// </summary>
        internal PedidoGanadoInfo GuardarPedidoGanado(PedidoGanadoInfo pedidoGanadoInfo)
        {
            PedidoGanadoInfo result;
            try
            {
                Logger.Info();
                var pedidoGanadoDAL = new PedidoGanadoDAL();
                result = pedidoGanadoDAL.GuardarPedidoGanado(pedidoGanadoInfo);

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
            return result;
        }

        /// <summary>
        /// Metodo para obtener el pedido de ganado semanal
        /// </summary>
        /// <param name="pedidoGanadoInfo"></param>
        /// <returns></returns>
        internal PedidoGanadoInfo ObtenerPedidoGanadoSemanal(PedidoGanadoInfo pedidoGanadoInfo)
        {
            PedidoGanadoInfo result;
            try
            {
                Logger.Info();
                var pedidoGanadoDAL = new PedidoGanadoDAL();
                result = pedidoGanadoDAL.ObtenerPedidoGanadoSemanal(pedidoGanadoInfo);
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
            return result;
        }

        /// <summary>
        /// Obtiene la lista de solicutdes de cambio por PedidoGanadoID
        /// </summary>
        /// <returns></returns>
        internal List<PedidoGanadoEspejoInfo> ObtenerPedidoGanadoEspejoPorPedidoGanadoID(PedidoGanadoInfo pedidoGanadoInfo)
        {
            List<PedidoGanadoEspejoInfo> result;
            try
            {
                Logger.Info();
                var pedidoGanadoDAL = new PedidoGanadoDAL();
                result = pedidoGanadoDAL.ObtenerPedidoGanadoEspejoPorPedidoGanadoID(pedidoGanadoInfo);
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
            return result;
        }

        /// <summary>
        /// Metrodo Para Guardar en  la tabla PedidoGanadoEspejo
        /// </summary>
        internal PedidoGanadoEspejoInfo GuardarPedidoGanadoEspejo(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            PedidoGanadoEspejoInfo result;
            try
            {
                Logger.Info();
                var pedidoGanadoDAL = new PedidoGanadoDAL();
                result = pedidoGanadoDAL.GuardarPedidoGanadoEspejo(pedidoGanadoEspejoInfo);

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
            return result;
        }

        /// <summary>
        /// Metodo Para actuaualizar el estatus PedidoGanadoEspejo y actualiza la tabla PedidoGanado
        /// </summary>
        internal void ActualizarPedidoGanadoEspejoEstatus(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                using (var transaccion = new TransactionScope())
                {
                    Logger.Info();
                    var pedidoGanadoDAL = new PedidoGanadoDAL();
                    pedidoGanadoDAL.ActualizarPedidoGanadoEspejoEstatus(pedidoGanadoEspejoInfo);
                    if (pedidoGanadoEspejoInfo.Estatus == true)
                    {
                        pedidoGanadoDAL.ActualizarPedidoGanado(pedidoGanadoEspejoInfo);
                    }
                    transaccion.Complete();
                }
               

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
        /// Metodo Para  actualizar la tabla PedidoGanado
        /// </summary>
        internal void ActualizarPedidoGanado(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                Logger.Info();
                var pedidoGanadoDAL = new PedidoGanadoDAL();
                pedidoGanadoDAL.ActualizarPedidoGanado(pedidoGanadoEspejoInfo);
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
