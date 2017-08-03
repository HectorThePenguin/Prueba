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
    internal class ReporteRecuperacionMermaDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Recuperacion de Merma
        /// </summary>
        /// <returns> </returns>
        internal List<ReporteRecuperacionMermaInfo> GenerarReporteRecuperacionMerma(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteRecuperacionMermaDAL.ObtenerParametrosReporteRecuperacionMerma(organizacionID, fechaInicial, fechaFinal);
                DataSet ds = Retrieve("RecuperacionDeMerma_ObtenerPorFechas", parameters);
                List<ReporteRecuperacionMermaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteRecuperacionMermaDAL.ObtenerReporteRecuperacionMerma(ds);
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
