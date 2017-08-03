using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class RecepcionMateriaPrimaPL
    {
        /// <summary>
        /// Obtener los pedidos parciales
        /// </summary>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public List<PedidoInfo> ObtenerPedidosParciales(PedidoInfo pedido)
        {
            try
            {
                Logger.Info();
                var recepcionMateriaPrimaBl = new RecepcionMateriaPrimaBL();
                return recepcionMateriaPrimaBl.ObtenerPedidosParciales(pedido);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
        /// <summary>
        /// Obtiene el surtido del pedido
        /// </summary>
        /// <param name="pedido">Debe de contener el folio del pedido</param>
        /// <returns></returns>
        public List<SurtidoPedidoInfo> ObtenerSurtidoPedido(PedidoInfo pedido)
        {
            try
            {
                Logger.Info();
                var recepcionMateriaPrimaBl = new RecepcionMateriaPrimaBL();
                return recepcionMateriaPrimaBl.ObtenerSurtidoPedido(pedido);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }
        /// <summary>
        /// Actualiza la recepcion de materia prima
        /// </summary>
        /// <param name="listaSurtido"></param>
        /// <param name="pedido"></param>
        /// <returns></returns>
        public bool ActualizarRecepcionMateriaPrima(List<SurtidoPedidoInfo> listaSurtido, PedidoInfo pedido)
        {
            try
            {
                Logger.Info();
                var recepcionMateriaPrimaBl = new RecepcionMateriaPrimaBL();
                return recepcionMateriaPrimaBl.ActualizarRecepcionMateriaPrima(listaSurtido, pedido);
            }
            catch (ExcepcionGenerica exg)
            {
                Logger.Error(exg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }


        /// <summary>
        /// Genera el documento impreso de la boleta de recepción una vez que paso por la bascula.
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <param name="entradaProducto"></param>
        public void ImprimirBoletaRecepcion(ImpresionBoletaRecepcionInfo datosReporte,EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                var recepcionMateriaPrimaBl = new RecepcionMateriaPrimaBL();
                
                recepcionMateriaPrimaBl.ImpresionBoletaRecepcion(datosReporte,entradaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(),ex);
            }
            
        }
    }
}
