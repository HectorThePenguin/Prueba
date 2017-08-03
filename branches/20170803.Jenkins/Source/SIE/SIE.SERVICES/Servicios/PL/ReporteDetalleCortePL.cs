using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteDetalleCortePL
    {
        /// <summary>
        /// Obtiene los datos del reporte
        /// </summary>
        /// <param name="encabezado"></param>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<ReporteDetalleCorteModel> ObtenerReporteDetalleCorte(ReporteEncabezadoInfo encabezado, int organizacionID, DateTime fechaInicial, DateTime fechaFinal, int idUsuario,int TipoMovimientoID)
        {
            List<ReporteDetalleCorteModel> lista;
            try
            {
                Logger.Info();
                var reporteBl = new ReporteDetalleCorteBL();
                lista = reporteBl.ObtenerReporteDetalleCorte(encabezado, organizacionID, fechaInicial, fechaFinal, idUsuario, TipoMovimientoID);

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
