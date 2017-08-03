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
    class ReporteLectorComederosDAL : DALBase
    {
        /// <summary>
        /// Obtiene la informacion del reporte Lector Comederos
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="horario"></param>
        /// <param name="FechaHoy"></param>
        /// <returns></returns>
        internal List<ReporteLectorComederosInfo> ObtenerReporteLectorComederos(int organizacionID, int horario, DateTime? FechaHoy)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteLectorComederosDAL.ObtenerParametrosReporteLectorComederos(organizacionID, horario, FechaHoy);
                DataSet ds = Retrieve("ReporteLectorComederos", parameters);
                List<ReporteLectorComederosInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteLectorComederosDAL.ObtenerParametrosDatosReporteLectorComederos(ds);
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
