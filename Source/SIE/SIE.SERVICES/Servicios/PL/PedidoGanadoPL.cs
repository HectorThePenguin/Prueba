using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class PedidoGanadoPL
    {
        /// <summary>
        /// Metrodo Para Guardar en  la tabla PedidoGanado
        /// </summary>
        public PedidoGanadoInfo GuardarPedidoGanado(PedidoGanadoInfo pedidoGanadoInfo)
        {
            PedidoGanadoInfo result;
            try
            {
                Logger.Info();
                var pedidoGanadoBL = new PedidoGanadoBL();
                result = pedidoGanadoBL.GuardarPedidoGanado(pedidoGanadoInfo);

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
        /// Metodo para obtener el pedido semanal
        /// </summary>
        /// <param name="pedidoGanadoInfo"></param>
        /// <returns></returns>
        public PedidoGanadoInfo ObtenerPedidoSemanal(PedidoGanadoInfo pedidoGanadoInfo)
        {
            PedidoGanadoInfo result;
            try
            {
                Logger.Info();
                var pedidoGanadoBL = new PedidoGanadoBL();
                result = pedidoGanadoBL.ObtenerPedidoGanadoSemanal(pedidoGanadoInfo);
                if (result != null)
                {
                    result.ListaSolicitudes = ObtenerPedidoGanadoEspejoPorPedidoID(result); 
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
            return result;

        }

        /// <summary>
        /// Metodo para obtener un listado de animales por Codigo de Corral
        /// </summary>
        public List<PedidoGanadoEspejoInfo> ObtenerPedidoGanadoEspejoPorPedidoID(PedidoGanadoInfo pedidoGanadoInfo)
        {
            List<PedidoGanadoEspejoInfo> result;
            try
            {
                Logger.Info();
                var pedidoGanadoBL = new PedidoGanadoBL();
                result = pedidoGanadoBL.ObtenerPedidoGanadoEspejoPorPedidoGanadoID(pedidoGanadoInfo);
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
        public PedidoGanadoEspejoInfo GuardarPedidoGanadoEspejo(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            PedidoGanadoEspejoInfo result;
            try
            {
                Logger.Info();
                var pedidoGanadoBL = new PedidoGanadoBL();
                result = pedidoGanadoBL.GuardarPedidoGanadoEspejo(pedidoGanadoEspejoInfo);

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
        /// Metodo Para actuaualizar el estatus PedidoGanadoEspejo
        /// </summary>
        public void ActualizarPedidoGanadoEspejoEstatus(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                Logger.Info();
                var pedidoGanadoBL = new PedidoGanadoBL();
                pedidoGanadoBL.ActualizarPedidoGanadoEspejoEstatus(pedidoGanadoEspejoInfo);

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
        /// Metodo Para actuaualizar el estatus PedidoGanadoEspejo
        /// </summary>
        public void ActualizarPedidoGanado(PedidoGanadoEspejoInfo pedidoGanadoEspejoInfo)
        {
            try
            {
                Logger.Info();
                var pedidoGanadoBL = new PedidoGanadoBL();
                pedidoGanadoBL.ActualizarPedidoGanado(pedidoGanadoEspejoInfo);

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
