using System;
using System.Collections.Generic;
using SIE.Base.Log;
using SIE.Base.Exepciones;
using System.Reflection;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    class AuxReportePaseaProcesoDAL
    {
        /// <summary>
        /// Obtiene un diccionario con los parametros 
        /// necesarios para la ejecuciono del procedimiento
        /// almacenado ReportePaseaProceso
        /// </summary>
        /// <param name="organizacionId"></param>
       /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReportePaseaProceso(int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                   {"@OrganizacionID", organizacionId},
                   {"@ValorActivo", (int)EstatusEnum.Activo}
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
