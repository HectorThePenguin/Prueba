using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteDetalleReimplantePL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte Inventario
        /// </summary>
        /// <returns> </returns>
        public DataTable GenerarReporteDetalleReimplante(int organizacionID, DateTime fecha)
        {
            //List<ReporteDetalleReimplanteInfo> lista;
            DataTable lista;
            try
            {
                Logger.Info();
                var reporteDetalleReimplanteBL = new ReporteDetalleReimplanteBL();
                lista = reporteDetalleReimplanteBL.GenerarReporteInventario(organizacionID, fecha);
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
