using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReportePaseaProcesoPL
    {
        /// <summary>
        /// Generar reporte de Pase a Proceso
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<ReportePaseaProcesoInfo> ObtenerReportePaseaProceso(int organizacionId)
        {
            IList<ReportePaseaProcesoInfo> lista;
            try
            {
                Logger.Info();
                var reportePaseaProcesoBL = new ReportePaseaProcesoBL();
                lista = reportePaseaProcesoBL.ObtenerReportePaseaProceso(organizacionId);
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

