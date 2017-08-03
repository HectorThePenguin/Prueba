using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Modelos;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ConciliacionDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos necesarios para la conciliacion
        /// de pases a proceso
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<PolizaPaseProcesoModel> ObtenerConciliacionPaseProceso(int organizacionId, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxConciliacionDAL.ObtenerParametrosConciliacionPaseProceso(organizacionId, fechaInicial, fechaFinal);
                DataSet ds = Retrieve("Conciliacion_ObtenerPasesProceso", parametros);
                List<PolizaPaseProcesoModel> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConciliacionDAL.ObtenerConciliacionPaseProceso(ds);
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


        /// <summary>
        /// Obtiene un una lista  de Conciliacion por tipo poliza
        /// </summary>
        /// <param name="TipoPoliza"></param>
        /// <returns></returns>
        internal IList<PolizasIncorrectasInfo> ConciliacionTipoPoliza( int TipoPoliza)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConciliacionDAL.ConciliacionTipoPoliza(TipoPoliza);
                DataSet ds = Retrieve("PolizasIncorrectas_S", parameters);
                IList<PolizasIncorrectasInfo> result = null;
                if(ValidateDataSet(ds))
                {
                    result = MapConciliacionDAL.ConciliacionTipoPoliza(ds);
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


        /// <summary>
        /// Obtiene una lista de conciliacion detalle por tipo de poliza
        /// </summary>
        /// <param name="TipoPoliza"></param>
        /// <returns></returns>
        internal IList<ConciliacionInfo> ConciliacionDetalle(int TipoPoliza)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxConciliacionDAL.ConciliacionTipoPoliza(TipoPoliza);
                DataSet ds = Retrieve("ConciliacionDetalle_S", parameters);
                IList<ConciliacionInfo> result = null;
                if(ValidateDataSet(ds))
                {
                    result = MapConciliacionDAL.ConciliacionDetalle(ds);
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


        /// <summary>
        /// Obtiene los movimientos de almacen
        /// para su conciliacion
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal ConciliacionMovimientosAlmacenModel ObtenerMovimientosAlmacenConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxConciliacionDAL.ObtenerParametrosMovimientosAlmacenConciliacion(organizacionID, fechaInicial, fechaFinal);
                DataSet ds = Retrieve("AlmacenMovimiento_ObtenerConciliacionMovimientosSIAP", parametros);
                ConciliacionMovimientosAlmacenModel result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConciliacionDAL.ObtenerMovimientosAlmacenConciliacion(ds);
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

        /// <summary>
        /// Obtiene los movimientos a conciliar de materia prima
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<ContenedorEntradaMateriaPrimaInfo> ObtenerContenedorEntradaMateriaPrimaConciliacion(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parametros =
                    AuxConciliacionDAL.ObtenerParametrosEntradaMateriaPrimaConciliacion(organizacionID, fechaInicial, fechaFinal);
                DataSet ds = Retrieve("EntradaProducto_ObtenerConciliacion", parametros);
                List<ContenedorEntradaMateriaPrimaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapConciliacionDAL.ObtenerEntradaMateriaPrimaConciliacion(ds);
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
    }
}
