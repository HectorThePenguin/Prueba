using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using System;
using SIE.Services.Servicios.BL;
namespace SIE.Services.Servicios.PL
{
    public class ReporteLectorComederosPL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Lector Comederos
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="horario"></param>
        /// <param name="FechaHoy"></param>
        /// <returns></returns>
        public List<ReporteLectorComederosInfo> ObtenerReporteLectorComederos(int organizacionID, int horario, DateTime? FechaHoy)
        {
            List<ReporteLectorComederosInfo> lista;
            try
            {
                Logger.Info();
                var reporteLectorComederos = new ReporteLectorComederosBL();
                lista = reporteLectorComederos.ObtenerReporteLectorComederos(organizacionID, horario, FechaHoy);
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
