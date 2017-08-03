using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteRecuperacionMermaPL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Recuperacion de Merma
        /// </summary>
        /// <returns> </returns>
        public List<ReporteRecuperacionMermaInfo> GenerarReporteRecuperacionMerma(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteRecuperacionMermaInfo> lista;
            try
            {
                Logger.Info();
                var reporteRecuperacionMermaBL = new ReporteRecuperacionMermaBL();
                lista = reporteRecuperacionMermaBL.GenerarReporteRecuperacionMerma(organizacionID, fechaInicial, fechaFinal);
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
