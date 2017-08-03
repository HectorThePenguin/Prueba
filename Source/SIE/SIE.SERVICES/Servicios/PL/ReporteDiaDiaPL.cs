using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using System;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteDiaDiaPL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Dia a Dia
        /// </summary>
        /// <returns> </returns>
        public List<ReporteDiaDiaInfo> GenerarReporteDiaDia(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteDiaDiaInfo> lista;
            try
            {
                Logger.Info();
                var reporteDiaDiaBL = new ReporteDiaDiaBL();
                lista = reporteDiaDiaBL.GenerarReporteDiaDia(organizacionID, fechaInicial, fechaFinal);
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
