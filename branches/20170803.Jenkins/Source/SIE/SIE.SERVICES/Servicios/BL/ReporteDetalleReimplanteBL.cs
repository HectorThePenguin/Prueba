using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    internal class ReporteDetalleReimplanteBL
    {
        /// <summary>
        /// Obtiene los datos para el Reporte detalle reimplante
        /// </summary>
        /// <returns> </returns>
        internal DataTable GenerarReporteInventario(int organizacionID, DateTime fecha)
        {
            DataTable lista;
            try
            {
                Logger.Info();
                var reporteInventarioDAL = new ReporteDetalleReimplanteDAL();
                lista = reporteInventarioDAL.GenerarReporteDetalleReimplante(organizacionID, fecha);
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
