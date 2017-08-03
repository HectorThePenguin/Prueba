using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteLectorComederosBL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Lector Comederos
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="horario"></param>
        /// <param name="FechaHoy"></param>
        /// <returns></returns>
        internal List<ReporteLectorComederosInfo> ObtenerReporteLectorComederos(int organizacionID, int horario, DateTime? FechaHoy)
        {
            List<ReporteLectorComederosInfo> lista;
            try
            {
                Logger.Info();
                var reporteLectorComederosDal = new ReporteLectorComederosDAL();
                lista = reporteLectorComederosDal.ObtenerReporteLectorComederos(organizacionID, horario, FechaHoy);
            }
            catch (ExcepcionGenerica)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return lista;
        }
    }
}
