﻿
//--*********** AUX *************
using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Services.Info.Enums;
using SIE.Base.Log;
namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    public class AuxReporteEntradasSinCosteoDal
    {
        /// <summary>
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ReporteEntradasSinCosteo
        /// </summary>
        /// <param name="organizacionId"></param>
        /// <param name="fechainicial"></param>
        /// <param name="fechafinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosReporteEntradasSinCosteo(int organizacionId)
        {
            Dictionary<string, object> parametros;
            try
            {
                Logger.Info();
                parametros = new Dictionary<string, object>
                {
                    {"@OrganizacionID", organizacionId},
                    {"@Activo",(int) EstatusEnum.Activo}
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
