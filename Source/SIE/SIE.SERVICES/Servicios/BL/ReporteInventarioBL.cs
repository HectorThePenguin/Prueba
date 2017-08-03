using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ReporteInventarioBL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Inventario
        /// </summary>
        /// <returns> </returns>
        internal List<ReporteInventarioInfo> GenerarReporteInventario(int organizacionId, int tipoProcesoID, DateTime fechaInicial, DateTime fechaFinal)
        {
            List<ReporteInventarioInfo> lista;
            try
            {
                Logger.Info();
                var reporteInventarioDAL = new ReporteInventarioDAL();
                lista = reporteInventarioDAL.GenerarReporteInventario(organizacionId, tipoProcesoID, fechaInicial,
                                                                      fechaFinal);
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
