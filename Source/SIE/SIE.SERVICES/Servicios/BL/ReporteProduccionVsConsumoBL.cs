//--*********** BL *************
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;

namespace SIE.Services.Servicios.BL
{
    public class ReporteProduccionVsConsumoBl
    {
        /// <summary>
        /// Obtiene los datos al reporte ReporteProduccionVsConsumo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechainicial"></param>
        /// <param name="fechafinal"></param>
        /// <returns></returns>
        internal IList<ReporteProduccionVsConsumoInfo> ObtenerParametrosReporteProduccionVsConsumo(int organizacionId, DateTime fechainicial,DateTime fechafinal)
        {
            IList<ReporteProduccionVsConsumoInfo> lista;
            try
            {
                Logger.Info();
                var reporteDal = new ReporteProduccionVsConsumoDal();
                lista = reporteDal.ObtenerParametrosReporteProduccionVsConsumo(organizacionId, fechainicial, fechafinal);
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
