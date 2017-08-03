using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class AlmacenMovimientoDetalleBL
    {
        /// <summary>
        /// Crea un registro en almacen movimiento detalle
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <returns></returns>
        internal int Crear(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleDal = new AlmacenMovimientoDetalleDAL();
                int result = almacenMovimientoDetalleDal.Crear(almacenMovimientoDetalleInfo);
                return result;
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
        /// Obtiene un registro de almacen movimiento detalle
        /// </summary>
        /// <returns></returns>
        internal AlmacenMovimientoDetalle ObtenerPorContratoId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleDAL = new AlmacenMovimientoDetalleDAL();
                AlmacenMovimientoDetalle result = almacenMovimientoDetalleDAL.ObtenerPorContratoId(almacenMovimientoDetalleInfo);
                return result;
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
        /// Obtiene un listado de almacenmovimientodetalle
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <param name="listaTipoMovimiento"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetallePorLoteId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo, List<TipoMovimientoInfo> listaTipoMovimiento)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleDal = new AlmacenMovimientoDetalleDAL();
                List<AlmacenMovimientoDetalle> result = almacenMovimientoDetalleDal.ObtenerAlmacenMovimientoDetallePorLoteId(almacenMovimientoDetalleInfo, listaTipoMovimiento);
                return result;
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
        /// Obtener por almacenmovimientodetalleid
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <returns></returns>
        internal AlmacenMovimientoDetalle ObtenerPorAlmacenMovimientoDetalleId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleDAL = new AlmacenMovimientoDetalleDAL();
                AlmacenMovimientoDetalle result = almacenMovimientoDetalleDAL.ObtenerPorAlmacenMovimientoDetalleId(almacenMovimientoDetalleInfo);
                return result;
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
        /// Actualiza registro almacenmovimientodetalle
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <returns></returns>
        internal void ActualizarAlmacenMovimientoDetalle(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleDal = new AlmacenMovimientoDetalleDAL();
                almacenMovimientoDetalleDal.ActualizarAlmacenMovimientoDetalle(almacenMovimientoDetalleInfo);
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
        /// Guardar la lista de almacenmovimientodetalle
        /// </summary>
        /// <param name="listaAlmacenMovimientoDetalle"></param>
        /// <param name="almacenMovimientoID"></param>
        /// <returns></returns>
        internal void GuardarAlmacenMovimientoDetalle(List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle, long almacenMovimientoID)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleDal = new AlmacenMovimientoDetalleDAL();
                almacenMovimientoDetalleDal.GuardarAlmacenMovimientoDetalle(listaAlmacenMovimientoDetalle, almacenMovimientoID);
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
        /// Obtiene un listado de almacenmovimientodetalle
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetallePorContratoId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleDal = new AlmacenMovimientoDetalleDAL();
                List<AlmacenMovimientoDetalle> result = almacenMovimientoDetalleDal.ObtenerAlmacenMovimientoDetallePorContratoId(almacenMovimientoDetalleInfo);
                return result;
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
        /// Obtiene el Almacen Movimiento Detalle por Almacen Movimiento
        /// </summary>
        /// <param name="almacenMovimientoID"></param>
        /// <returns></returns>
        internal AlmacenMovimientoDetalle ObtenerPorAlmacenMovimientoID(long almacenMovimientoID)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleDal = new AlmacenMovimientoDetalleDAL();
                AlmacenMovimientoDetalle result =
                    almacenMovimientoDetalleDal.ObtenerPorAlmacenMovimientoID(almacenMovimientoID);
                return result;
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
        /// Obtiene un listado de almacenmovimientodetalle
        /// </summary>
        /// <returns></returns>
        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetalleEntregadosPlanta(DateTime fechaInicial, DateTime fechaFinal, int organizacionID)
        {
            try
            {
                Logger.Info();
                var almacenMovimientoDetalleDal = new AlmacenMovimientoDetalleDAL();
                List<AlmacenMovimientoDetalle> result =
                    almacenMovimientoDetalleDal.ObtenerAlmacenMovimientoDetalleEntregadosPlanta(fechaInicial, fechaFinal,
                                                                                                organizacionID);
                return result;
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
