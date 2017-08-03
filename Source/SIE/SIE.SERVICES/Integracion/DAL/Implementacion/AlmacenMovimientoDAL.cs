using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class AlmacenMovimientoDAL : DALBase
    {
        /// <summary>
        /// Crear un registro en almacen movimiento
        /// </summary>
        /// <returns></returns>
        internal long Crear(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDAL.ObtenerParametrosCrear(almacenMovimientoInfo);
                long result = Create("AlmacenMovimiento_Crear", parameters);
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
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="almacenMovimientoId"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo ObtenerPorId(long almacenMovimientoId)
        {
            AlmacenMovimientoInfo almacenMovimientoInfo = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenMovimientoDAL.ObtenerParametrosObtenerPorId(almacenMovimientoId);
                DataSet ds = Retrieve("AlmacenMovimiento_ObtenerPorId", parametros);
                if (ValidateDataSet(ds))
                {
                    almacenMovimientoInfo = MapAlmacenMovimientoDAL.ObtenerPorId(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenMovimientoInfo;
        }

        /// <summary>
        /// Actualiza el status del registro
        /// </summary>
        /// <param name="almacenMovimientoInfo"></param>
        /// <returns></returns>
        internal void ActualizarEstatus(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDAL.ObtenerParametrosActualizarEstatus(almacenMovimientoInfo);
                Update("AlmacenMovimiento_ActualizarEstatus", parameters);
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
        /// Crear un registro en almacen movimiento
        /// </summary>
        /// <returns></returns>
        internal long GuardarMovimientoCierreDiaPA(AlmacenMovimientoInfo almacenMovimientoInfo)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDAL.ObtenerParametrosGuardarMovimientoCierreDiaPA(almacenMovimientoInfo);
                long result = Create("AlmacenMovimiento_CrearCierreDia", parameters);
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
        /// Crear un registro en almacen movimiento
        /// </summary>
        /// <returns></returns>
        internal bool ValidarEjecucionCierreDia(int almacenID)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDAL.ObtenerParametrosValidarEjecucionCierreDia(almacenID);
                DataSet ds = Retrieve("CierreDiaInventarioPA_ValidarCierreDia", parameters);
                if (ValidateDataSet(ds))
                {
                    return true;
                }
                return false;
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
        /// Obtiene los movimientos de inventario para generar
        /// poliza de consumo de producto
        /// </summary>
        /// <param name="almacenID"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal List<ContenedorAlmacenMovimientoCierreDia> ObtenerMovimientosInventario(int almacenID, int organizacionID)
        {
            List<ContenedorAlmacenMovimientoCierreDia> almacenesMovimiento = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAlmacenMovimientoDAL.ObtenerParametrosMovimientosInventario(almacenID, organizacionID);
                DataSet ds = Retrieve("AlmacenMovimiento_ObtenerMovimientosCierreDia", parameters);
                if (ValidateDataSet(ds))
                {
                    almacenesMovimiento = MapAlmacenMovimientoDAL.ObtenerMovimientosInventario(ds);
                }
                return almacenesMovimiento;
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

        internal void ActualizarGeneracionPoliza(List<ContenedorAlmacenMovimientoCierreDia> movimientos)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAlmacenMovimientoDAL.ObtenerParametrosActualizarGeneracionPoliza(movimientos);
                Update("AlmacenMovimiento_ActualizaGeneracionPoliza", parameters);
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
        /// Obtiene los movimientos pendientes pro autorizar del cierre de dia de inventario de planta de alimentos
        /// </summary>
        /// <returns></returns>
        internal List<MovimientosAutorizarCierreDiaPAModel> ObtenerMovimientosPendientesAutorizar(FiltrosAutorizarCierreDiaInventarioPA filtrosAutorizarCierreDia)
        {
            try
            {
                List<MovimientosAutorizarCierreDiaPAModel> result = null;
                Logger.Info();
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDAL.ObtenerParametrosObtenerMovimientosPendientesAutorizar(filtrosAutorizarCierreDia);
                DataSet ds = Retrieve("CierreDiaInventarioPA_ObtenerMovimientosAutorizar", parameters);
                if (ValidateDataSet(ds))
                {
                    result = MapAlmacenMovimientoDAL.ObtenerPorMovimientosPendientesAutorizar(ds);
                }
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

        internal void ActualizarEstatusAlmacenMovimiento(FiltroCambiarEstatusInfo filtros)
        {
            try
            {
                Dictionary<string, object> parameters = AuxAlmacenMovimientoDAL.ObtenerParametrosActualizarEstatusAlmacenMovimiento(filtros);
                Update("AlmacenMovimiento_ActualizarEstatusNuevo", parameters);
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
        /// Obtiene el Almacen Movimiento por su Detalle
        /// </summary>
        /// <param name="almancenMovimientosDetalle"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo ObtenerMovimientoPorClaveDetalle(List<AlmacenMovimientoDetalle> almancenMovimientosDetalle)
        {
            AlmacenMovimientoInfo almacenMovimiento = null;
            try
            {
                Dictionary<string, object> parameters =
                    AuxAlmacenMovimientoDAL.ObtenerParametrosPorClaveDetalle(almancenMovimientosDetalle);
                DataSet ds = Retrieve("AlmacenMovimiento_ObtenerMovimientoXML", parameters);
                if (ValidateDataSet(ds))
                {
                    almacenMovimiento = MapAlmacenMovimientoDAL.ObtenerMovimientoPorClaveDetalle(ds);
                }
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
            return almacenMovimiento;
        }

        /// <summary>
        /// Obtiene un registro por id
        /// </summary>
        /// <param name="almacenMovimientoId"></param>
        /// <returns></returns>
        internal AlmacenMovimientoInfo ObtenerPorIDCompleto(long almacenMovimientoId)
        {
            AlmacenMovimientoInfo almacenMovimientoInfo = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenMovimientoDAL.ObtenerParametrosObtenerPorIDCompleto(almacenMovimientoId);
                DataSet ds = Retrieve("AlmacenMovimiento_ObtenerMovimientosPorID", parametros);
                if (ValidateDataSet(ds))
                {
                    almacenMovimientoInfo = MapAlmacenMovimientoDAL.ObtenerPorIDCompleto(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return almacenMovimientoInfo;
        }

        /// <summary>
        /// Obtiene los movimientos de inventario para generar
        /// poliza de consumo de producto
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        internal List<ContenedorAlmacenMovimientoCierreDia> ObtenerMovimientosInventarioFiltros(FiltroAlmacenMovimientoInfo filtros)
        {
            List<ContenedorAlmacenMovimientoCierreDia> almacenesMovimiento = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAlmacenMovimientoDAL.ObtenerParametrosMovimientosInventarioFiltros(filtros);
                DataSet ds = Retrieve("AlmacenMovimiento_ObtenerMovimientosFiltros", parameters);
                if (ValidateDataSet(ds))
                {
                    almacenesMovimiento = MapAlmacenMovimientoDAL.ObtenerMovimientosInventarioFiltros(ds);
                }
                return almacenesMovimiento;
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
        /// Obtiene los movimientos de inventario para generar
        /// poliza de consumo de producto
        /// </summary>
        /// <param name="almacenID"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"> </param>
        /// <returns></returns>
        internal List<ContenedorAlmacenMovimientoCierreDia> ObtenerMovimientosInventario(int almacenID, int organizacionID, DateTime fecha)
        {
            List<ContenedorAlmacenMovimientoCierreDia> almacenesMovimiento = null;
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxAlmacenMovimientoDAL.ObtenerParametrosMovimientosInventario(almacenID, organizacionID, fecha);
                DataSet ds = Retrieve("AlmacenMovimiento_ObtenerMovimientosCierreDia", parameters);
                if (ValidateDataSet(ds))
                {
                    almacenesMovimiento = MapAlmacenMovimientoDAL.ObtenerMovimientosInventario(ds);
                }
                return almacenesMovimiento;
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
        /// Obtiene los movimientos de los subproductos de premezcla
        /// </summary>
        /// <param name="productosPremezcla"></param>
        /// <returns></returns>
        internal IEnumerable<AlmacenMovimientoSubProductosModel> ObtenerMovimientosSubProductos(IEnumerable<AlmacenMovimientoSubProductosModel> productosPremezcla)
        {
            try
            {
                Logger.Info();
                IMapBuilderContext<AlmacenMovimientoSubProductosModel> mapeo =
                    MapAlmacenMovimientoDAL.ObtenerMapeoAlmacenMovimientoSubProductos();
                string parameters = AuxAlmacenMovimientoDAL.ObtenerParametrosMovimientosSubProductos(productosPremezcla);
                IEnumerable<AlmacenMovimientoSubProductosModel> movimientosSubProductos = GetDatabase().ExecuteSprocAccessor
                    <AlmacenMovimientoSubProductosModel>(
                        "AlmacenMoviento_ObtenerSubProductos", mapeo.Build(),
                        new object[] { parameters });
                return movimientosSubProductos;
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
        /// Obtiene todos los movimientos de un Contrato
        /// </summary>
        /// <param name="contrato"></param>
        /// <returns></returns>
        internal List<AlmacenMovimientoInfo> ObtenerMovimientosPorContrato(ContratoInfo contrato)
        {
            List<AlmacenMovimientoInfo> movimientos = null;
            try
            {
                Dictionary<string, object> parametros = AuxAlmacenMovimientoDAL.ObtenerParametrosObtenerMovimientosPorContrato(contrato);
                DataSet ds = Retrieve("AlmacenMovimiento_ObtenerMovimientosPorContrato", parametros);
                if (ValidateDataSet(ds))
                {
                    movimientos = MapAlmacenMovimientoDAL.ObtenerMovimientosPorContrato(ds);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return movimientos;
        }
    }
}
