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
    internal class EntradaProductoDetalleDAL:DALBase 
    {
        /// <summary>
        /// Obtiene la lista de indicadores de cada entrada producto
        /// </summary>
        /// <param name="entradaProductoId"></param>
        /// <returns></returns>
        internal List<EntradaProductoDetalleInfo> ObtenerDetalleEntradaProductosPorIdEntrada(int entradaProductoId)
        {
            List<EntradaProductoDetalleInfo> listaEntradaProductos = null;

            try
            {
                Dictionary<string, object> parametros = AuxEntradaProductoDetalleDAL.ObtenerParametrosEntradaProductoDetalleProductoPorIdEntrada(entradaProductoId);
                DataSet ds = Retrieve("EntradaProductoDetalle_ObtenerPorEntradaId", parametros);
                if (ValidateDataSet(ds))
                {
                    listaEntradaProductos = MapEntradaProductoDetalleDAL.ObtenerDetalleEntradaProductosPorIdEntrada(ds);
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
        internal int GuardarEntradaProductoDetalle(EntradaProductoInfo entradaProducto)
        {
            int entradaProductoDetalleId = 0;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDetalleDAL.ObtenerParametrosGuardarEntradaProductoDetalle(entradaProducto);

                entradaProductoDetalleId = Create("EntradaProductoDetalle_Crear", parameters);

            }catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return entradaProductoDetalleId;
        }


        /// <summary>
        /// Actualiza el detalle de una entrada producto
        /// </summary>
        /// <param name="entradaProducto"></param>
        /// <returns></returns>
        internal void ActualizarEntradaProductoDetalle(EntradaProductoInfo entradaProducto)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxEntradaProductoDetalleDAL.ObtenerParametrosActualizarEntradaProductoDetalle(entradaProducto);

               Update("EntradaProductoDetalle_Actualizar", parameters);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

        }
    }
}
