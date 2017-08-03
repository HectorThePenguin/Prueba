using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ReporteDiaDiaBL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Dia a Dia
        /// </summary>
        /// <returns> </returns>
        internal List<ReporteDiaDiaInfo> GenerarReporteDiaDia(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteDiaDiaInfo> lista;
            try
            {
                Logger.Info();
                var reporteDiaDiaDAL = new ReporteDiaDiaDAL();
                lista = reporteDiaDiaDAL.GenerarReporteDiaDia(organizacionID, fechaInicial, fechaFinal);
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
