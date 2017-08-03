using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;
using System.IO;
using SIE.Services.Integracion.DAL.Excepciones;

namespace SIE.Services.Servicios.PL
{
    public class RecepcionProductoPL
    {
        /// <summary>
        /// Guarda una recepcion de producto
        /// </summary>
        /// <param name="recepcionProducto"></param>
        /// <returns></returns>
        public MemoryStream Guardar(RecepcionProductoInfo recepcionProducto)
        {
            MemoryStream poliza;
            try
            {
                var recepcionProductoBl = new RecepcionProductoBL();
                poliza = recepcionProductoBl.Guardar(recepcionProducto);
            }
            catch (ExcepcionServicio ex)
            {
                Logger.Error(ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw;
            }
            return poliza;
        }

        /// <summary>
        /// Obtiene la recepcion desde la vista del servidor de la organizacion
        /// </summary>
        /// <param name="recepcionProductoCompra"></param>
        /// <param name="organizacionId"></param>
        /// <param name="conexion"></param>
        /// <returns></returns>
        public RecepcionProductoInfo ObtenerRecepcionVista(RecepcionProductoInfo recepcionProductoCompra, int organizacionId, string conexion)
        {
            try
            {
                var recepcionProductoBl = new RecepcionProductoBL();
                return recepcionProductoBl.ObtenerRecepcionVista(recepcionProductoCompra, organizacionId, conexion);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        public RecepcionProductoInfo ObtenerRecepcionPorFolio(RecepcionProductoInfo recepcionProductoCompra)
        {
            try
            {
                var recepcionProductoBl = new RecepcionProductoBL();
                return recepcionProductoBl.ObtenerRecepcionPorFolio(recepcionProductoCompra);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Obtiene una coleccion de recepcion producto
        /// </summary>
        /// <param name="almacenesMovimiento"></param>
        /// <returns></returns>
        public List<RecepcionProductoInfo> ObtenerRecepcionProductoConciliacionPorAlmacenMovimiento(List<AlmacenMovimientoInfo> almacenesMovimiento)
        {
            List<RecepcionProductoInfo> resultado;
            try
            {
                Logger.Info();
                var recepcionProductoBL = new RecepcionProductoBL();
                resultado =
                    recepcionProductoBL.ObtenerRecepcionProductoConciliacionPorAlmacenMovimiento(almacenesMovimiento);
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
            return resultado;
        }

    }
}
