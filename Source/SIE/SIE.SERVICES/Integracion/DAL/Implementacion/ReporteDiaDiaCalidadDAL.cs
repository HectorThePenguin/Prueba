


using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BLToolkit.Data.Sql;
using SIE.Base.Exepciones;
using SIE.Base.Integracion.DAL;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Enums;
using SIE.Services.Integracion.DAL.Excepciones;
using SIE.Services.Integracion.DAL.Integracion.Auxiliar;
using SIE.Services.Integracion.DAL.Integracion.Mapeos;
using SqlException = System.Data.SqlClient.SqlException;

namespace SIE.Services.Integracion.DAL.Implementacion
{
    internal class ReporteDiaDiaCalidadDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos del reporte desde la base de datos
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IList<ReporteDiaDiaCalidadInfo> ObtenerParametrosReporteDiaDiaCalidad(int organizacionID, DateTime fecha)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteDiaDiaCalidadDAL.ObtenerParametrosReporteDiaDiaCalidad(organizacionID, fecha);
                DataSet ds = Retrieve("ReporteDiaDiaCalidad", parameters);
                IList<ReporteDiaDiaCalidadInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteDiaDiaCalidadDAL.ObtenerDatosDiaDiaCalidad(ds);
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
        /// Obtiene los parametros del reporte dia a dia de calidad
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal IList<ReporteDiaDiaCalidadAnalisisInfo> ReporteDiaDiaCalidadAnalisis(int organizacionId, DateTime fecha)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteDiaDiaCalidadDAL.ObtenerParametrosReporteDiaDiaCalidad(organizacionId, fecha);
                DataSet ds = Retrieve("ReporteDiaDiaCalidad_OtrosAnalisis", parameters);
                IList<ReporteDiaDiaCalidadAnalisisInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteDiaDiaCalidadDAL.ObtenerDatosDiaDiaCalidadAnalisis(ds);
                }
                else
                {
                    result = new List<ReporteDiaDiaCalidadAnalisisInfo>();
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
