using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Filtros;
using SIE.Services.Info.Info;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxReporteSalidaConCostosDAL
    {
        /// <summary>
        /// Obtiene parametros para obtener el reporte de muertes de ganado.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public static Dictionary<string, object> obtenerReporte(ReporteSalidasConCostoParametrosInfo DatosConsulta)
        {
            try
            {
                Logger.Info();
                var parametros =
                    new Dictionary<string, object>
                        {
                            {"@OrganizacionID", DatosConsulta.OrganizacionID},
                            {"@FechaInicial", DatosConsulta.FechaInicial},
                            {"@FechaFinal", DatosConsulta.FechaFinal},
                            {"@TipoSalida", DatosConsulta.TipoSalida},
                            {"@TipoProceso", DatosConsulta.TipoProceso},
                            {"@EsDetallado", DatosConsulta.EsDetallado},
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
