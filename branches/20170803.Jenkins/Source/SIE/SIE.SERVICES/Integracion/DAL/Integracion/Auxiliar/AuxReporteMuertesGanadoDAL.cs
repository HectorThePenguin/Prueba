using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxReporteMuertesGanadoDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener el reporte de muertes de ganado.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> Generar(FiltroFechasInfo filtro)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", filtro.OrgazizacionId},
                            {"@FechaIni", filtro.FechaInicial},
                            {"@FechaFin", filtro.FechaFinal},
                        };
                return parametros;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
        }
    }
}

