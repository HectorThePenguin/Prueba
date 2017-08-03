using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteInventarioPL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Inventario
        /// </summary>
        /// <returns> </returns>
        public List<ReporteInventarioInfo> GenerarReporteInventario(int organizacionId, int tipoProcesoID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteInventarioInfo> lista;
            try
            {
                Logger.Info();
                var reporteInventarioBL = new ReporteInventarioBL();
                lista = reporteInventarioBL.GenerarReporteInventario(organizacionId, tipoProcesoID, fechaInicial, fechaFinal);
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
