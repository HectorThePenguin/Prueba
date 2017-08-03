using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Servicios.BL
{
    public class ReporteBitacoraLlegadaBL
    {
        /// <summary>
        /// Obtiene los datos del Reporte de Bitacora Llegada
        /// El TipoFecha esta a la espera de 1.-Fecha Llegada, 2.-Fecha Entrada y 3.-Fecha Pesaje
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionId"></param>
        /// <param name="tipoFecha"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        internal List<ReporteBitacoraLlegadaInfo> ObtenerReporteBitacoraLlegada(ReporteEncabezadoInfo encabezado, int organizacionId, int tipoFecha, DateTime fechaIni, DateTime fechaFin)
        {
            List<ReporteBitacoraLlegadaInfo> lista;
            try
            {
                Logger.Info();
                var reporteBitacoraLlegadaDAL = new ReporteBitacoraLlegadaDAL();
                lista = reporteBitacoraLlegadaDAL.ObtenerParametrosDatosReporteBitacoraLlegada(organizacionId,
                                                                                                tipoFecha,
                                                                                                fechaIni,
                                                                                                fechaFin);
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
