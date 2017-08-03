using SIE.Base.Exepciones;
using SIE.Base.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxReporteBitacoraLlegadaDAL
    {
        /// <summary>
        /// Obtiene los parametros para alimentar el SP que obtiene los datos del reporte bitacora de llegada
        /// El TipoFecha esta a la espera de 1.-Fecha Llegada, 2.-Fecha Entrada y 3.-Fecha Pesaje
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="tipoFecha"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosBitacoraLlegada(int organizacionID, int tipoFecha, DateTime fechaInicial, DateTime fechaFinal)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionID},
                                     {"@TipoFecha", tipoFecha },
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
