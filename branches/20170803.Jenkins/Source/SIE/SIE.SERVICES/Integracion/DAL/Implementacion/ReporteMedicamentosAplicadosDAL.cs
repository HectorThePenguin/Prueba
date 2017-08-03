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
    internal class ReporteMedicamentosAplicadosDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos del reporte desde la base de datos
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<ReporteMedicamentosAplicadosDatos> ObtenerParametrosDatosMedicamentosAplicados(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteMedicamentosAplicadosDAL.ObtenerParametrosDatosMedicamentosAplicados(organizacionID, fechaInicial, fechaFinal);
                DataSet ds = Retrieve("ReporteMedicamentosAplicados_Obtener", parameters);
                List<ReporteMedicamentosAplicadosDatos> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteMedicamentosAplicadosDAL.ObtenerDatosMedicamentosCabezasAplicados(ds);
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
        /// Obtiene de la base de datos los movimientos de tratamientos de sanidad
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public List<ReporteMedicamentosAplicadosDatos> ObtenerParametrosDatosMedicamentosAplicadosSanidad(int organizacionId, int almacenId, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteMedicamentosAplicadosDAL.ObtenerParametrosDatosMedicamentosAplicados(organizacionId, fechaInicial, fechaFinal, almacenId);
                DataSet ds = Retrieve("ReporteMedicamentosAplicadosSanidad_Obtener", parameters);
                List<ReporteMedicamentosAplicadosDatos> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteMedicamentosAplicadosDAL.ObtenerDatosMedicamentosAplicados(ds);
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
