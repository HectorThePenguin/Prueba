using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteBitacoraLlegadaPL
    {
        /// <summary>
        /// Obtiene la informacion del reporte bitacora llegada
        /// El TipoFecha esta a la espera de 1.-Fecha Llegada, 2.-Fecha Entrada y 3.-Fecha Pesaje
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionId"></param>
        /// <param name="tipoFecha"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<ReporteBitacoraLlegadaInfo> ObtenerReporteBitacoraLlegada(ReporteEncabezadoInfo encabezado, int organizacionId, int tipoFecha, DateTime fechaIni, DateTime fechaFin)
        {
            List<ReporteBitacoraLlegadaInfo> lista;
            try
            {
                Logger.Info();
                var reporteReporteBitacoraLlegadaBL = new ReporteBitacoraLlegadaBL();
                lista = reporteReporteBitacoraLlegadaBL.ObtenerReporteBitacoraLlegada(encabezado,
                    organizacionId, tipoFecha, fechaIni, fechaFin);

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
