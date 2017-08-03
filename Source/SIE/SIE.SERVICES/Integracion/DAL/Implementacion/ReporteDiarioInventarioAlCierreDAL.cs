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
    internal class ReporteDiarioInventarioAlCierreDAL:DALBase
    {
        internal ReporteDiarioInventarioAlCierreDatos GenerarReporteDiarioInventarioAlCierre(int organizacionID, DateTime fecha)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteDiarioInventarioAlCierre.ObtenerParametrosReporte(
                    organizacionID, fecha);
                DataSet ds = Retrieve("ReporteDiarioInventarioAlCierre", parameters);
                ReporteDiarioInventarioAlCierreDatos result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteDiarioInventarioAlCierre.ObtenerDatosReporte(ds);
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
