using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Reportes;
using SIE.Services.Servicios.BL;

namespace SIE.Services.Servicios.PL
{
    public class ReporteKardexGanadoPL
    {
       /// <summary>
       /// Obtener el reporte de kardex
       /// </summary>
       /// <param name="filtro"></param>
       /// <returns></returns>
        public IList<ReporteKardexGanadoInfo> Generar(FiltroParametrosKardexGanado filtro)
        {
            IList<ReporteKardexGanadoInfo> result = null;
            try
            {
                Logger.Info();
                var reporteBl = new ReporteKardexGanadoBL();
                result = reporteBl.Generar(filtro);

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
            return result;

        }


    }
}
