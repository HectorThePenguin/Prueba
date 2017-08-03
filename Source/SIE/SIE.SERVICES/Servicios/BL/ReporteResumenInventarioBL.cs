//--*********** BL *************
using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Constantes;
using SIE.Services.Info.Info;
using SIE.Services.Info.Enums;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteResumenInventarioBL
    {
       /// <summary>
        /// Obtiene los datos al reporte ReporteResumenInventario
       /// </summary>
       /// <param name="organizacionID"></param>
       /// <param name="familiaID"></param>
       /// <param name="fechaInicial"></param>
       /// <param name="fechaFinal"></param>
       /// <returns></returns>
        internal IList<ReporteResumenInventarioInfo> ObtenerReporteResumenInventario(int organizacionID, int familiaID, DateTime fechaInicial, DateTime fechaFinal)
        {
            IList<ReporteResumenInventarioInfo> lista = null;
            try
            {
                Logger.Info();
                var reporteDal = new ReporteResumenInventarioDAL();
               lista = reporteDal.ObtenerParametrosReporteResumenInventario(organizacionID, familiaID, fechaInicial, fechaFinal);
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
