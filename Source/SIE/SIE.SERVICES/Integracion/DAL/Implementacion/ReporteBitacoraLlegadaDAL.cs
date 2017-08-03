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
    public class ReporteBitacoraLlegadaDAL : DALBase
    {
        /// <summary>
        /// Obtiene los datos del reporte bitacora de llegada
        /// El TipoFecha esta a la espera de 1.-Fecha Llegada, 2.-Fecha Entrada y 3.-Fecha Pesaje
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoFecha"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal List<ReporteBitacoraLlegadaInfo> ObtenerParametrosDatosReporteBitacoraLlegada(int organizacionID, int tipoFecha, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                Logger.Info();
                Dictionary<string, object> parameters = AuxReporteBitacoraLlegadaDAL.ObtenerParametrosDatosBitacoraLlegada(organizacionID, tipoFecha, fechaInicial, fechaFinal);
                DataSet ds = Retrieve("ReporteBitacora_FechaLLegadaEntradaGanaderaPesaje", parameters);
                List<ReporteBitacoraLlegadaInfo> result = null;
                if (ValidateDataSet(ds))
                {
                    result = MapReporteBitacoraLlegadaDAL.ObtenerDatosBitacoraLlegada(ds);
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
