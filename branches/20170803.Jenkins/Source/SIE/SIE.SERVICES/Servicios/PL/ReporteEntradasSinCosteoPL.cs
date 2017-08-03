//--*********** PL *************
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Info;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteEntradasSinCosteoPL
    {
        /// <summary>
        /// Obtiene los datos al informe de ReporteEntradasSinCosteo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <returns></returns>
        public IList<ReporteEntradasSinCosteoInfo> ObtenerReporteResumenInventario(int organizacionId)
        {
            IList<ReporteEntradasSinCosteoInfo> lista = null;
            try
            {
                Logger.Info();
                var reporteBl = new ReporteEntradasSinCosteoBl();
                lista = reporteBl.ObtenerParametrosReporteEntradasSinCosteo(organizacionId);

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
