﻿using System;
using System.Collections.Generic;
using System.Reflection;
using SIE.Base.Exepciones;
using SIE.Base.Log;

namespace SIE.Services.Integracion.DAL.Integracion.Auxiliar
{
    internal class AuxReporteLlegadaLogisticaDAL
    {
        /// <summary>
        /// Obtiene un diccionario con los parametros
        /// necesarios para la ejecucion del procedimiento
        /// almacenado ReporteLlegadaLogistica_Obtener
        /// </summary>
        /// <param name="organizacionID"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        internal static Dictionary<string, object> ObtenerParametrosDatosLlegadaLogistica(int organizacionID, DateTime fechaInicial, DateTime fechaFinal)
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
    }
}