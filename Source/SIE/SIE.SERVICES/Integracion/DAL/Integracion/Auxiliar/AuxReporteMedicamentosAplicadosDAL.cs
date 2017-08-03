using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxReporteMedicamentosAplicadosDAL
    {
        /// <summary>
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ReporteMedicamentosAplicados_Obtener
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosMedicamentosAplicados(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID},
                                     {"@FechaInicial", fechaInicial},
                                     {"@FechaFinal", fechaFinal},
                                 };
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw new ExcepcionDesconocida(MethodBase.GetCurrentMethod(), ex);
            }
            return parametros;
        }

        /// <summary>
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ReporteMedicamentosAplicadosSanidad_Obtener
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosMedicamentosAplicados(int organizacionID, DateTime fechaInicial, DateTime fechaFinal, int almacenId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@AlmacenID", almacenId},
                                     {"@OrganizacionID", organizacionID},
                                     {"@FechaInicial", fechaInicial},
                                     {"@FechaFinal", fechaFinal},
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
