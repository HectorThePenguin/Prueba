using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class EntradaProductoMuestraDAL:DALBase
    {
        /// <summary>
        /// Obtiene la lista de muestras de cada indicador de cada entrada producto
        /// </summary>
        /// <param name="entradaProductoDetalleId"></param>
        /// <returns></returns>
        internal List<EntradaProductoMuestraInfo> ObtenerEntradaProductoMuestraPorEntradaDetalleId(int entradaProductoDetalleId)
        {
            List<EntradaProductoMuestraInfo> listaEntradaProductos = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoMuestraDAL.ObtenerParametrosEntradaProductoMuestraPorIdEntradaDetalle(entradaProductoDetalleId);
                DataSet ds = Retrieve("EntradaProductoMuestra_ObtenerPorEntradaDetalleId", parametros);
                if (ValidateDataSet(ds))
                {
                    listaEntradaProductos = MapEntradaProductoMuestrasDAL.ObtenerEntradaProductoMuestraPorEntradaDetalleId(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaEntradaProductos;
        }

        
        /// <summary>
        /// Guarda el detalle de una entrada producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal bool GuardarEntradaProductoMuestra(EntradaProductoInfo entradaProducto)
        {
            bool resultado = true;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoMuestraDAL.ObtenerParametrosGuardarEntradaProductoMuestra(entradaProducto);

                Create("EntradaProductoMuestra_Crear", parameters);
            }
            catch (Exception ex)
            {
                resultado = false;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }


        /// <summary>
        /// Actualiza el descuento de las muestras por EntradaProductoDetalleID
        /// </summary>
        /// <param name="indicadores"></param>
        /// <returns></returns>
        internal bool GuardarActualizacionProductos(EntradaProductoDetalleInfo indicadores)
        {
            bool resultado = true;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoMuestraDAL.ObtenerParametrosGuardarActualizacionProductos(indicadores);
                Create("EntradaProductoMuestra_ActualizarDescuentos", parameters);
            }
            catch (Exception ex)
            {
                resultado = false;
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return resultado;
        }
    }
}
