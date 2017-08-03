//--*********** PL *************
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
   public class ReporteProduccionVsConsumoPl
    {
        /// <summary>
        /// Obtiene los datos al informe de ReporteResumenInventario
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechainicial"></param>
        /// <param name="fechafinal"></param>
        /// <returns></returns>
        public IList<ReporteProduccionVsConsumoInfo> ObtenerReporteResumenInventario(int organizacionId,DateTime fechainicial,DateTime fechafinal)
        {
            IList<ReporteProduccionVsConsumoInfo> lista;
            try
            {
                Logger.Info();
                var reporteBl = new ReporteProduccionVsConsumoBl();
                lista = reporteBl.ObtenerParametrosReporteProduccionVsConsumo(organizacionId, fechainicial,fechafinal);

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
