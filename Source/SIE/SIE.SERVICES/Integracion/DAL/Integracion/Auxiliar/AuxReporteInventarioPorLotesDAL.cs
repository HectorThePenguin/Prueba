//--*********** AUX *************
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxReporteInventarioPorLotesDAL
    {
        /// <summary>
        /// Obtiene los parametros del sp
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="familiaId"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReporteInventarioPorlotes(int organizacionId, int familiaId, DateTime fecha)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionID", organizacionId},
                    {"@FamiliaID", familiaId},
                    {"@Fecha", fecha}
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