using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteOperacionSanidadPL
    {
        /// <summary>
        /// Obtiene la informacion del reporte de operacion de sanidad
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<ReporteOperacionSanidadInfo> GenerarReporteOperacionSanidad(int organizacionId, DateTime fechaIni,
            DateTime fechaFin)
        {
            List<ReporteOperacionSanidadInfo> retValue = null;
            try
            {
                var reporteBl = new ReporteOperacionSanidadBL();
                retValue = reporteBl.GenerarReporteOperacionSanidad(organizacionId, fechaIni, fechaFin);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return retValue;
        }
    }
}
