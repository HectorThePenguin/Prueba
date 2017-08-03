using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteSolicitudPaseProcesoPL
    {
        /// <summary>
        /// Obtiene los datos al informe de solicitud de pase a proceso
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public IList<ReporteSolicitudPaseProcesoInfo> ObtenerReporteSolicitudPaseProceso(int organizacionId, DateTime fecha)
        {
            IList<ReporteSolicitudPaseProcesoInfo> lista = null;
            try
            {
                Logger.Info();
                var reporteBl = new ReporteSolicitudPaseProcesoBL();
                lista = reporteBl.ObtenerReporteSolicitudPaseProceso(organizacionId, fecha);

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
