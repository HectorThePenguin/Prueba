using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxReporteSolicitudPaseProcesoDAL
    {
      /// <summary>
    /// Obtiene un diccionario con los parametros
    /// necesarios para la ejecucion del procedimiento
    /// almacenado ReporteSolicitudPaseProceso
      /// </summary>
      /// <param name="organizacionID"></param>
      /// <param name="fecha"></param>
      /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReporteSolicitudPaseProceso(int organizacionID, DateTime fecha, TipoAlmacenEnum tipoAlmacen, FamiliasEnum familia)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID},
                                     {"@Fecha", fecha},
                                     {"@TipoAlmacen", tipoAlmacen.GetHashCode()},
                                     {"@Familia", familia.GetHashCode()}
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
