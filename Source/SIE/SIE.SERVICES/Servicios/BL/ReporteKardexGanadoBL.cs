

using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Services.Info.Filtros;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;
using System.Data;


namespace SIE.Services.Servicios.BL
{
    public class ReporteKardexGanadoBL
    {
        readonly ReporteKardexGanadoDAL  reportekardexGanadoDL;

        public ReporteKardexGanadoBL()
        {
            reportekardexGanadoDL = new ReporteKardexGanadoDAL();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de ReporteMuertesGanado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IList<ReporteKardexGanadoInfo> Generar(FiltroParametrosKardexGanado filtro)
        {
            IList<ReporteKardexGanadoInfo> lista = null;
            try
            {
                Logger.Info();
                lista = reportekardexGanadoDL.Generar(filtro);
            }
            catch(ExcepcionGenerica)
            {
                throw;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }

            return lista;
        }
    }
}
