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
    public class ReporteResumenInventarioPL
    {
       /// <summary>
        /// Obtiene los datos al informe de ReporteResumenInventario
       /// </summary>
       /// <param name="organizacionId"></param>
       /// <param name="familiaId"></param>
       /// <param name="fechaInicial"></param>
       /// <param name="fechaFinal"></param>
       /// <returns></returns>
        public IList<ReporteResumenInventarioInfo> ObtenerReporteResumenInventario(int organizacionId, int familiaId, DateTime fechaInicial, DateTime fechaFinal)
        {
            IList<ReporteResumenInventarioInfo> lista = null;
            try
            {
                Logger.Info();
                var reporteBl = new ReporteResumenInventarioBL();
                lista = reporteBl.ObtenerReporteResumenInventario(organizacionId, familiaId, fechaInicial, fechaFinal);

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

