using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL 
{
    public class EntradaProductoMuestraBL
    {
        /// <summary>
        /// Obtiene la lista de indicadores de cada entrada producto
        /// </summary>
        /// <param name="entradaProductoDetalleId"></param>
        /// <returns>Lista de EntradaProductoInfo</returns>
        internal List<EntradaProductoMuestraInfo> ObtenerEntradaProductoMuestraPorIdEntradaDetalle(int entradaProductoDetalleId)
        {
            List<EntradaProductoMuestraInfo> listaEntradaProducto;

            try
            {
                var entradaProductoDal = new EntradaProductoMuestraDAL();
                listaEntradaProducto = entradaProductoDal.ObtenerEntradaProductoMuestraPorEntradaDetalleId(entradaProductoDetalleId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProducto;
        }


        /// <summary>
        /// Almacena la(s) muestra(s) del detalle de una entrada de producto.
        /// </summary>
        /// <param name="entradaProducto"></param>
        internal void GuardarEntradaProductoMuestra(EntradaProductoInfo entradaProducto)
        {
            try
            {
                var entradaProductoDal = new EntradaProductoMuestraDAL();

                entradaProductoDal.GuardarEntradaProductoMuestra(entradaProducto);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Guarda la actualizacion de los descuentos
        /// </summary>
        /// <param name="indicadores"></param>
        /// <returns></returns>
        internal bool GuardarActualizacionProductos(EntradaProductoDetalleInfo indicadores)
        {
            try
            {
                var entradaProductoMuestraDal = new EntradaProductoMuestraDAL();
                return entradaProductoMuestraDal.GuardarActualizacionProductos(indicadores);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }
    }
}
