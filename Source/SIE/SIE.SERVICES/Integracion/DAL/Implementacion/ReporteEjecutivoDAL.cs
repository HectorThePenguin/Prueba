using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteEjecutivoDAL : DALBase
    {
        /// <summary>
        ///     Obtiene un lista de los tipos de ganado
        /// </summary>
        /// <returns> </returns>
        internal List<TipoGanadoReporteEjecutivoInfo> ObtenerTipoGanadoReporteEjecutivo(int organizacionId, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteEjecutivoDAL.ObtenerParametrosTipoGanadoReporteEjecutivo(organizacionId, fechaInicial, fechaFinal);
                DataSet ds = Retrieve("TipoGanadoReporteEjecutivo_ObtenerPorFechas", parameters);
                List<TipoGanadoReporteEjecutivoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteEjecutivoDAL.ObtenerTipoGanadoReporteEjecutivo(ds);
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
        ///     Obtiene un lista de los Costos
        /// </summary>
        /// <returns> </returns>
        internal List<CostosReporteEjecutivoInfo> ObtenerCostosReporteEjecutivo(int organizacionId, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxReporteEjecutivoDAL.ObtenerParametrosCostosReporteEjecutivo(organizacionId, fechaInicial, fechaFinal);
                DataSet ds = Retrieve("CostosReporteEjecutivo_ObtenerPorFechas", parameters);
                List<CostosReporteEjecutivoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteEjecutivoDAL.ObtenerCostosReporteEjecutivo(ds);
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
