using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    /// <summary>
    /// Clase de presentacion del reporte de inventario de materia prima
    /// </summary>
    public class ReporteInventarioMateriaPrimaPL
    {
        /// <summary>
        /// Obtiene la informacion del reporte de inventario de materia prima
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="productoId"></param>
        /// <param name="almacenId"></param>
        /// <param name="lote"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <returns></returns>
        public List<ReporteInventarioMateriaPrimaInfo> GenerarReporteInventario(int organizacionId, int productoId, int almacenId, int lote, DateTime fechaInicio, DateTime fechaFin)
        {
            List<ReporteInventarioMateriaPrimaInfo> lista;
            try
            {
                Logger.Info();
                var reporteBl = new ReporteInventarioMateriaPrimaBL();
                lista = reporteBl.GenerarReporteInventario(organizacionId, productoId, almacenId, lote, fechaInicio, fechaFin);
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
