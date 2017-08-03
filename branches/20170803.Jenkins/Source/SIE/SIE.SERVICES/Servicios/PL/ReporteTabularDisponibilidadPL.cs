using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class  ReporteTabularDisponibilidadPL
    {
        /// <summary>
        /// Generar reporte de tabular disponibilidad
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public List<ReporteTabularDisponibilidadSemanaInfo> GenerarReporteTabularDisponibilidad(int organizacionId,DateTime fecha)
        {
            List<ReporteTabularDisponibilidadSemanaInfo> lista;
            try
            {
                Logger.Info();
                var reporteTabularDisponibilidadBL = new ReporteTabularDisponibilidadBL();
                lista = reporteTabularDisponibilidadBL.GenerarReporteTabularDisponibilidad(organizacionId,fecha);
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
