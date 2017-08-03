using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;
using SIE.Services.Info.Enums;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxReporteAuxiliarInventarioDAL
    {
        /// <summary>
        /// Obtiene un diccionario con los parametros necesarios
        /// para la ejecucion del procedimiento almancenado
        /// Corral_AuxiliarInventario
        /// </summary>
        /// <param name="codigoCorral"></param>
        /// <param name="organizacionID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosAuxiliarInventario(string codigoCorral, int organizacionID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@Corral", codigoCorral},
                        {"@OrganizacionID", organizacionID},
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
        /// Obtiene los parametros para el procedimiento del reporte
        /// </summary>
        /// <param name="loteID"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReporteAuxiliarInventario(int loteID)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                    {
                        {"@LoteID", loteID},
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
