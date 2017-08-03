using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{

    internal class AuxReporteTabularDisponibilidadDAL
    {
        /// <summary>
        /// Obtiene un datos con los parametros
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="produccion"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReporteTabularDisponibilidad(int organizacionId, TipoCorral produccion, DateTime fecha)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                                 {
                                     {"@OrganizacionID", organizacionId},
                                     {"@TipoCorral", produccion.GetHashCode()},
                                     {"@Fecha",fecha}
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
