using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;


namespace SIE.Services.Servicios.BL
{
    internal class ReportePaseaProcesoBL
    {

        /// <summary>
        /// Regresa el reporte de Pase a Proceso
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        internal IList<ReportePaseaProcesoInfo> ObtenerReportePaseaProceso(int organizacionId)
        {
            IList<ReportePaseaProcesoInfo> listaPases;
            try
            {
                Logger.Info();
                var reporteDal = new ReportePaseaProcesoDAL();
                listaPases = reporteDal.ObtenerReportePaseaProceso(organizacionId);

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

            return listaPases;
        }
    }
}
