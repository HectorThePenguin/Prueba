using System;
using System.Collections.Generic;
using SIE.Base.Integracion.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReportePaseaProcesoDAL: DALBase
    {

        /// <summary>
        /// Obtiene un lista de información para generar el reporte de pase a proceso
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns> </returns>
        internal IList<ReportePaseaProcesoInfo> ObtenerReportePaseaProceso(int organizacionId)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters =
                    AuxReportePaseaProcesoDAL.ObtenerParametrosReportePaseaProceso(organizacionId);
                DataSet ds = Retrieve("ReportePaseaProceso", parameters);
                IList<ReportePaseaProcesoInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReportePaseaProcesoDAL.Generar(ds);
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
