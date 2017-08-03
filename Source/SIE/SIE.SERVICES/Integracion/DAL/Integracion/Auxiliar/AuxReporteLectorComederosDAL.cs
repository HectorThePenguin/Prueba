using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    class AuxReporteLectorComederosDAL
    {
        /// <summary>
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ReporteLectorComederos
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="horario"></param>
        /// <param name="FechaHoy"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReporteLectorComederos(int organizacionID, int horario, DateTime? FechaHoy)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID},
                                     {"@horario", horario},
                                     {"@FechaHoy", FechaHoy},
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }
    }
}
