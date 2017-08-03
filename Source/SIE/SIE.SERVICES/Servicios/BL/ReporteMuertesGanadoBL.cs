using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Services.Info.Filtros;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;

namespace SIE.Services.Servicios.BL
{
    public class ReporteMuertesGanadoBL
    {
        readonly ReporteMuertesGanadoDAL reporteMuertesGanadoDL;

        public ReporteMuertesGanadoBL()
        {
            reporteMuertesGanadoDL = new ReporteMuertesGanadoDAL();
        }
        
        /// <summary>
        /// Obtiene una lista paginada de ReporteMuertesGanado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IList<ReporteMuertesGanadoInfo> Generar(FiltroFechasInfo filtro)
        {
            try
            {
                Logger.Info();
                return reporteMuertesGanadoDL.Generar(filtro);
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
        }
     }
}
