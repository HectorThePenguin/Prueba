using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteLlegadaLogisticaPL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Recuperacion de Merma
        /// </summary>
        /// <returns> </returns>
        public List<ReporteLlegadaLogisticaDatos> GenerarReporteLlegadaLogistica(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteLlegadaLogisticaDatos> lista;
            try
            {
                Logger.Info();
                var reporteLlegadaLogisticaBL = new ReporteLlegadaLogisticaBL();
                lista = reporteLlegadaLogisticaBL.GenerarReporteLlegadaLogistica(organizacionID, fechaInicial, fechaFinal);
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
