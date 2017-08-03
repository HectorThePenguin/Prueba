using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class AlmacenMovimientoDetallePL
    {
        /// <summary>
        /// Obtiene un registro por almacenmovimientoid
        /// </summary>
        /// <returns></returns>
        public AlmacenMovimientoDetalle ObtenerPorContratoId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            AlmacenMovimientoDetalle info;
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                info = almacenMovimientoDetalleBl.ObtenerPorContratoId(almacenMovimientoDetalleInfo);
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
            return info;
        }

        /// <summary>
        /// Obtiene un listado de almacenmovimientodetalle
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <param name="listaTipoMovimiento"> </param>
        /// <returns></returns>
        public List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetallePorLoteId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo, List<TipoMovimientoInfo> listaTipoMovimiento)
        {
            List<AlmacenMovimientoDetalle> info;
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                info = almacenMovimientoDetalleBl.ObtenerAlmacenMovimientoDetallePorLoteId(almacenMovimientoDetalleInfo, listaTipoMovimiento);
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
            return info;
        }

        /// <summary>
        /// Obtiene un listado de almacenmovimientodetalle
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <returns></returns>
        public List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetallePorContratoId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            List<AlmacenMovimientoDetalle> info;
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                info = almacenMovimientoDetalleBl.ObtenerAlmacenMovimientoDetallePorContratoId(almacenMovimientoDetalleInfo);
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
            return info;
        }

        /// <summary>
        /// Obtiene un listado de almacenmovimientodetalle
        /// </summary>
        /// <returns></returns>
        public List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetalleEntregadosPlanta(DateTime fechaInicial, DateTime fechaFinal, int organizacionID)
        {
            List<AlmacenMovimientoDetalle> info;
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleBl = new AlmacenMovimientoDetalleBL();
                info = almacenMovimientoDetalleBl.ObtenerAlmacenMovimientoDetalleEntregadosPlanta(fechaInicial,
                                                                                                  fechaFinal,
                                                                                                  organizacionID);
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
            return info;
        }
    }
}
