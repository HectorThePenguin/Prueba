using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;


namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AlmacenMovimientoDetalleDAL : DALBase
    {
        /// <summary>
        /// Crea un registro en almacen movimiento detalle
        /// </summary>
        /// <returns></returns>
        internal int Crear(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDetalleDAL.ObtenerParametrosCrear(almacenMovimientoDetalleInfo);
                int result = Create("AlmacenMovimientoDetalle_Crear", parameters);
                return result;
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un registro por contrato id
        /// </summary>
        /// <returns></returns>
        internal AlmacenMovimientoDetalle ObtenerPorContratoId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            AlmacenMovimientoDetalle listaAlmacenMovimientoDetalleInfo = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenMovimientoDetalleDAL.ObtenerParametrosObtenerPorContratoId(almacenMovimientoDetalleInfo);
                DataSet ds = Retrieve("AlmacenMovimientoDetalle_ObtenerPorContratoId", parametros);
                if (ValidateDataSet(ds))
                {
                    listaAlmacenMovimientoDetalleInfo = MapAlmacenMovimientoDetalleDAL.ObtenerPorContratoId(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return listaAlmacenMovimientoDetalleInfo;
        }

        /// <summary>
        /// Obtiene un listado de almacenmovimientodetalle por loteid
        /// </summary>
        /// <returns></returns>
        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetallePorLoteId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo, List<TipoMovimientoInfo> listaTipoMovimiento)
        {
            List<AlmacenMovimientoDetalle> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxAlmacenMovimientoDetalleDAL.ObtenerAlmacenMovimientoDetallePorLoteId(almacenMovimientoDetalleInfo, listaTipoMovimiento);

                DataSet ds = Retrieve("AlmacenMovimientoDetalle_ObtenerPorLoteID", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapAlmacenMovimientoDetalleDAL.ObtenerAlmacenMovimientoDetallePorLoteId(ds);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtener registro por almacenmovimientodetalleid
        /// </summary>
        /// <param name="almacenMovimientoDetalleInfo"></param>
        /// <returns></returns>
        internal AlmacenMovimientoDetalle ObtenerPorAlmacenMovimientoDetalleId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            AlmacenMovimientoDetalle resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxAlmacenMovimientoDetalleDAL.ObtenerParametrosObtenerPorAlmacenMovimientoDetalleId(almacenMovimientoDetalleInfo);

                DataSet ds = Retrieve("AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoDetalleID", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapAlmacenMovimientoDetalleDAL.ObtenerPorContratoId(ds);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Se actualiza registro
        /// </summary>
        internal void ActualizarAlmacenMovimientoDetalle(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDetalleDAL.ActualizarAlmacenMovimientoDetalle(almacenMovimientoDetalleInfo);
                Update("AlmacenMovimientoDetalle_Actualizar", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Crea un registro en almacen movimiento detalle
        /// </summary>
        /// <returns></returns>
        internal void GuardarDetalleCierreDiaInventarioPA(List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDetalleDAL.ObtenerGuardarDetalleCierreDiaInventarioPA(listaAlmacenMovimientoDetalle);
                Create("AlmacenMovimientoDetalle_GuardarDetalleXml", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Crea un registro en almacen movimiento detalle
        /// </summary>
        /// <returns></returns>
        internal void GuardarAlmacenMovimientoDetalle(List<AlmacenMovimientoDetalle> listaAlmacenMovimientoDetalle, long almacenMovimientoID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDetalleDAL.ObtenerGuardarAlmacenMovimientoDetalle(listaAlmacenMovimientoDetalle, almacenMovimientoID);
                Create("AlmacenMovimientoDetalle_GuardarDetalleXml", parameters);
            }
            catch (SqlException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (DataException ex)
            {
                Logger.Error(ex);
                throw new ExcepcionServicio(MethodBase.GetCurrentMethod(), ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }

        /// <summary>
        /// Obtiene un listado de almacenmovimientodetalle por loteid
        /// </summary>
        /// <returns></returns>
        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetallePorContratoId(AlmacenMovimientoDetalle almacenMovimientoDetalleInfo)
        {
            List<AlmacenMovimientoDetalle> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxAlmacenMovimientoDetalleDAL.ObtenerParametrosObtenerPorContratoId(almacenMovimientoDetalleInfo);

                DataSet ds = Retrieve("AlmacenMovimientoDetalle_ObtenerPorContratoIDListado", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapAlmacenMovimientoDetalleDAL.ObtenerAlmacenMovimientoDetallePorLoteId(ds);
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene el Almacen Movimiento Detalle por Almacen Movimiento
        /// </summary>
        /// <param name="almacenMovimientoID"></param>
        /// <returns></returns>
        internal AlmacenMovimientoDetalle ObtenerPorAlmacenMovimientoID(long almacenMovimientoID)
        {
            AlmacenMovimientoDetalle resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxAlmacenMovimientoDetalleDAL.ObtenerParametrosPorAlmacenMovimientoID(almacenMovimientoID);
                DataSet ds = Retrieve("AlmacenMovimientoDetalle_ObtenerPorAlmacenMovimientoID", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapAlmacenMovimientoDetalleDAL.ObtenerAlmacenMovimientoDetallePorAlmacenMovimientoID(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return resultado;
        }

        /// <summary>
        /// Obtiene un listado de almacenmovimientodetalle
        /// </summary>
        /// <returns></returns>
        internal List<AlmacenMovimientoDetalle> ObtenerAlmacenMovimientoDetalleEntregadosPlanta(DateTime fechaInicial, DateTime fechaFinal, int organizacionID)
        {
            List<AlmacenMovimientoDetalle> resultado = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxAlmacenMovimientoDetalleDAL.ObtenerParametrosMovimientosEntregadosPlanta(fechaInicial, fechaFinal, organizacionID);
                DataSet ds = Retrieve("AlmacenMovimientoDetalle_ObtenerGranoEntregado", parametros);
                if (ValidateDataSet(ds))
                {
                    resultado = MapAlmacenMovimientoDetalleDAL.ObtenerAlmacenMovimientoDetalleEntregadosPlanta(ds);
                }
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
