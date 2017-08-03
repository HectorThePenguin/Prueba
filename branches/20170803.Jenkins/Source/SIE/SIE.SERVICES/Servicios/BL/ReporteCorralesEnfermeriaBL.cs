using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SIE.Services.Info.Filtros;
using SIE.Base.Log;
using SIE.Services.Info.Reportes;
using SIE.Services.Integracion.DAL.Implementacion;
using SIE.Base.Exepciones;

namespace SIE.Services.Servicios.BL
{
    public class ReporteCorralesEnfermeriaBL
    {
        readonly ReporteCorralesEnfermeriaDAL reporteCorralesEnfermeriaGanadoDAL;

        public ReporteCorralesEnfermeriaBL()
        {
            reporteCorralesEnfermeriaGanadoDAL = new ReporteCorralesEnfermeriaDAL();
        }

        /// <summary>
        /// Obtiene una lista paginada de ReporteCorralesEnfermeriaGanado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IList<ReporteCorralesEnfermeriaInfo> Generar(FiltroReporteCorralesEnfermeria filtro)
        {
            try
            {
                Logger.Info();
                return reporteCorralesEnfermeriaGanadoDAL.Generar(filtro);
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
        }

        /// <summary>
        /// Obtiene una lista paginada de ReporteCorralesEnfermeriaGanado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IList<ReporteCorralesEnfermeriaInfo> GenerarConFormato(FiltroReporteCorralesEnfermeria filtro)
        {
            try
            {
                Logger.Info();
                IList<ReporteCorralesEnfermeriaInfo> resultadoInfo =  reporteCorralesEnfermeriaGanadoDAL.Generar(filtro);

                return resultadoInfo;
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
        }

    }
}



   //Concentrado

               