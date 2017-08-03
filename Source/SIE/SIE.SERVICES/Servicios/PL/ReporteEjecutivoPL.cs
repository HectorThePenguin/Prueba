using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteEjecutivoPL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Ejecutivo
        /// </summary>
        /// <returns> </returns>
        public List<ReporteEjecutivoResultadoInfo> GenerarReporteEjecutivo(int organizacionId, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteEjecutivoResultadoInfo> lista;
            try
            {
                Logger.Info();
                var reporteEjecutivoBL = new ReporteEjecutivoBL();
                lista = reporteEjecutivoBL.GenerarReporteEjecutivo(organizacionId, fechaInicial, fechaFinal);
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
