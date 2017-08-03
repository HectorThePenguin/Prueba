using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class EntradaProductoDetalleBL 
    {
        /// <summary>
        /// Obtiene la lista de indicadores de cada entrada producto
        /// </summary>
        /// <param name="entradaProductoId"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        internal List<EntradaProductoDetalleInfo> ObtenerDetalleEntradaProductosPorIdEntrada(int entradaProductoId)
        {
            List<EntradaProductoDetalleInfo> listaEntradaProducto;

            try
            {
                var entradaProductoDAL = new EntradaProductoDetalleDAL();
                listaEntradaProducto = entradaProductoDAL.ObtenerDetalleEntradaProductosPorIdEntrada(entradaProductoId);

                if (listaEntradaProducto != null)
                {
                    foreach (EntradaProductoDetalleInfo entradaProductoDetalleInfo in listaEntradaProducto)
                    {
                        var indicadorBl = new IndicadorBL();
                        entradaProductoDetalleInfo.Indicador =
                           indicadorBl.ObtenerPorId(entradaProductoDetalleInfo.Indicador);

                        var entradaProductoMuestraBl = new EntradaProductoMuestraBL();
                        entradaProductoDetalleInfo.ProductoMuestras = entradaProductoMuestraBl.ObtenerEntradaProductoMuestraPorIdEntradaDetalle(entradaProductoDetalleInfo.EntradaProductoDetalleId);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProducto;
        }

        /// <summary>
        /// Almacena el detalle de una entrada producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        internal int GuardarProductoDetalle(EntradaProductoInfo entradaProducto)
        {
            int resultado = 0;
            try
            {
                var entradaProductoDal = new EntradaProductoDetalleDAL();

                resultado = entradaProductoDal.GuardarEntradaProductoDetalle(entradaProducto);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }


        /// <summary>
        /// Actualiza el detalle de una entrada producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        internal void ActualizarProductoDetalle(EntradaProductoInfo entradaProducto)
        {
            try
            {
                var entradaProductoDal = new EntradaProductoDetalleDAL();

                entradaProductoDal.ActualizarEntradaProductoDetalle(entradaProducto);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}
